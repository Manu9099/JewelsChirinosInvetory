  using JewelShrinos.Core.Entities;
using System.Linq.Expressions;
  
  // <summary>
    /// Servicio de Devoluciones 🔥 CRÍTICO
    /// Aquí se restaura el inventario al aprobar
    /// </summary>
     
namespace JewelShrinos.Core.Interfaces
{
   public class ReturnResponse
    {
        public int ReturnId { get; set; }
        public string ReturnNumber { get; set; } = null!;
        public int SaleId { get; set; }
        public string? SaleNumber { get; set; }
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string Reason { get; set; } = null!;
        public decimal? RefundAmount { get; set; }
        public string ReturnStatus { get; set; } = null!;
        public List<ReturnDetailResponse> ReturnDetails { get; set; } = new();
        public DateTime RequestDate { get; set; }
    }
 public class ReturnDetailResponse
    {
        public int ReturnDetailId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int QuantityReturned { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
    }

       public class CreatePurchaseRequest
    {
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<PurchaseDetailRequest> PurchaseDetails { get; set; } = new();
        public string? Observations { get; set; }
        public string? CreatedBy { get; set; }
    }
 
    public class PurchaseDetailRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
    
   public class CreateReturnRequest
    {
        public int SaleId { get; set; }
        public int? CustomerId { get; set; }
        public string Reason { get; set; } = null!;
        public List<ReturnDetailRequest> ReturnDetails { get; set; } = new();
        public string? Observations { get; set; }
        public string? CreatedBy { get; set; }
    }
      public class ReturnDetailRequest
    {
        public int SaleDetailId { get; set; }
        public int QuantityReturned { get; set; }
    }
  
    public interface IReturnService
    {

      
        Task<ReturnResponse> CreateReturnAsync(CreateReturnRequest request);
        
        /// <summary>
        /// Aprobar devolución - 🔥 INVENTARIO SE RESTAURA AUTOMÁTICAMENTE
        /// </summary>
        Task<ReturnResponse> ApproveReturnAsync(int returnId);
        
        Task<bool> RejectReturnAsync(int returnId);
        Task<ReturnResponse?> GetByIdAsync(int id);
        Task<IEnumerable<ReturnResponse>> GetPendingAsync();
        Task<IEnumerable<ReturnResponse>> GetBySaleAsync(int saleId);
    }
}