namespace JewelShrinos.Application.DTOs.Request.Sale;

public class SaleDetailRequest
{
public int ProductId { get; set; }
    public int Quantity { get; set; }

    // Si viene null o <= 0, se usará Product.SellingPrice
    public decimal? UnitPrice { get; set; }

    // Descuento total de la línea
    public decimal LineDiscount { get; set; } = 0;
}