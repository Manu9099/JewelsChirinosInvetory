using JewelShrinos.Application.DTOs.Request.Return;
using JewelShrinos.Application.DTOs.Response.Return;

namespace JewelShrinos.Application.Interfaces;

public interface IReturnService
{
    Task<ReturnResponse> CreateReturnAsync(CreateReturnRequest request);
    Task<bool> ApproveReturnAsync(int returnId);
    Task<bool> RejectReturnAsync(int returnId);
    Task<ReturnResponse?> GetByIdAsync(int id);
    Task<IEnumerable<ReturnResponse>> GetPendingAsync();
    Task<IEnumerable<ReturnResponse>> GetBySaleAsync(int saleId);
}