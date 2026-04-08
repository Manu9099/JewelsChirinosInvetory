namespace JewelShrinos.Core.Constants
{
    public static class ErrorMessages
    {
        // Stock errors
        public const string INSUFFICIENT_STOCK = "Stock insuficiente para la venta";
        public const string PRODUCT_NOT_FOUND = "Producto no encontrado";
 
        // Sales errors
        public const string SALE_NOT_FOUND = "Venta no encontrada";
        public const string SALE_ALREADY_CANCELLED = "La venta ya ha sido anulada";
        public const string INVALID_SALE_STATUS = "Estado de venta inválido";
 
        // Customer errors
        public const string CUSTOMER_NOT_FOUND = "Cliente no encontrado";
        public const string EMAIL_ALREADY_EXISTS = "El correo electrónico ya está registrado";
        public const string INVALID_EMAIL = "Correo electrónico inválido";
 
        // Authentication errors
        public const string INVALID_CREDENTIALS = "Credenciales inválidas";
        public const string INVALID_TOKEN = "Token inválido o expirado";
        public const string USER_NOT_FOUND = "Usuario no encontrado";
 
        // SUNAT errors
        public const string SUNAT_CONNECTION_ERROR = "Error de conexión con SUNAT";
        public const string SUNAT_INVALID_RUC = "RUC inválido";
        public const string SUNAT_INVALID_DOCUMENT_DATA = "Datos del documento inválidos para SUNAT";
        public const string SUNAT_INVOICE_GENERATION_ERROR = "Error al generar factura en SUNAT";
 
        // Google OAuth errors
        public const string INVALID_GOOGLE_TOKEN = "Token de Google inválido";
        public const string GOOGLE_ACCOUNT_ALREADY_LINKED = "La cuenta de Google ya está vinculada";
        public const string GOOGLE_OAUTH_ERROR = "Error en autenticación con Google";
 
        // Purchase errors
        public const string PURCHASE_NOT_FOUND = "Compra no encontrada";
        public const string INVALID_PURCHASE_STATUS = "Estado de compra inválido";
 
        // Return errors
        public const string RETURN_NOT_FOUND = "Devolución no encontrada";
        public const string INVALID_RETURN_STATUS = "Estado de devolución inválido";
    }
}