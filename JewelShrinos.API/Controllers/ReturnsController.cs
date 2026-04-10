using JewelShrinos.Application.DTOs.Request.Return;
using JewelShrinos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JewelShrinos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReturnsController : ControllerBase
{
    private readonly IReturnService _returnService;

    public ReturnsController(IReturnService returnService)
    {
        _returnService = returnService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _returnService.GetByIdAsync(id);
        if (result is null) return NotFound(new { message = "Devolución no encontrada." });

        return Ok(result);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        var result = await _returnService.GetPendingAsync();
        return Ok(result);
    }

    [HttpGet("sale/{saleId:int}")]
    public async Task<IActionResult> GetBySale(int saleId)
    {
        var result = await _returnService.GetBySaleAsync(saleId);
        return Ok(result);
    }

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
