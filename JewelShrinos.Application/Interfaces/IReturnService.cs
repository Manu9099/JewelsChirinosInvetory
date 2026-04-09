

namespace JewelShrinos.Application.Interfaces;

    using JewelShrinos.Application.DTOs.Request.Return;
    using JewelShrinos.Application.DTOs.Response.Return;
 

  
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
