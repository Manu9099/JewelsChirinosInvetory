 using JewelShrinos.Core.Entities;
using System.Linq.Expressions;
 
 /// <summary>
    /// Servicio de Ventas 🔥 CRÍTICO
    /// Aquí se descarga el inventario automáticamente
    /// </summary>
 
namespace JewelShrinos.Core.Interfaces
{

      public class SaleResponse
    {
        public int SaleId { get; set; }
        public string SaleNumber { get; set; } = null!;
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public string SaleStatus { get; set; } = null!;
        public string? InvoiceNumber { get; set; }
        public string? SunatTicketNumber { get; set; }
        public List<SaleDetailResponse> SaleDetails { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
      public class SaleDetailResponse
    {
        public int SaleDetailId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public decimal LineDiscount { get; set; }
        public string DetailStatus { get; set; } = null!;
    }
    public class CreateSaleRequest
    {
        public int? CustomerId { get; set; }
        public List<SaleDetailRequest> SaleDetails { get; set; } = new();
        public decimal? DiscountAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Observations { get; set; }
        public string? CreatedBy { get; set; }
    }
public class SaleDetailRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? LineDiscount { get; set; }
    }

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
}
    