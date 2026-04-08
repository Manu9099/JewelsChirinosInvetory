using JewelShrinos.Core.Entities;
using System.Linq.Expressions;  
  /// <summary>
    /// Servicio de Clientes
    /// Soporte para login local y Google OAuth
    /// </summary>

 
 
    public interface ICustomerService
    {
        Task<CustomerResponse> RegisterAsync(RegisterCustomerRequest request);
        Task<CustomerResponse?> GetByIdAsync(int id);
        Task<CustomerResponse?> GetByEmailAsync(string email);
        Task<bool> UpdateAsync(int id, UpdateCustomerRequest request);
        Task<bool> DeleteAsync(int id);
    }
