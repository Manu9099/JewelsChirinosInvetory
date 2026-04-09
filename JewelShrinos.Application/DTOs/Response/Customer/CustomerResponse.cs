namespace JewelShrinos.Application.DTOs.Response.Customer;
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
