 
namespace JewelShrinos.Core.Enums
{
    public enum SunatStatus
    {
        Pending = 1,        // Pendiente de envío
        Sent = 2,           // Enviado a SUNAT
        Approved = 3,       // Aprobado por SUNAT
        Rejected = 4,       // Rechazado por SUNAT
        Error = 5           // Error en envío
    }
}