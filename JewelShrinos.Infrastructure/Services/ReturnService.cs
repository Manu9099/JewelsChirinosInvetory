using JewelShrinos.Application.DTOs.Request.Return;
using JewelShrinos.Application.DTOs.Response.Return;
using JewelShrinos.Application.Interfaces;
using JewelShrinos.Core.Entities;
using JewelShrinos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelShrinos.Infrastructure.Services;

public class ReturnService : IReturnService
{
    private readonly IRepository<Return> _returnRepository;
    private readonly IRepository<Sale> _saleRepository;
    private readonly IRepository<SaleDetail> _saleDetailRepository;
    private readonly IRepository<Inventory> _inventoryRepository;
    private readonly IRepository<InventoryMovement> _inventoryMovementRepository;

    public ReturnService(
        IRepository<Return> returnRepository,
        IRepository<Sale> saleRepository,
        IRepository<SaleDetail> saleDetailRepository,
        IRepository<Inventory> inventoryRepository,
        IRepository<InventoryMovement> inventoryMovementRepository)
    {
        _returnRepository = returnRepository;
        _saleRepository = saleRepository;
        _saleDetailRepository = saleDetailRepository;
        _inventoryRepository = inventoryRepository;
        _inventoryMovementRepository = inventoryMovementRepository;
    }

