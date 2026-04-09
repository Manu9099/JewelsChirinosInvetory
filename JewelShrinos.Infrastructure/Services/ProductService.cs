using JewelShrinos.Application.DTOs.Request.Product;
using JewelShrinos.Application.DTOs.Response.Product;
using JewelShrinos.Application.Interfaces;
using JewelShrinos.Core.Entities;
using JewelShrinos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelShrinos.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Material> _materialRepository;
    private readonly IRepository<Supplier> _supplierRepository;

    public ProductService(
        IRepository<Product> productRepository,
        IRepository<Category> categoryRepository,
        IRepository<Material> materialRepository,
        IRepository<Supplier> supplierRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _materialRepository = materialRepository;
        _supplierRepository = supplierRepository;
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var product = await _productRepository.AsQueryable()
            .Include(p => p.Category)
            .Include(p => p.Material)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        return product is null ? null : MapToResponse(product);
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = await _productRepository.AsQueryable()
            .Include(p => p.Category)
            .Include(p => p.Material)
            .Include(p => p.Supplier)
            .OrderByDescending(p => p.ProductId)
            .ToListAsync();

        return products.Select(MapToResponse);
    }

    public async Task<ProductResponse?> GetByCodeAsync(string code)
    {
        var normalizedCode = NormalizeRequired(code, "El código es obligatorio.");

        var product = await _productRepository.AsQueryable()
            .Include(p => p.Category)
            .Include(p => p.Material)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Code == normalizedCode);

        return product is null ? null : MapToResponse(product);
    }

    public async Task<ProductResponse?> GetByBarcodeAsync(string barcode)
    {
        var normalizedBarcode = NormalizeRequired(barcode, "El barcode es obligatorio.");

        var product = await _productRepository.AsQueryable()
            .Include(p => p.Category)
            .Include(p => p.Material)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.Barcode == normalizedBarcode);

        return product is null ? null : MapToResponse(product);
    }

    public async Task<ProductResponse?> GetByQrCodeAsync(string qrCode)
    {
        var normalizedQr = NormalizeRequired(qrCode, "El QR es obligatorio.");

        var product = await _productRepository.AsQueryable()
            .Include(p => p.Category)
            .Include(p => p.Material)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(p => p.QrCode == normalizedQr);

        return product is null ? null : MapToResponse(product);
    }

    public async Task<IEnumerable<ProductResponse>> GetByCategoryAsync(int categoryId)
    {
        var products = await _productRepository.AsQueryable()
            .Include(p => p.Category)
            .Include(p => p.Material)
            .Include(p => p.Supplier)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

        return products.Select(MapToResponse);
    }

    public async Task<IEnumerable<ProductResponse>> GetByMaterialAsync(int materialId)
    {
        var products = await _productRepository.AsQueryable()
            .Include(p => p.Category)
            .Include(p => p.Material)
            .Include(p => p.Supplier)
            .Where(p => p.MaterialId == materialId)
            .ToListAsync();

        return products.Select(MapToResponse);
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        await ValidateCreateRequestAsync(request);

        var normalizedCode = request.Code.Trim();
        var normalizedBarcode = NormalizeOptional(request.Barcode);
        var normalizedName = request.Name.Trim();

        var codeExists = await _productRepository.AnyAsync(p => p.Code == normalizedCode);
        if (codeExists)
            throw new InvalidOperationException("Ya existe un producto con ese código.");

        if (!string.IsNullOrWhiteSpace(normalizedBarcode))
        {
            var barcodeExists = await _productRepository.AnyAsync(p => p.Barcode == normalizedBarcode);
            if (barcodeExists)
                throw new InvalidOperationException("Ya existe un producto con ese código de barras.");
        }

        var product = new Product
        {
            Code = normalizedCode,
            Barcode = normalizedBarcode,
            Name = normalizedName,
            Description = NormalizeOptional(request.Description),
            CategoryId = request.CategoryId,
            MaterialId = request.MaterialId,
            SupplierId = request.SupplierId,
            CostPrice = request.CostPrice,
            SellingPrice = request.SellingPrice,
            Weight = request.Weight,
            Certificate = NormalizeOptional(request.Certificate),
            ImageUrl = NormalizeOptional(request.ImageUrl),
            Sku = NormalizeOptional(request.Sku),
            Status = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return await GetByIdAsync(product.ProductId)
            ?? throw new InvalidOperationException("No se pudo recuperar el producto creado.");
    }

    public async Task<ProductResponse> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
            throw new KeyNotFoundException("Producto no encontrado.");

        if (request.Name is not null)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new InvalidOperationException("El nombre no puede estar vacío.");

            product.Name = request.Name.Trim();
        }

        if (request.Description is not null)
            product.Description = request.Description.Trim();

        if (request.CategoryId.HasValue)
        {
            var categoryExists = await _categoryRepository.AnyAsync(c => c.CategoryId == request.CategoryId.Value);
            if (!categoryExists)
                throw new InvalidOperationException("La categoría seleccionada no existe.");

            product.CategoryId = request.CategoryId.Value;
        }

        if (request.MaterialId.HasValue)
        {
            var materialExists = await _materialRepository.AnyAsync(m => m.MaterialId == request.MaterialId.Value);
            if (!materialExists)
                throw new InvalidOperationException("El material seleccionado no existe.");

            product.MaterialId = request.MaterialId.Value;
        }

        if (request.SupplierId.HasValue)
        {
            var supplierExists = await _supplierRepository.AnyAsync(s => s.SupplierId == request.SupplierId.Value);
            if (!supplierExists)
                throw new InvalidOperationException("El proveedor seleccionado no existe.");

            product.SupplierId = request.SupplierId.Value;
        }

        if (request.CostPrice.HasValue)
        {
            if (request.CostPrice.Value <= 0)
                throw new InvalidOperationException("El costo debe ser mayor a 0.");

            product.CostPrice = request.CostPrice.Value;
        }

        if (request.SellingPrice.HasValue)
        {
            if (request.SellingPrice.Value <= 0)
                throw new InvalidOperationException("El precio de venta debe ser mayor a 0.");

            product.SellingPrice = request.SellingPrice.Value;
        }

        if (product.SellingPrice < product.CostPrice)
            throw new InvalidOperationException("El precio de venta no puede ser menor que el costo.");

        if (request.Weight.HasValue)
        {
            if (request.Weight.Value < 0)
                throw new InvalidOperationException("El peso no puede ser negativo.");

            product.Weight = request.Weight.Value;
        }

        if (request.Status.HasValue)
            product.Status = request.Status.Value;

        product.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateAsync(product);
        await _productRepository.SaveChangesAsync();

        return await GetByIdAsync(product.ProductId)
            ?? throw new InvalidOperationException("No se pudo recuperar el producto actualizado.");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
            return false;

        await _productRepository.DeleteAsync(product);
        await _productRepository.SaveChangesAsync();

        return true;
    }

    private async Task ValidateCreateRequestAsync(CreateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Code))
            throw new InvalidOperationException("El código es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Name))
            throw new InvalidOperationException("El nombre es obligatorio.");

        if (request.CostPrice <= 0)
            throw new InvalidOperationException("El costo debe ser mayor a 0.");

        if (request.SellingPrice <= 0)
            throw new InvalidOperationException("El precio de venta debe ser mayor a 0.");

        if (request.SellingPrice < request.CostPrice)
            throw new InvalidOperationException("El precio de venta no puede ser menor que el costo.");

        if (request.Weight.HasValue && request.Weight.Value < 0)
            throw new InvalidOperationException("El peso no puede ser negativo.");

        var categoryExists = await _categoryRepository.AnyAsync(c => c.CategoryId == request.CategoryId);
        if (!categoryExists)
            throw new InvalidOperationException("La categoría seleccionada no existe.");

        if (request.MaterialId.HasValue)
        {
            var materialExists = await _materialRepository.AnyAsync(m => m.MaterialId == request.MaterialId.Value);
            if (!materialExists)
                throw new InvalidOperationException("El material seleccionado no existe.");
        }

        var supplierExists = await _supplierRepository.AnyAsync(s => s.SupplierId == request.SupplierId);
        if (!supplierExists)
            throw new InvalidOperationException("El proveedor seleccionado no existe.");
    }

    private static ProductResponse MapToResponse(Product product)
    {
        return new ProductResponse
        {
            ProductId = product.ProductId,
            Code = product.Code,
            Barcode = product.Barcode,
            QrCode = product.QrCode,
            Name = product.Name,
            Description = product.Description,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name,
            MaterialId = product.MaterialId,
            MaterialName = product.Material?.Name,
            SupplierId = product.SupplierId,
            SupplierName = product.Supplier?.Name,
            CostPrice = product.CostPrice,
            SellingPrice = product.SellingPrice,
            MarginPercentage = CalculateMargin(product.CostPrice, product.SellingPrice),
            Weight = product.Weight,
            Status = product.Status
        };
    }

    private static decimal CalculateMargin(decimal costPrice, decimal sellingPrice)
    {
        if (costPrice <= 0)
            return 0;

        return ((sellingPrice - costPrice) / costPrice) * 100;
    }

    private static string NormalizeRequired(string value, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException(errorMessage);

        return value.Trim();
    }

    private static string? NormalizeOptional(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}