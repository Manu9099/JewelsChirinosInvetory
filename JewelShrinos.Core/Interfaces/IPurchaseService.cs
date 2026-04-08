
 using JewelShrinos.Core.Entities;
using System.Linq.Expressions;


 
namespace JewelShrinos.Core.Interfaces
{

  public class PurchaseResponse
    {
        public int PurchaseId { get; set; }
        public string PurchaseNumber { get; set; } = null!;
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string PurchaseStatus { get; set; } = null!;
        public List<PurchaseDetailResponse> PurchaseDetails { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
      public class PurchaseDetailResponse
    {
        public int PurchaseDetailId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
        public int QuantityReceived { get; set; }
        public int QuantityPending => Quantity - QuantityReceived;
    }
 
    public class ReceivePurchaseRequest
    {
        public List<ReceiveDetailRequest> Details { get; set; } = new();
        public string? Observations { get; set; }
    }
    
    public class ReceiveDetailRequest
    {
        public int PurchaseDetailId { get; set; }
        public int QuantityReceived { get; set; }
    }
 
 public interface IPurchaseService
    {
        Task<PurchaseResponse> CreatePurchaseAsync(CreatePurchaseRequest request);
        
        /// <summary>
        /// Recibir mercadería - 🔥 INVENTARIO SE CARGA AUTOMÁTICAMENTE
        /// </summary>
        Task<PurchaseResponse> ReceivePurchaseAsync(int purchaseId, ReceivePurchaseRequest request);
        
        Task<bool> CancelPurchaseAsync(int purchaseId);
        Task<PurchaseResponse?> GetByIdAsync(int id);
        Task<IEnumerable<PurchaseResponse>> GetAllAsync();
        Task<PurchaseResponse?> GetByPurchaseNumberAsync(string purchaseNumber);
        Task<IEnumerable<PurchaseResponse>> GetBySupplierAsync(int supplierId);
    }
}