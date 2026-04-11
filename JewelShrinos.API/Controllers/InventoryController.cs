using JewelShrinos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JewelShrinos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

   [Authorize(Roles = "ADMIN,WAREHOUSE,SELLER")]
    [HttpGet("{productId:int}")]
    public async Task<IActionResult> GetByProduct(int productId)
    {
        var result = await _inventoryService.GetInventoryAsync(productId);

        if (result is null)
            return NotFound(new { message = "Inventario no encontrado para ese producto." });

        return Ok(result);
    }

   [Authorize(Roles = "ADMIN,WAREHOUSE,SELLER")]
    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock([FromQuery] int threshold = 5)
    {
        try
        {
            var result = await _inventoryService.GetLowStockAsync(threshold);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
  
    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpGet("{productId:int}/movements")]
    public async Task<IActionResult> GetMovements(int productId)
    {
        var result = await _inventoryService.GetMovementsAsync(productId);
        return Ok(result);
    }

    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpPost("{productId:int}/adjust")]
    public async Task<IActionResult> Adjust(
        int productId,
        [FromQuery] int quantity,
        [FromQuery] string movementType,
        [FromQuery] int? userId,
        [FromQuery] string? observations)
    {
        try
        {
            var result = await _inventoryService.AdjustStockAsync(
                productId,
                quantity,
                movementType,
                userId,
                observations);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "ADMIN,WAREHOUSE,SELLER")]
    [HttpPost("{productId:int}/reserve")]
    public async Task<IActionResult> Reserve(
        int productId,
        [FromQuery] int quantity,
        [FromQuery] int? userId,
        [FromQuery] string? observations)
    {
        try
        {
            var result = await _inventoryService.ReserveAsync(
                productId,
                quantity,
                userId,
                observations);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

   [Authorize(Roles = "ADMIN,WAREHOUSE,SELLER")]
    [HttpPost("{productId:int}/release-reserve")]
    public async Task<IActionResult> ReleaseReserve(
        int productId,
        [FromQuery] int quantity,
        [FromQuery] int? userId,
        [FromQuery] string? observations)
    {
        try
        {
            var result = await _inventoryService.ReleaseReserveAsync(
                productId,
                quantity,
                userId,
                observations);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}