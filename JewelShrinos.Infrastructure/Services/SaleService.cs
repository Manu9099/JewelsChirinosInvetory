using JewelShrinos.Application.DTOs.Request.Sale;
using JewelShrinos.Application.DTOs.Response.Sale;
using JewelShrinos.Application.Interfaces;
using JewelShrinos.Core.Entities;
using JewelShrinos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelShrinos.Infrastructure.Services;

public class SaleService : ISaleService
{
    private readonly IRepository<Sale> _saleRepository;
    private readonly IRepository<Inventory> _inventoryRepository;
    private readonly IRepository<InventoryMovement> _inventoryMovementRepository;
    private readonly IRepository<Product> _productRepository;

    public SaleService(
        IRepository<Sale> saleRepository,
        IRepository<Inventory> inventoryRepository,
        IRepository<InventoryMovement> inventoryMovementRepository,
        IRepository<Product> productRepository)
    {
        _saleRepository = saleRepository;
        _inventoryRepository = inventoryRepository;
        _inventoryMovementRepository = inventoryMovementRepository;
        _productRepository = productRepository;
    }

    public async Task<SaleResponse> CreateSaleAsync(CreateSaleRequest request)
    {
        await ValidateCreateRequestAsync(request);

        var productCache = new Dictionary<int, Product>();
        var inventoryCache = new Dictionary<int, Inventory>();

        foreach (var detail in request.SaleDetails)
        {
            var product = await _productRepository.FirstOrDefaultAsync(p => p.ProductId == detail.ProductId)
                          ?? throw new InvalidOperationException($"El producto {detail.ProductId} no existe.");

            var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == detail.ProductId)
                           ?? throw new InvalidOperationException($"El producto {detail.ProductId} no tiene inventario creado.");

            if (inventory.AvailableStock < detail.Quantity)
            {
                throw new InvalidOperationException(
                    $"Stock insuficiente para el producto {product.Name}. Disponible: {inventory.AvailableStock}, solicitado: {detail.Quantity}.");
            }

            productCache[detail.ProductId] = product;
            inventoryCache[detail.ProductId] = inventory;
        }

