using JewelShrinos.Application.DTOs.Request.Supplier;
using JewelShrinos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelShrinos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SuppliersController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var suppliers = await _supplierService.GetAllAsync();
        return Ok(suppliers);
    }

    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var supplier = await _supplierService.GetByIdAsync(id);
        if (supplier is null)
            return NotFound(new { message = "Proveedor no encontrado." });

        return Ok(supplier);
    }

    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request)
    {
        try
        {
            var created = await _supplierService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.SupplierId }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplierRequest request)
    {
        try
        {
            var updated = await _supplierService.UpdateAsync(id, request);
            return Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "ADMIN")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _supplierService.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = "Proveedor no encontrado." });

        return Ok(new { message = "Proveedor desactivado correctamente." });
    }
}