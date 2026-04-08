 using JewelShrinos.Core.Entities;
using System.Linq.Expressions;

namespace JewelShrinos.Core.Interfaces
{
   /// <summary>
    /// Servicio de Email
    /// </summary>
    public interface IEmailService
    {
        Task SendInvoiceAsync(string email, byte[] invoicePdf);
        Task SendBolletaAsync(string email, byte[] bolletaPdf);
        Task SendReceiptAsync(string email, string receiptNumber);
        Task SendPasswordResetAsync(string email, string resetLink);
    }
 
    /// <summary>
    /// Generador de Códigos QR y Códigos de Barras
    /// </summary>
    public interface IBarcodeQrService
    {
        byte[] GenerateQrCode(string data);
        byte[] GenerateBarcode(string code);
        Task<string> SaveQrCodeAsync(string data, string fileName);
        Task<string> SaveBarcodeAsync(string code, string fileName);
    }
}