using JewelShrinos.Core.Entities;
using System.Linq.Expressions;  

namespace JewelShrinos.Core.Interfaces
{
/// <summary>
    /// Servicio de Integración SUNAT
    /// Para emitir facturas y boletas
    /// </summary>
    public interface ISunatService
    {
        Task<bool> ConfigureAsync(ConfigureSunatRequest request);
        Task<InvoiceResponse> EmitInvoiceAsync(int saleId);
        Task<InvoiceResponse> EmitBolletaAsync(int saleId);
        Task<InvoiceStatusResponse> GetInvoiceStatusAsync(string ticketNumber);
        Task<SunatIntegrationResponse?> GetConfigurationAsync();
    }
}