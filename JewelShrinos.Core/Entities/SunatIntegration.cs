namespace JewelShrinos.Core.Entities
{
    /// <summary>
    /// Configuración de integración con SUNAT para emitir facturas
    /// </summary>
    public class SunatIntegration
    {
        public int SunatId { get; set; }
        public string RucNumber { get; set; } = null!; // RUC de la empresa
        public string UsernameSunat { get; set; } = null!; // Usuario SOL
        public string PasswordSunat { get; set; } = null!; // Contraseña encriptada
        
        public string? CertificatePath { get; set; } // Ruta del certificado digital
        public string? CertificatePassword { get; set; } // Contraseña del certificado
        
        public string? ApiToken { get; set; } // Token de API externa
        public string? ApiBaseUrl { get; set; } // URL base de API
        public string? ApiProvider { get; set; } // SUNAT, FACTURADOR, OTRO
        
        public bool Status { get; set; } = true;
        public DateTime? LastSync { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}