    public async Task<ReturnResponse> CreateReturnAsync(CreateReturnRequest request)
    {
        if (request.SaleId <= 0)
            throw new InvalidOperationException("La venta es obligatoria.");

        if (string.IsNullOrWhiteSpace(request.Reason))
            throw new InvalidOperationException("El motivo es obligatorio.");

        if (request.ReturnDetails is null || request.ReturnDetails.Count == 0)
            throw new InvalidOperationException("Debes agregar al menos un detalle a la devolución.");

        var sale = await _saleRepository.AsQueryable()
            .Include(s => s.Customer)
            .Include(s => s.SaleDetails)
            .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(s => s.SaleId == request.SaleId);

        if (sale is null)
            throw new InvalidOperationException("La venta no existe.");

        if (sale.SaleStatus == "CANCELLED")
            throw new InvalidOperationException("No puedes devolver una venta cancelada.");

        var duplicatedSaleDetails = request.ReturnDetails
            .GroupBy(x => x.SaleDetailId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        if (duplicatedSaleDetails.Count > 0)
            throw new InvalidOperationException("No repitas el mismo saleDetailId en la devolución.");

        var details = new List<ReturnDetail>();
        decimal refundAmount = 0m;

        foreach (var item in request.ReturnDetails)
        {
            if (item.SaleDetailId <= 0)
                throw new InvalidOperationException("Cada detalle debe tener un saleDetailId válido.");

            if (item.QuantityReturned <= 0)
                throw new InvalidOperationException("La cantidad devuelta debe ser mayor a 0.");

            var saleDetail = sale.SaleDetails.FirstOrDefault(d => d.SaleDetailId == item.SaleDetailId);
            if (saleDetail is null)
                throw new InvalidOperationException($"El detalle de venta {item.SaleDetailId} no pertenece a la venta {sale.SaleId}.");

            var previouslyRequested = await _returnRepository.AsQueryable()
                .Where(r => r.SaleId == sale.SaleId && r.ReturnStatus != "REJECTED")
                .SelectMany(r => r.ReturnDetails)
                .Where(rd => rd.SaleDetailId == item.SaleDetailId)
                .SumAsync(rd => (int?)rd.QuantityReturned) ?? 0;

            var remaining = saleDetail.Quantity - previouslyRequested;

            if (item.QuantityReturned > remaining)
                throw new InvalidOperationException(
                    $"La cantidad a devolver para el detalle {item.SaleDetailId} excede lo disponible. Restante: {remaining}.");

            var subtotal = saleDetail.UnitPrice * item.QuantityReturned;
            refundAmount += subtotal;

            details.Add(new ReturnDetail
            {
                SaleDetailId = item.SaleDetailId,
                QuantityReturned = item.QuantityReturned,
                UnitPrice = saleDetail.UnitPrice,
                Subtotal = subtotal,
                CreatedAt = DateTime.UtcNow
            });
        }

        var entity = new Return
        {
            ReturnNumber = await GenerateReturnNumberAsync(),
            SaleId = sale.SaleId,
            CustomerId = request.CustomerId ?? sale.CustomerId,
            Reason = request.Reason.Trim(),
            RefundAmount = refundAmount,
            ReturnStatus = "PENDING",
            CreatedBy = NormalizeOptional(request.CreatedBy),
            Observations = NormalizeOptional(request.Observations),
            RequestDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ReturnDetails = details
        };

        await _returnRepository.AddAsync(entity);
        await _returnRepository.SaveChangesAsync();

        return await GetByIdAsync(entity.ReturnId)
               ?? throw new InvalidOperationException("No se pudo recuperar la devolución creada.");
    }

    public async Task<bool> ApproveReturnAsync(int returnId)
    {
        var entity = await _returnRepository.AsQueryable()
            .Include(r => r.Sale)
            .ThenInclude(s => s.Customer)
            .Include(r => r.ReturnDetails)
            .ThenInclude(rd => rd.SaleDetail)
            .ThenInclude(sd => sd!.Product)
            .FirstOrDefaultAsync(r => r.ReturnId == returnId);

        if (entity is null) return false;

        if (entity.ReturnStatus == "APPROVED")
            throw new InvalidOperationException("La devolución ya fue aprobada.");

        if (entity.ReturnStatus == "REJECTED")
            throw new InvalidOperationException("No puedes aprobar una devolución rechazada.");

        decimal refundAmount = 0m;

        foreach (var detail in entity.ReturnDetails)
        {
            if (detail.SaleDetail is null)
                throw new InvalidOperationException($"No se encontró el SaleDetail {detail.SaleDetailId}.");

            var saleDetail = detail.SaleDetail;

            var inventory = await _inventoryRepository.FirstOrDefaultAsync(i => i.ProductId == saleDetail.ProductId)
                           ?? throw new InvalidOperationException($"No existe inventario para el producto {saleDetail.ProductId}.");

            var approvedBefore = await _returnRepository.AsQueryable()
                .Where(r => r.SaleId == entity.SaleId
                            && r.ReturnId != entity.ReturnId
                            && r.ReturnStatus == "APPROVED")
                .SelectMany(r => r.ReturnDetails)
                .Where(rd => rd.SaleDetailId == detail.SaleDetailId)
                .SumAsync(rd => (int?)rd.QuantityReturned) ?? 0;

            var totalApproved = approvedBefore + detail.QuantityReturned;

            if (totalApproved > saleDetail.Quantity)
                throw new InvalidOperationException(
                    $"La devolución aprobada excede la cantidad vendida para el detalle {detail.SaleDetailId}.");

            var stockBefore = inventory.AvailableStock;
            var stockAfter = stockBefore + detail.QuantityReturned;

            inventory.AvailableStock = stockAfter;
            inventory.SoldStock = Math.Max(0, inventory.SoldStock - detail.QuantityReturned);
            inventory.UpdatedAt = DateTime.UtcNow;

            refundAmount += detail.Subtotal ?? 0m;

            var movement = new InventoryMovement
            {
                ProductId = saleDetail.ProductId,
                MovementType = "RETURN_IN",
                Quantity = detail.QuantityReturned,
                StockBefore = stockBefore,
                StockAfter = stockAfter,
                ReferenceType = "RETURN",
                ReferenceId = entity.ReturnId,
                Observations = $"Devolución aprobada {entity.ReturnNumber}",
                MovementDate = DateTime.UtcNow
            };

            await _inventoryMovementRepository.AddAsync(movement);

            saleDetail.DetailStatus = totalApproved >= saleDetail.Quantity ? "RETURNED" : "SOLD";
        }

        entity.RefundAmount = refundAmount;
        entity.ReturnStatus = "APPROVED";
        entity.ProcessingDate = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        await _returnRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RejectReturnAsync(int returnId)
    {
        var entity = await _returnRepository.FirstOrDefaultAsync(r => r.ReturnId == returnId);
        if (entity is null) return false;

        if (entity.ReturnStatus == "APPROVED")
            throw new InvalidOperationException("No puedes rechazar una devolución aprobada.");

        if (entity.ReturnStatus == "REJECTED")
            throw new InvalidOperationException("La devolución ya fue rechazada.");

        entity.ReturnStatus = "REJECTED";
        entity.ProcessingDate = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        await _returnRepository.SaveChangesAsync();
        return true;
    }

    public async Task<ReturnResponse?> GetByIdAsync(int id)
    {
        var entity = await _returnRepository.AsQueryable()
            .Include(r => r.Sale)
            .ThenInclude(s => s.Customer)
            .Include(r => r.Customer)
            .Include(r => r.ReturnDetails)
            .ThenInclude(rd => rd.SaleDetail)
            .ThenInclude(sd => sd!.Product)
            .FirstOrDefaultAsync(r => r.ReturnId == id);

        return entity is null ? null : MapToResponse(entity);
    }

    public async Task<IEnumerable<ReturnResponse>> GetPendingAsync()
    {
        var entities = await _returnRepository.AsQueryable()
            .Include(r => r.Sale)
            .ThenInclude(s => s.Customer)
            .Include(r => r.Customer)
            .Include(r => r.ReturnDetails)
            .ThenInclude(rd => rd.SaleDetail)
            .ThenInclude(sd => sd!.Product)
            .Where(r => r.ReturnStatus == "PENDING")
            .OrderByDescending(r => r.ReturnId)
            .ToListAsync();

        return entities.Select(MapToResponse);
    }

    public async Task<IEnumerable<ReturnResponse>> GetBySaleAsync(int saleId)
    {
        var entities = await _returnRepository.AsQueryable()
            .Include(r => r.Sale)
            .ThenInclude(s => s.Customer)
            .Include(r => r.Customer)
            .Include(r => r.ReturnDetails)
            .ThenInclude(rd => rd.SaleDetail)
            .ThenInclude(sd => sd!.Product)
            .Where(r => r.SaleId == saleId)
            .OrderByDescending(r => r.ReturnId)
            .ToListAsync();

        return entities.Select(MapToResponse);
    }

    private async Task<string> GenerateReturnNumberAsync()
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);

        var countToday = await _returnRepository.CountAsync(r => r.CreatedAt >= today && r.CreatedAt < tomorrow);
        return $"RET-{today:yyyyMMdd}-{(countToday + 1):000}";
    }

