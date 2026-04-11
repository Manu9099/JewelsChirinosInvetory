using JewelShrinos.Application.DTOs.Request.Purchase;
using JewelShrinos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JewelShrinos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PurchasesController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchasesController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var purchases = await _purchaseService.GetAllAsync();
        return Ok(purchases);
    }

   [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var purchase = await _purchaseService.GetByIdAsync(id);
        if (purchase is null)
            return NotFound(new { message = "Compra no encontrada." });

        return Ok(purchase);
    }

   [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpGet("by-number/{purchaseNumber}")]
    public async Task<IActionResult> GetByPurchaseNumber(string purchaseNumber)
    {
        var purchase = await _purchaseService.GetByPurchaseNumberAsync(purchaseNumber);
        if (purchase is null)
            return NotFound(new { message = "Compra no encontrada." });

        return Ok(purchase);
    }


      [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpGet("supplier/{supplierId:int}")]
    public async Task<IActionResult> GetBySupplier(int supplierId)
    {
        var purchases = await _purchaseService.GetBySupplierAsync(supplierId);
        return Ok(purchases);
    }


   [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseRequest request)
    {
        try
        {
            var created = await _purchaseService.CreatePurchaseAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.PurchaseId }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


      [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpPost("{purchaseId:int}/receive")]
    public async Task<IActionResult> Receive(int purchaseId, [FromBody] ReceivePurchaseRequest request)
    {
        try
        {
            var updated = await _purchaseService.ReceivePurchaseAsync(purchaseId, request);
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


      [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpPost("{purchaseId:int}/cancel")]
    public async Task<IActionResult> Cancel(int purchaseId)
    {
        try
        {
            var cancelled = await _purchaseService.CancelPurchaseAsync(purchaseId);
            if (!cancelled)
                return NotFound(new { message = "Compra no encontrada." });

            return Ok(new { message = "Compra cancelada correctamente." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}