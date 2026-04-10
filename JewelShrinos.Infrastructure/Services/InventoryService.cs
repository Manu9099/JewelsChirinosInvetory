using JewelShrinos.Application.DTOs.Response.Inventory;
using JewelShrinos.Application.Interfaces;
using JewelShrinos.Core.Entities;
using JewelShrinos.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelShrinos.Infrastructure.Services;

public class InventoryService : IInventoryService
{
    private readonly IRepository<Inventory> _inventoryRepository;
    private readonly IRepository<InventoryMovement> _inventoryMovementRepository;

    public InventoryService(
        IRepository<Inventory> inventoryRepository,
        IRepository<InventoryMovement> inventoryMovementRepository)
    {
        _inventoryRepository = inventoryRepository;
        _inventoryMovementRepository = inventoryMovementRepository;
    }

    public async Task<InventoryResponse?> GetInventoryAsync(int productId)
    {
        var inventory = await _inventoryRepository.AsQueryable()
            .Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.ProductId == productId);

        return inventory is null ? null : MapInventory(inventory);
    }

    public async Task<IEnumerable<InventoryResponse>> GetLowStockAsync(int threshold = 5)
    {
        if (threshold < 0)
            throw new InvalidOperationException("El threshold no puede ser negativo.");

        var items = await _inventoryRepository.AsQueryable()
            .Include(i => i.Product)
            .Where(i => i.AvailableStock <= threshold)
            .OrderBy(i => i.AvailableStock)
            .ThenBy(i => i.Product!.Name)
            .ToListAsync();

        return items.Select(MapInventory);
    }

    public async Task<IEnumerable<InventoryMovementResponse>> GetMovementsAsync(int productId)
    {
        var movements = await _inventoryMovementRepository.AsQueryable()
            .Include(m => m.Product)
            .Where(m => m.ProductId == productId)
            .OrderByDescending(m => m.MovementDate)
            .ThenByDescending(m => m.InventoryMovementId)
            .ToListAsync();

        return movements.Select(MapMovement);
    }

    public async Task<InventoryResponse> AdjustStockAsync(
        int productId,
        int quantity,
        string movementType,
        int? userId = null,
        string? observations = null)
    {
        if (productId <= 0)
            throw new InvalidOperationException("El productId es obligatorio.");

        if (quantity <= 0)
            throw new InvalidOperationException("La cantidad debe ser mayor a 0.");

        if (string.IsNullOrWhiteSpace(movementType))
            throw new InvalidOperationException("El movementType es obligatorio.");

        var normalizedType = movementType.Trim().ToUpperInvariant();

        var inventory = await _inventoryRepository.AsQueryable()
            .Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.ProductId == productId)
            ?? throw new InvalidOperationException("No existe inventario para ese producto.");

        var stockBefore = inventory.AvailableStock;
        int stockAfter;

        switch (normalizedType)
        {
            case "ADJUSTMENT_IN":
                stockAfter = stockBefore + quantity;
                inventory.AvailableStock = stockAfter;
                break;

            case "ADJUSTMENT_OUT":
            case "LOSS_OUT":
                if (stockBefore < quantity)
                    throw new InvalidOperationException("No hay stock suficiente para realizar la salida.");

                stockAfter = stockBefore - quantity;
                inventory.AvailableStock = stockAfter;
                break;

            case "DAMAGED_OUT":
                if (stockBefore < quantity)
                    throw new InvalidOperationException("No hay stock suficiente para marcar como dañado.");

                stockAfter = stockBefore - quantity;
                inventory.AvailableStock = stockAfter;
                inventory.DamagedStock += quantity;
                break;

            default:
                throw new InvalidOperationException("MovementType no válido.");
        }

        inventory.UpdatedAt = DateTime.UtcNow;

        var movement = new InventoryMovement
        {
            ProductId = productId,
            MovementType = normalizedType,
            Quantity = quantity,
            StockBefore = stockBefore,
            StockAfter = stockAfter,
            ReferenceType = "MANUAL",
            ReferenceId = null,
            UserId = userId,
            Observations = NormalizeOptional(observations),
            MovementDate = DateTime.UtcNow
        };

        await _inventoryMovementRepository.AddAsync(movement);
        await _inventoryRepository.SaveChangesAsync();

        return MapInventory(inventory);
    }

    public async Task<InventoryResponse> ReserveAsync(
        int productId,
        int quantity,
        int? userId = null,
        string? observations = null)
    {
        if (productId <= 0)
            throw new InvalidOperationException("El productId es obligatorio.");

        if (quantity <= 0)
            throw new InvalidOperationException("La cantidad debe ser mayor a 0.");

        var inventory = await _inventoryRepository.AsQueryable()
            .Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.ProductId == productId)
            ?? throw new InvalidOperationException("No existe inventario para ese producto.");

        if (inventory.AvailableStock < quantity)
            throw new InvalidOperationException("No hay stock suficiente para reservar.");

        var stockBefore = inventory.AvailableStock;
        inventory.AvailableStock -= quantity;
        inventory.ReservedStock += quantity;
        inventory.UpdatedAt = DateTime.UtcNow;

        var movement = new InventoryMovement
        {
            ProductId = productId,
            MovementType = "RESERVE",
            Quantity = quantity,
            StockBefore = stockBefore,
            StockAfter = inventory.AvailableStock,
            ReferenceType = "MANUAL",
            UserId = userId,
            Observations = NormalizeOptional(observations),
            MovementDate = DateTime.UtcNow
        };

        await _inventoryMovementRepository.AddAsync(movement);
        await _inventoryRepository.SaveChangesAsync();

        return MapInventory(inventory);
    }

    public async Task<InventoryResponse> ReleaseReserveAsync(
        int productId,
        int quantity,
        int? userId = null,
        string? observations = null)
    {
        if (productId <= 0)
            throw new InvalidOperationException("El productId es obligatorio.");

        if (quantity <= 0)
            throw new InvalidOperationException("La cantidad debe ser mayor a 0.");

        var inventory = await _inventoryRepository.AsQueryable()
            .Include(i => i.Product)
            .FirstOrDefaultAsync(i => i.ProductId == productId)
            ?? throw new InvalidOperationException("No existe inventario para ese producto.");

        if (inventory.ReservedStock < quantity)
            throw new InvalidOperationException("No hay stock reservado suficiente para liberar.");

        var stockBefore = inventory.AvailableStock;
        inventory.ReservedStock -= quantity;
        inventory.AvailableStock += quantity;
        inventory.UpdatedAt = DateTime.UtcNow;

        var movement = new InventoryMovement
        {
            ProductId = productId,
            MovementType = "RELEASE_RESERVE",
            Quantity = quantity,
            StockBefore = stockBefore,
            StockAfter = inventory.AvailableStock,
            ReferenceType = "MANUAL",
            UserId = userId,
            Observations = NormalizeOptional(observations),
            MovementDate = DateTime.UtcNow
        };

        await _inventoryMovementRepository.AddAsync(movement);
        await _inventoryRepository.SaveChangesAsync();

        return MapInventory(inventory);
    }

    private static InventoryResponse MapInventory(Inventory inventory)
    {
        return new InventoryResponse
        {
            InventoryId = inventory.InventoryId,
            ProductId = inventory.ProductId,
            ProductName = inventory.Product?.Name,
            AvailableStock = inventory.AvailableStock,
            ReservedStock = inventory.ReservedStock,
            SoldStock = inventory.SoldStock,
            DamagedStock = inventory.DamagedStock,
            TotalStock = inventory.TotalStock,
            LastSaleDate = inventory.LastSaleDate,
            LastPurchaseDate = inventory.LastPurchaseDate
        };
    }

    private static InventoryMovementResponse MapMovement(InventoryMovement movement)
    {
        return new InventoryMovementResponse
        {
            MovementId = movement.InventoryMovementId,
            ProductId = movement.ProductId,
            ProductName = movement.Product?.Name,
            MovementType = movement.MovementType,
            Quantity = movement.Quantity,
            StockBefore = movement.StockBefore,
            StockAfter = movement.StockAfter,
            ReferenceType = movement.ReferenceType,
            ReferenceId = movement.ReferenceId,
            Observations = movement.Observations,
            MovementDate = movement.MovementDate
        };
    }

    private static string? NormalizeOptional(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}