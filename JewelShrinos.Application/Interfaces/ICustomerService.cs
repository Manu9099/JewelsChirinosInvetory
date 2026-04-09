 
 using JewelShrinos.Application.DTOs.Request.Customer;
using JewelShrinos.Application.DTOs.Response.Customer;

namespace JewelShrinos.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerResponse> RegisterAsync(RegisterCustomerRequest request);
    Task<IEnumerable<CustomerResponse>> GetAllAsync();
    Task<CustomerResponse?> GetByIdAsync(int id);
    Task<CustomerResponse?> GetByEmailAsync(string email);
    Task<CustomerResponse> UpdateAsync(int id, UpdateCustomerRequest request);
    Task<bool> DeleteAsync(int id);
}
