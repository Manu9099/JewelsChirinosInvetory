using JewelShrinos.Application.DTOs.Request.Return;
using JewelShrinos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JewelShrinos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReturnsController : ControllerBase
{
    private readonly IReturnService _returnService;

    public ReturnsController(IReturnService returnService)
    {
        _returnService = returnService;
    }

    [Authorize(Roles = "ADMIN,SELLER,WAREHOUSE")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _returnService.GetByIdAsync(id);
        if (result is null) return NotFound(new { message = "Devolución no encontrada." });

        return Ok(result);
    }

    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        var result = await _returnService.GetPendingAsync();
        return Ok(result);
    }

    [Authorize(Roles = "ADMIN,WAREHOUSE,SELLER")]

    [HttpGet("sale/{saleId:int}")]
    public async Task<IActionResult> GetBySale(int saleId)
    {
        var result = await _returnService.GetBySaleAsync(saleId);
        return Ok(result);
    }

    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReturnRequest request)
    {
        try
        {
            var created = await _returnService.CreateReturnAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.ReturnId }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
   
    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpPost("{returnId:int}/approve")]
    public async Task<IActionResult> Approve(int returnId)
    {
        try
        {
            var ok = await _returnService.ApproveReturnAsync(returnId);
            if (!ok) return NotFound(new { message = "Devolución no encontrada." });

            return Ok(new { message = "Devolución aprobada correctamente." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
   
    [Authorize(Roles = "ADMIN,WAREHOUSE")]
    [HttpPost("{returnId:int}/reject")]
    public async Task<IActionResult> Reject(int returnId)
    {
        try
        {
            var ok = await _returnService.RejectReturnAsync(returnId);
            if (!ok) return NotFound(new { message = "Devolución no encontrada." });

            return Ok(new { message = "Devolución rechazada correctamente." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
