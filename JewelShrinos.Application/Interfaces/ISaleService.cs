 using JewelShrinos.Core.Entities;
using System.Linq.Expressions;
 
 /// <summary>
    /// Servicio de Ventas 🔥 CRÍTICO
    /// Aquí se descarga el inventario automáticamente
    /// </summary>
    public interface ISaleService
    {
        /// <summary>
        /// Crear venta - 🔥 INVENTARIO SE DESCARGA AUTOMÁTICAMENTE
        /// </summary>
        Task<SaleResponse> CreateSaleAsync(CreateSaleRequest request);
        
        /// <summary>
        /// Anular venta - Stock se restaura
        /// </summary>
        Task<bool> CancelSaleAsync(int saleId);
        
        Task<SaleResponse?> GetByIdAsync(int id);
        Task<IEnumerable<SaleResponse>> GetAllAsync();
        Task<SaleResponse?> GetBySaleNumberAsync(string saleNumber);
        Task<IEnumerable<SaleResponse>> GetByCustomerAsync(int customerId);
        Task<IEnumerable<SaleResponse>> GetByDateAsync(DateTime date);
    }
