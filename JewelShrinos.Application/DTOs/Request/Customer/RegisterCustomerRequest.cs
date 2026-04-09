  namespace JewelShrinos.Application.DTOs.Request.Customer;
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