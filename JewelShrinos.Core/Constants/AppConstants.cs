namespace JewelShrinos.Core.Constants
{
    public static class AppConstants
    {
        // IGV (Impuesto General a las Ventas) - Perú
        public const decimal IGV_PERCENTAGE = 0.18m;
 
        // Minimum stock alerts
        public const int MIN_STOCK_ALERT = 5;
 
        // Code formats
        public const string SALE_NUMBER_FORMAT = "V-{0:yyyyMMdd}-{1:D3}";
        public const string PURCHASE_NUMBER_FORMAT = "C-{0:yyyyMMdd}-{1:D3}";
        public const string RETURN_NUMBER_FORMAT = "R-{0:yyyyMMdd}-{1:D3}";
 
        // JWT
        public const int JWT_EXPIRATION_MINUTES = 60;
 
        // SUNAT
        public const string SUNAT_API_BASE_URL = "https://api.sunat.gob.pe";
        
        // Google OAuth
        public const string GOOGLE_OAUTH_SCOPE = "openid profile email";
    }
}