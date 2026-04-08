using JewelShrinos.Application.DTOs.Request.Sunat;
using JewelShrinos.Core.Entities;
using System.Linq.Expressions;  

namespace JewelShrinos.Application.Interfaces
{

    public interface ISunatService
    {
       Task<bool> ConfigureAsync(ConfigureSunatRequest request);
        Task<InvoiceResponse> EmitInvoiceAsync(int saleId);
        Task<InvoiceResponse> EmitBolletaAsync(int saleId);
        Task<InvoiceStatusResponse> GetInvoiceStatusAsync(string ticketNumber);
        Task<SunatIntegrationResponse?> GetConfigurationAsync();
    }
}