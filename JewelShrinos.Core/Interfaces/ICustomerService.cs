using JewelShrinos.Core.Entities;
using System.Linq.Expressions;  
  /// <summary>
    /// Servicio de Clientes
    /// Soporte para login local y Google OAuth
    /// </summary>
   
namespace JewelShrinos.Core.Interfaces
{
     public class CustomerResponse
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? RucDni { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
       public class RegisterCustomerRequest
    {
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? RucDni { get; set; }
    }
       public class UpdateCustomerRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? RucDni { get; set; }
    }
    public interface ICustomerService
    {
        Task<CustomerResponse> RegisterAsync(RegisterCustomerRequest request);
        Task<CustomerResponse?> GetByIdAsync(int id);
        Task<CustomerResponse?> GetByEmailAsync(string email);
        Task<bool> UpdateAsync(int id, UpdateCustomerRequest request);
        Task<bool> DeleteAsync(int id);
    }
}