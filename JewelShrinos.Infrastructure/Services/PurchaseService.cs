using JewelShrinos.Application.DTOs.Request.Purchase;
using JewelShrinos.Application.DTOs.Response.Purchase;
using JewelShrinos.Application.Interfaces;
using JewelShrinos.Core.Entities;
using JewelShrinos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelShrinos.Infrastructure.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IRepository<Purchase> _purchaseRepository;
    private readonly IRepository<PurchaseDetail> _purchaseDetailRepository;
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Inventory> _inventoryRepository;
    private readonly IRepository<InventoryMovement> _inventoryMovementRepository;

    public PurchaseService(
        IRepository<Purchase> purchaseRepository,
        IRepository<PurchaseDetail> purchaseDetailRepository,
        IRepository<Supplier> supplierRepository,
        IRepository<Product> productRepository,
        IRepository<Inventory> inventoryRepository,
        IRepository<InventoryMovement> inventoryMovementRepository)
    {
        _purchaseRepository = purchaseRepository;
        _purchaseDetailRepository = purchaseDetailRepository;
        _supplierRepository = supplierRepository;
        _productRepository = productRepository;
        _inventoryRepository = inventoryRepository;
        _inventoryMovementRepository = inventoryMovementRepository;
    }

    public async Task<PurchaseResponse> CreatePurchaseAsync(CreatePurchaseRequest request)
    {
        await ValidateCreateRequestAsync(request);

        var purchase = new Purchase
        {
            PurchaseNumber = await GeneratePurchaseNumberAsync(),
            SupplierId = request.SupplierId,
            PurchaseDate = request.PurchaseDate,
            DeliveryDate = request.DeliveryDate,
            PurchaseStatus = "PENDING",
            Observations = NormalizeOptional(request.Observations),
            CreatedBy = NormalizeOptional(request.CreatedBy),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            PurchaseDetails = request.PurchaseDetails.Select(d => new PurchaseDetail
            {
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                Subtotal = d.Quantity * d.UnitPrice,
                QuantityReceived = 0,
                CreatedAt = DateTime.UtcNow
            }).ToList()
        };

        purchase.TotalAmount = purchase.PurchaseDetails.Sum(x => x.Subtotal ?? 0);

        await _purchaseRepository.AddAsync(purchase);
        await _purchaseRepository.SaveChangesAsync();

        return await GetByIdAsync(purchase.PurchaseId)
            ?? throw new InvalidOperationException("No se pudo recuperar la compra creada.");
    }

   public async Task<PurchaseResponse> ReceivePurchaseAsync(int purchaseId, ReceivePurchaseRequest request)
{
    if (request.Details is null || request.Details.Count == 0)
        throw new InvalidOperationException("Debes enviar al menos un detalle para recibir.");

    var purchase = await _purchaseRepository.AsQueryable()
        .Include(p => p.Supplier)
        .Include(p => p.PurchaseDetails)
            .ThenInclude(d => d.Product)
        .FirstOrDefaultAsync(p => p.PurchaseId == purchaseId);

    if (purchase is null)
        throw new KeyNotFoundException("Compra no encontrada.");

    if (purchase.PurchaseStatus == "CANCELLED")
        throw new InvalidOperationException("No puedes recibir una compra cancelada.");

    foreach (var item in request.Details)
    {
        var detail = purchase.PurchaseDetails.FirstOrDefault(d => d.PurchaseDetailId == item.PurchaseDetailId);

        if (detail is null)
            throw new InvalidOperationException($"No existe el detalle {item.PurchaseDetailId} en la compra.");

        if (item.QuantityReceived <= 0)
            throw new InvalidOperationException("La cantidad recibida debe ser mayor a 0.");

        var pendingQuantity = detail.Quantity - detail.QuantityReceived;

        if (item.QuantityReceived > pendingQuantity)
            throw new InvalidOperationException(
                $"La cantidad recibida para el detalle {detail.PurchaseDetailId} excede lo pendiente ({pendingQuantity}).");

        // 1) Actualizar detalle de compra
        detail.QuantityReceived += item.QuantityReceived;

        // 2) Buscar inventario del producto
        var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == detail.ProductId);

        var stockBefore = inventory?.AvailableStock ?? 0;
        var stockAfter = stockBefore + item.QuantityReceived;

        if (inventory is null)
        {
            inventory = new Inventory
            {
                ProductId = detail.ProductId,
                AvailableStock = item.QuantityReceived,
                ReservedStock = 0,
                SoldStock = 0,
                DamagedStock = 0,
                LastCostPrice = detail.UnitPrice,
                LastPurchaseDate = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _inventoryRepository.AddAsync(inventory);
        }
        else
        {
            inventory.AvailableStock = stockAfter;
            inventory.LastCostPrice = detail.UnitPrice;
            inventory.LastPurchaseDate = DateTime.UtcNow;
            inventory.UpdatedAt = DateTime.UtcNow;

            // NO llames UpdateAsync(inventory) aquí.
            // Ya está trackeado por EF si vino de FirstOrDefaultAsync.
        }

        // 3) Registrar movimiento
   var movement = new InventoryMovement
{
    ProductId = detail.ProductId,
    MovementType = "PURCHASE_IN",
    Quantity = item.QuantityReceived,
    StockBefore = stockBefore,
    StockAfter = stockAfter,
    ReferenceType = "PURCHASE",
    ReferenceId = purchase.PurchaseId,
    Observations = $"Recepción de compra {purchase.PurchaseNumber}",
    MovementDate = DateTime.UtcNow
};

await _inventoryMovementRepository.AddAsync(movement);
    }

    // 4) Actualizar estado de la compra
    purchase.UpdatedAt = DateTime.UtcNow;

    if (purchase.PurchaseDetails.All(d => d.QuantityReceived >= d.Quantity))
        purchase.PurchaseStatus = "RECEIVED";
    else if (purchase.PurchaseDetails.Any(d => d.QuantityReceived > 0))
        purchase.PurchaseStatus = "PARTIAL";

    if (!string.IsNullOrWhiteSpace(request.Observations))
        purchase.Observations = request.Observations.Trim();

    // NO llames UpdateAsync(purchase) aquí.
    // purchase ya está trackeada porque la cargaste con Include.

    await _purchaseRepository.SaveChangesAsync();

    return await GetByIdAsync(purchase.PurchaseId)
        ?? throw new InvalidOperationException("No se pudo recuperar la compra recibida.");
}

    public async Task<bool> CancelPurchaseAsync(int purchaseId)
    {
        var purchase = await _purchaseRepository.AsQueryable()
            .Include(p => p.PurchaseDetails)
            .FirstOrDefaultAsync(p => p.PurchaseId == purchaseId);

        if (purchase is null)
            return false;

        if (purchase.PurchaseDetails.Any(d => d.QuantityReceived > 0))
            throw new InvalidOperationException("No puedes cancelar una compra que ya recibió mercadería.");

        purchase.PurchaseStatus = "CANCELLED";
        purchase.UpdatedAt = DateTime.UtcNow;

        await _purchaseRepository.UpdateAsync(purchase);
        await _purchaseRepository.SaveChangesAsync();

        return true;
    }

    public async Task<PurchaseResponse?> GetByIdAsync(int id)
    {
        var purchase = await _purchaseRepository.AsQueryable()
            .Include(p => p.Supplier)
            .Include(p => p.PurchaseDetails)
                .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(p => p.PurchaseId == id);

        return purchase is null ? null : MapToResponse(purchase);
    }

    public async Task<IEnumerable<PurchaseResponse>> GetAllAsync()
    {
        var purchases = await _purchaseRepository.AsQueryable()
            .Include(p => p.Supplier)
            .Include(p => p.PurchaseDetails)
                .ThenInclude(d => d.Product)
            .OrderByDescending(p => p.PurchaseId)
            .ToListAsync();

        return purchases.Select(MapToResponse);
    }

    public async Task<PurchaseResponse?> GetByPurchaseNumberAsync(string purchaseNumber)
    {
        if (string.IsNullOrWhiteSpace(purchaseNumber))
            throw new InvalidOperationException("El número de compra es obligatorio.");

        var normalized = purchaseNumber.Trim();

        var purchase = await _purchaseRepository.AsQueryable()
            .Include(p => p.Supplier)
            .Include(p => p.PurchaseDetails)
                .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(p => p.PurchaseNumber == normalized);

        return purchase is null ? null : MapToResponse(purchase);
    }

    public async Task<IEnumerable<PurchaseResponse>> GetBySupplierAsync(int supplierId)
    {
        var purchases = await _purchaseRepository.AsQueryable()
            .Include(p => p.Supplier)
            .Include(p => p.PurchaseDetails)
                .ThenInclude(d => d.Product)
            .Where(p => p.SupplierId == supplierId)
            .OrderByDescending(p => p.PurchaseId)
            .ToListAsync();

        return purchases.Select(MapToResponse);
    }

    private async Task ValidateCreateRequestAsync(CreatePurchaseRequest request)
    {
        if (request.SupplierId <= 0)
            throw new InvalidOperationException("El proveedor es obligatorio.");

        if (request.PurchaseDetails is null || request.PurchaseDetails.Count == 0)
            throw new InvalidOperationException("Debes agregar al menos un producto a la compra.");

        var supplierExists = await _supplierRepository.AnyAsync(s => s.SupplierId == request.SupplierId);
        if (!supplierExists)
            throw new InvalidOperationException("El proveedor seleccionado no existe.");

        var duplicatedProducts = request.PurchaseDetails
            .GroupBy(d => d.ProductId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicatedProducts.Count > 0)
            throw new InvalidOperationException("No repitas el mismo producto en varios detalles de la compra.");

        foreach (var detail in request.PurchaseDetails)
        {
            if (detail.ProductId <= 0)
                throw new InvalidOperationException("Cada detalle debe tener un producto válido.");

            if (detail.Quantity <= 0)
                throw new InvalidOperationException("La cantidad debe ser mayor a 0.");

            if (detail.UnitPrice <= 0)
                throw new InvalidOperationException("El precio unitario debe ser mayor a 0.");

            var productExists = await _productRepository.AnyAsync(p => p.ProductId == detail.ProductId);
            if (!productExists)
                throw new InvalidOperationException($"El producto {detail.ProductId} no existe.");
        }
    }

    private async Task<string> GeneratePurchaseNumberAsync()
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);

        var countToday = await _purchaseRepository.CountAsync(p =>
            p.CreatedAt >= today && p.CreatedAt < tomorrow);

        return $"C-{today:yyyyMMdd}-{(countToday + 1):000}";
    }

    private static PurchaseResponse MapToResponse(Purchase purchase)
    {
        return new PurchaseResponse
        {
            PurchaseId = purchase.PurchaseId,
            PurchaseNumber = purchase.PurchaseNumber,
            SupplierId = purchase.SupplierId,
            SupplierName = purchase.Supplier?.Name,
            PurchaseDate = purchase.PurchaseDate,
            DeliveryDate = purchase.DeliveryDate,
            TotalAmount = purchase.TotalAmount,
            PurchaseStatus = purchase.PurchaseStatus,
            Observations = purchase.Observations,
            CreatedBy = purchase.CreatedBy,
            CreatedAt = purchase.CreatedAt,
            PurchaseDetails = purchase.PurchaseDetails.Select(d => new PurchaseDetailResponse
            {
                PurchaseDetailId = d.PurchaseDetailId,
                ProductId = d.ProductId,
                ProductName = d.Product?.Name,
                Quantity = d.Quantity,
                QuantityReceived = d.QuantityReceived,
                UnitPrice = d.UnitPrice,
                Subtotal = d.Subtotal,
                Observations = d.Observations
            }).ToList()
        };
    }

    private static string? NormalizeOptional(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}