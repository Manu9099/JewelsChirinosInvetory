using JewelShrinos.Application.DTOs.Response.Inventory;

namespace JewelShrinos.Application.Interfaces;

public interface IInventoryService
{
    Task<InventoryResponse?> GetInventoryAsync(int productId);
    Task<IEnumerable<InventoryResponse>> GetLowStockAsync(int threshold = 5);
    Task<IEnumerable<InventoryMovementResponse>> GetMovementsAsync(int productId);

    Task<InventoryResponse> AdjustStockAsync(
        int productId,
        int quantity,
        string movementType,
        int? userId = null,
        string? observations = null);

    Task<InventoryResponse> ReserveAsync(
        int productId,
        int quantity,
        int? userId = null,
        string? observations = null);

    Task<InventoryResponse> ReleaseReserveAsync(
        int productId,
        int quantity,
        int? userId = null,
        string? observations = null);
}