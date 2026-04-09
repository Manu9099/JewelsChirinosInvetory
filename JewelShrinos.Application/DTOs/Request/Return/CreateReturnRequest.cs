  namespace JewelShrinos.Application.DTOs.Request.Return
  {
  public class CreateReturnRequest
    {
        public int SaleId { get; set; }
        public int? CustomerId { get; set; }
        public string Reason { get; set; } = null!;
        public List<ReturnDetailRequest> ReturnDetails { get; set; } = new();
        public string? Observations { get; set; }
        public string? CreatedBy { get; set; }
    }
  }