   namespace JewelShrinos.Application.DTOs.Response.Sunat;
   public class InvoiceResponse
    {
        public bool Success { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? TicketNumber { get; set; }
        public string? InvoiceUrl { get; set; }
        public string? Message { get; set; }
    }