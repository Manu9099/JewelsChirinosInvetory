    public class InvoiceStatusResponse
    {
        public string Status { get; set; } = null!; // ACEPTADA, RECHAZADA, PROCESANDO
        public string? InvoiceNumber { get; set; }
        public string? ErrorMessage { get; set; }
    }