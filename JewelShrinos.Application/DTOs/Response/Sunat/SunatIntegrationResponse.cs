  public class SunatIntegrationResponse
    {
        public string RucNumber { get; set; } = null!;
        public string? ApiProvider { get; set; }
        public bool Status { get; set; }
        public DateTime? LastSync { get; set; }
    }