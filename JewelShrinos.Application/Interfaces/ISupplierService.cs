using JewelShrinos.Application.DTOs.Request.Supplier;
using JewelShrinos.Application.DTOs.Response.Supplier;

namespace JewelShrinos.Application.Interfaces;

public interface ISupplierService
{
    Task<SupplierResponse?> GetByIdAsync(int id);
    Task<IEnumerable<SupplierResponse>> GetAllAsync();
    Task<SupplierResponse> CreateAsync(CreateSupplierRequest request);
    Task<SupplierResponse> UpdateAsync(int id, UpdateSupplierRequest request);
    Task<bool> DeleteAsync(int id);
}