        var saleDetails = request.SaleDetails.Select(d =>
        {
            var product = productCache[d.ProductId];
            var unitPrice = d.UnitPrice.HasValue && d.UnitPrice.Value > 0
                ? d.UnitPrice.Value
                : product.SellingPrice;

            var lineDiscount = d.LineDiscount < 0 ? 0 : d.LineDiscount;
            var lineSubtotal = (d.Quantity * unitPrice) - lineDiscount;

            return new SaleDetail
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitPrice = unitPrice,
                LineDiscount = lineDiscount,
                Subtotal = lineSubtotal,
                DetailStatus = "SOLD",
                CreatedAt = DateTime.UtcNow
            };
        }).ToList();

        var subtotalAmount = saleDetails.Sum(x => x.Subtotal ?? 0m);
        var taxAmount = request.TaxAmount < 0 ? 0 : request.TaxAmount;
        var discountAmount = request.DiscountAmount < 0 ? 0 : request.DiscountAmount;
        var totalAmount = subtotalAmount - discountAmount + taxAmount;

        var sale = new Sale
        {
            SaleNumber = await GenerateSaleNumberAsync(),
            CustomerId = request.CustomerId,
            SubtotalAmount = subtotalAmount,
            TaxAmount = taxAmount,
            DiscountAmount = discountAmount,
            TotalAmount = totalAmount,
            PaymentMethod = NormalizeOptional(request.PaymentMethod),
            SaleStatus = "COMPLETED",
            Observations = NormalizeOptional(request.Observations),
            CreatedBy = NormalizeOptional(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            SaleDetails = saleDetails
        };

        await _saleRepository.AddAsync(sale);
        await _saleRepository.SaveChangesAsync();

        foreach (var detail in sale.SaleDetails)
        {
            var inventory = inventoryCache[detail.ProductId];

            var stockBefore = inventory.AvailableStock;
            var stockAfter = stockBefore - detail.Quantity;

            inventory.AvailableStock = stockAfter;
            inventory.SoldStock += detail.Quantity;
            inventory.LastSellingPrice = detail.UnitPrice;
            inventory.LastSaleDate = DateTime.UtcNow;
            inventory.UpdatedAt = DateTime.UtcNow;

            var movement = new InventoryMovement
            {
                ProductId = detail.ProductId,
                MovementType = "SALE_OUT",
                Quantity = detail.Quantity,
                StockBefore = stockBefore,
                StockAfter = stockAfter,
                ReferenceType = "SALE",
                ReferenceId = sale.SaleId,
                Observations = $"Venta {sale.SaleNumber}",
                MovementDate = DateTime.UtcNow
            };

            await _inventoryMovementRepository.AddAsync(movement);
        }

        await _saleRepository.SaveChangesAsync();

        return await GetByIdAsync(sale.SaleId)
               ?? throw new InvalidOperationException("No se pudo recuperar la venta creada.");
    }

    public async Task<bool> CancelSaleAsync(int saleId)
    {
        var sale = await _saleRepository.AsQueryable()
            .Include(s => s.SaleDetails)
            .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(s => s.SaleId == saleId);

        if (sale is null) return false;

        if (sale.SaleStatus == "CANCELLED")
            throw new InvalidOperationException("La venta ya está cancelada.");

        foreach (var detail in sale.SaleDetails.Where(d => d.DetailStatus != "CANCELLED"))
        {
            var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == detail.ProductId)
                           ?? throw new InvalidOperationException($"No existe inventario para el producto {detail.ProductId}.");

            var stockBefore = inventory.AvailableStock;
            var stockAfter = stockBefore + detail.Quantity;

            inventory.AvailableStock = stockAfter;
            inventory.SoldStock = Math.Max(0, inventory.SoldStock - detail.Quantity);
            inventory.UpdatedAt = DateTime.UtcNow;

            detail.DetailStatus = "CANCELLED";

            var movement = new InventoryMovement
            {
                ProductId = detail.ProductId,
                MovementType = "SALE_CANCEL",
                Quantity = detail.Quantity,
                StockBefore = stockBefore,
                StockAfter = stockAfter,
                ReferenceType = "SALE",
                ReferenceId = sale.SaleId,
                Observations = $"Anulación de venta {sale.SaleNumber}",
                MovementDate = DateTime.UtcNow
            };

            await _inventoryMovementRepository.AddAsync(movement);
        }

        sale.SaleStatus = "CANCELLED";
        sale.UpdatedAt = DateTime.UtcNow;

        await _saleRepository.SaveChangesAsync();
        return true;
    }

public async Task<SaleResponse?> GetByIdAsync(int id)
{
    var sale = await _saleRepository.AsQueryable()
        .Include(s => s.Customer)
        .Include(s => s.SaleDetails)
        .ThenInclude(d => d.Product)
        .FirstOrDefaultAsync(s => s.SaleId == id);

    return sale is null ? null : MapToResponse(sale);
}

public async Task<IEnumerable<SaleResponse>> GetAllAsync()
{
    var sales = await _saleRepository.AsQueryable()
        .Include(s => s.Customer)
        .Include(s => s.SaleDetails)
        .ThenInclude(d => d.Product)
        .OrderByDescending(s => s.SaleId)
        .ToListAsync();

    return sales.Select(MapToResponse);
}

public async Task<SaleResponse?> GetBySaleNumberAsync(string saleNumber)
{
    if (string.IsNullOrWhiteSpace(saleNumber))
        throw new InvalidOperationException("El número de venta es obligatorio.");

    var normalized = saleNumber.Trim();

    var sale = await _saleRepository.AsQueryable()
        .Include(s => s.Customer)
        .Include(s => s.SaleDetails)
        .ThenInclude(d => d.Product)
        .FirstOrDefaultAsync(s => s.SaleNumber == normalized);

    return sale is null ? null : MapToResponse(sale);
}

