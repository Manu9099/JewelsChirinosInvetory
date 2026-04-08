namespace JewelShrinos.Application.DTOs.Request.Sunat;

public class ConfigureSunatRequest
{
    public string Ruc { get; set; } = null!;
    public string UsuarioSol { get; set; } = null!;
    public string ClaveSol { get; set; } = null!;
    public string CertificadoDigital { get; set; } = null!;
    public string? RutaCertificado { get; set; }
    public bool Produccion { get; set; }
}