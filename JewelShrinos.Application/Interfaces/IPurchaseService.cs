
 using JewelShrinos.Core.Entities;
using System.Linq.Expressions;


 


  
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
