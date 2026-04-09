using JewelShrinos.Core.Entities;
using System.Linq.Expressions;

/// <summary>
    /// Servicio de Inventario
    /// Control de stock
    /// </summary>
 
using JewelShrinos.Application.DTOs.Response.Inventory;

        public interface IInventoryService
    {
        Task<InventoryResponse?> GetInventoryAsync(int productId);
        Task<IEnumerable<InventoryResponse>> GetLowStockAsync(int threshold = 5);
        Task<int> AdjustStockAsync(int productId, int quantity, string movementType, int? userId = null, string? observations = null);
        Task<bool> ReserveAsync(int productId, int quantity, int? userId = null);
        Task<bool> ReleaseReserveAsync(int productId, int quantity, int? userId = null);
        Task<IEnumerable<InventoryMovementResponse>> GetMovementsAsync(int productId);
    }
