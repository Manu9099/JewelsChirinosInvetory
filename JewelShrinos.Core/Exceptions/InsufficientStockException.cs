namespace JewelShrinos.Core.Exceptions
{
    /// <summary>
    /// Excepción cuando no hay stock suficiente para una venta
    /// </summary>
    public class InsufficientStockException : Exception
    {
        public int ProductId { get; set; }
        public int AvailableStock { get; set; }
        public int RequestedQuantity { get; set; }
 
        public InsufficientStockException(int productId, int available, int requested)
            : base($"Stock insuficiente. Disponible: {available}, Solicitado: {requested}")
        {
            ProductId = productId;
            AvailableStock = available;
            RequestedQuantity = requested;
        }
    }
}