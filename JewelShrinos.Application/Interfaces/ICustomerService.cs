
using JewelShrinos.Application.DTOs.Request.Customer;
using JewelShrinos.Application.DTOs.Response.Customer;


 
 
    public interface ICustomerService
    {
        Task<CustomerResponse> RegisterAsync(RegisterCustomerRequest request);
        Task<CustomerResponse?> GetByIdAsync(int id);
        Task<CustomerResponse?> GetByEmailAsync(string email);
        Task<bool> UpdateAsync(int id, UpdateCustomerRequest request);
        Task<bool> DeleteAsync(int id);
    }