    private static ReturnResponse MapToResponse(Return entity)
    {
        return new ReturnResponse
        {
            ReturnId = entity.ReturnId,
            ReturnNumber = entity.ReturnNumber,
            SaleId = entity.SaleId,
            SaleNumber = entity.Sale?.SaleNumber,
            CustomerId = entity.CustomerId,
            CustomerName = entity.Customer is null
                ? (entity.Sale?.Customer is null ? null : $"{entity.Sale.Customer.FirstName} {entity.Sale.Customer.LastName}".Trim())
                : $"{entity.Customer.FirstName} {entity.Customer.LastName}".Trim(),
            Reason = entity.Reason,
            RefundAmount = entity.RefundAmount,
            ReturnStatus = entity.ReturnStatus,
            RequestDate = entity.RequestDate,
            ReturnDetails = entity.ReturnDetails.Select(d => new ReturnDetailResponse
            {
                ReturnDetailId = d.ReturnDetailId,
                ProductId = d.SaleDetail?.ProductId ?? 0,
                ProductName = d.SaleDetail?.Product?.Name,
                QuantityReturned = d.QuantityReturned,
                UnitPrice = d.UnitPrice,
                Subtotal = d.Subtotal
            }).ToList()
        };
    }

    private static string? NormalizeOptional(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}