using JewelShrinos.Application.DTOs.Request.Product;
using JewelShrinos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JewelShrinos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
            return NotFound(new { message = "Producto no encontrado." });

        return Ok(product);
    }

    [HttpGet("by-code/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var product = await _productService.GetByCodeAsync(code);

        if (product is null)
            return NotFound(new { message = "Producto no encontrado." });

        return Ok(product);
    }

    [HttpGet("by-barcode/{barcode}")]
    public async Task<IActionResult> GetByBarcode(string barcode)
    {
        var product = await _productService.GetByBarcodeAsync(barcode);

        if (product is null)
            return NotFound(new { message = "Producto no encontrado." });

        return Ok(product);
    }

    [HttpGet("by-qrcode/{qrCode}")]
    public async Task<IActionResult> GetByQrCode(string qrCode)
    {
        var product = await _productService.GetByQrCodeAsync(qrCode);

        if (product is null)
            return NotFound(new { message = "Producto no encontrado." });

        return Ok(product);
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        var products = await _productService.GetByCategoryAsync(categoryId);
        return Ok(products);
    }

    [HttpGet("material/{materialId:int}")]
    public async Task<IActionResult> GetByMaterial(int materialId)
    {
        var products = await _productService.GetByMaterialAsync(materialId);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        try
        {
            var created = await _productService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.ProductId }, created);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
    {
        try
        {
            var updated = await _productService.UpdateAsync(id, request);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _productService.DeleteAsync(id);

        if (!deleted)
            return NotFound(new { message = "Producto no encontrado." });

        return NoContent();
    }
}