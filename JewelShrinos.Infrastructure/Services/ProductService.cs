using JewelShrinos.Application.DTOs.Request.Product;
using JewelShrinos.Application.DTOs.Response.Product;
using JewelShrinos.Application.Interfaces;
using JewelShrinos.Core.Entities;
using JewelShrinos.Core.Interfaces;

namespace JewelShrinos.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;

    public ProductService(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product is null ? null : MapToResponse(product);
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(MapToResponse);
    }

    public async Task<ProductResponse?> GetByCodeAsync(string code)
    {
        var product = await _productRepository.FirstOrDefaultAsync(p =>
            p.Code == code);

        return product is null ? null : MapToResponse(product);
    }

    public async Task<ProductResponse?> GetByBarcodeAsync(string barcode)
    {
        var product = await _productRepository.FirstOrDefaultAsync(p =>
            p.Barcode == barcode);

        return product is null ? null : MapToResponse(product);
    }

    public async Task<ProductResponse?> GetByQrCodeAsync(string qrCode)
    {
        var product = await _productRepository.FirstOrDefaultAsync(p =>
            p.QrCode == qrCode);

        return product is null ? null : MapToResponse(product);
    }

    public async Task<IEnumerable<ProductResponse>> GetByCategoryAsync(int categoryId)
    {
        var products = await _productRepository.FindAsync(p =>
            p.CategoryId == categoryId);

        return products.Select(MapToResponse);
    }

    public async Task<IEnumerable<ProductResponse>> GetByMaterialAsync(int materialId)
    {
        var products = await _productRepository.FindAsync(p =>
            p.MaterialId == materialId);

        return products.Select(MapToResponse);
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var codeExists = await _productRepository.AnyAsync(p => p.Code == request.Code);
        if (codeExists)
            throw new InvalidOperationException("Ya existe un producto con ese código.");

        if (!string.IsNullOrWhiteSpace(request.Barcode))
        {
            var barcodeExists = await _productRepository.AnyAsync(p => p.Barcode == request.Barcode);
            if (barcodeExists)
                throw new InvalidOperationException("Ya existe un producto con ese código de barras.");
        }

        var product = new Product
        {
            Code = request.Code.Trim(),
            Barcode = request.Barcode?.Trim(),
            // QrCode no viene en tu CreateProductRequest actual
            Name = request.Name.Trim(),
            Description = request.Description?.Trim(),
            CategoryId = request.CategoryId,
            MaterialId = request.MaterialId,
            SupplierId = request.SupplierId,
            CostPrice = request.CostPrice,
            SellingPrice = request.SellingPrice,
            Weight = request.Weight,
            Certificate = request.Certificate?.Trim(),
            ImageUrl = request.ImageUrl?.Trim(),
            Sku = request.Sku?.Trim(),
            Status = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return MapToResponse(product);
    }

    public async Task<ProductResponse> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
            throw new KeyNotFoundException("Producto no encontrado.");

        if (!string.IsNullOrWhiteSpace(request.Name))
            product.Name = request.Name.Trim();

        if (request.Description is not null)
            product.Description = request.Description.Trim();

        if (request.CategoryId.HasValue)
            product.CategoryId = request.CategoryId.Value;

        if (request.MaterialId.HasValue)
            product.MaterialId = request.MaterialId.Value;

        if (request.SupplierId.HasValue)
            product.SupplierId = request.SupplierId.Value;

        if (request.CostPrice.HasValue)
            product.CostPrice = request.CostPrice.Value;

        if (request.SellingPrice.HasValue)
            product.SellingPrice = request.SellingPrice.Value;

        if (request.Weight.HasValue)
            product.Weight = request.Weight.Value;

        if (request.Status.HasValue)
            product.Status = request.Status.Value;

        product.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateAsync(product);
        await _productRepository.SaveChangesAsync();

        return MapToResponse(product);
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
}