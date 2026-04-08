// JewelShrinos.Application/Interfaces/IProductService.cs
using JewelShrinos.Application.DTOs.Request.Product;
using JewelShrinos.Application.DTOs.Response.Product;

namespace JewelShrinos.Application.Interfaces;

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