public async Task<IEnumerable<SaleResponse>> GetByCustomerAsync(int customerId)
{
    var sales = await _saleRepository.AsQueryable()
        .Include(s => s.Customer)
        .Include(s => s.SaleDetails)
        .ThenInclude(d => d.Product)
        .Where(s => s.CustomerId == customerId)
        .OrderByDescending(s => s.SaleId)
        .ToListAsync();

    return sales.Select(MapToResponse);
}

public async Task<IEnumerable<SaleResponse>> GetByDateAsync(DateTime date)
{
    var start = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
    var end = start.AddDays(1);

    var sales = await _saleRepository.AsQueryable()
        .Include(s => s.Customer)
        .Include(s => s.SaleDetails)
        .ThenInclude(d => d.Product)
        .Where(s => s.CreatedAt >= start && s.CreatedAt < end)
        .OrderByDescending(s => s.SaleId)
        .ToListAsync();

    return sales.Select(MapToResponse);
}
    private async Task ValidateCreateRequestAsync(CreateSaleRequest request)
    {
        if (request.SaleDetails is null || request.SaleDetails.Count == 0)
            throw new InvalidOperationException("Debes agregar al menos un producto a la venta.");

        if (request.DiscountAmount < 0)
            throw new InvalidOperationException("El descuento general no puede ser negativo.");

        if (request.TaxAmount < 0)
            throw new InvalidOperationException("El impuesto no puede ser negativo.");

        var duplicatedProducts = request.SaleDetails
            .GroupBy(d => d.ProductId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicatedProducts.Count > 0)
            throw new InvalidOperationException("No repitas el mismo producto en varios detalles de la venta.");

        foreach (var detail in request.SaleDetails)
        {
            if (detail.ProductId <= 0)
                throw new InvalidOperationException("Cada detalle debe tener un producto válido.");

            if (detail.Quantity <= 0)
                throw new InvalidOperationException("La cantidad debe ser mayor a 0.");

            if (detail.UnitPrice.HasValue && detail.UnitPrice.Value <= 0)
                throw new InvalidOperationException("El precio unitario debe ser mayor a 0.");

            if (detail.LineDiscount < 0)
                throw new InvalidOperationException("El descuento de línea no puede ser negativo.");

            var productExists = await _productRepository.AnyAsync(p => p.ProductId == detail.ProductId);
            if (!productExists)
                throw new InvalidOperationException($"El producto {detail.ProductId} no existe.");
        }
    }

    private async Task<string> GenerateSaleNumberAsync()
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);

        var countToday = await _saleRepository.CountAsync(s => s.CreatedAt >= today && s.CreatedAt < tomorrow);

        return $"V-{today:yyyyMMdd}-{(countToday + 1):000}";
    }

    private static SaleResponse MapToResponse(Sale sale)
    {
        return new SaleResponse
        {
            SaleId = sale.SaleId,
            SaleNumber = sale.SaleNumber,
            CustomerId = sale.CustomerId,
            CustomerName = sale.Customer is null
                ? null
                : $"{sale.Customer.FirstName} {sale.Customer.LastName}".Trim(),
            SubtotalAmount = sale.SubtotalAmount,
            TaxAmount = sale.TaxAmount,
            DiscountAmount = sale.DiscountAmount,
            TotalAmount = sale.TotalAmount,
            PaymentMethod = sale.PaymentMethod,
            SaleStatus = sale.SaleStatus,
            InvoiceNumber = sale.InvoiceNumber,
            SunatTicketNumber = sale.SunatTicketNumber,
      //      Observation = sale.Observations,
        //    CreatedBy = sale.CreatedBy,
            CreatedAt = sale.CreatedAt,
            SaleDetails = sale.SaleDetails.Select(d => new SaleDetailResponse
            {
                SaleDetailId = d.SaleDetailId,
                ProductId = d.ProductId,
                ProductName = d.Product?.Name,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                Subtotal = d.Subtotal ?? 0,
                LineDiscount = d.LineDiscount,
                DetailStatus = d.DetailStatus
            }).ToList()
        };
    }

    private static string? NormalizeOptional(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}