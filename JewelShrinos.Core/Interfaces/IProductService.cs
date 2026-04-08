using JewelShrinos.Core.Entities;
using System.Linq.Expressions;
using JewelShrinos.Application.DTOs.Request.Product;

namespace JewelShrinos.Core.Interfaces
{
    /// <summary>
    /// Servicio de Productos
    /// </summary>
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string Code { get; set; } = null!;
        public string? Barcode { get; set; }
        public string? QrCode { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal MarginPercentage { get; set; }
        public decimal? Weight { get; set; }
        public bool Status { get; set; }
    }
 

    
    public interface IProductService
    {
        Task<ProductResponse?> GetByIdAsync(int id);
        Task<IEnumerable<ProductResponse>> GetAllAsync();
        Task<ProductResponse?> GetByCodeAsync(string code);
        Task<ProductResponse?> GetByBarcodeAsync(string barcode);
        Task<ProductResponse?> GetByQrCodeAsync(string qrCode);
        Task<IEnumerable<ProductResponse>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductResponse>> GetByMaterialAsync(int materialId);
        
        Task<ProductResponse> CreateAsync(CreateProductRequest request);
        Task<ProductResponse> UpdateAsync(int id, UpdateProductRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
