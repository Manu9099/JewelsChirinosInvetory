namespace JewelShrinos.Core.Enums
{
    /// <summary>
    /// Tipos de movimientos de inventario
    /// </summary>
    public enum MovementType
    {
        Entry = 1,          // Entrada de compra
        Sale = 2,           // Salida por venta
        Adjustment = 3,     // Ajuste manual
        Damaged = 4,        // Stock dañado
        Reserve = 5,        // Reserva de cliente
        Return = 6          // Devolución
    }
}