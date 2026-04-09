using JewelShrinos.Application.DTOs.Request.Sale;
using JewelShrinos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JewelShrinos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _saleService.GetAllAsync();
        return Ok(sales);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var sale = await _saleService.GetByIdAsync(id);
        if (sale is null) return NotFound(new { message = "Venta no encontrada." });

        return Ok(sale);
    }

    [HttpGet("by-number/{saleNumber}")]
    public async Task<IActionResult> GetBySaleNumber(string saleNumber)
    {
        var sale = await _saleService.GetBySaleNumberAsync(saleNumber);
        if (sale is null) return NotFound(new { message = "Venta no encontrada." });

        return Ok(sale);
    }

    [HttpGet("customer/{customerId:int}")]
    public async Task<IActionResult> GetByCustomer(int customerId)
    {
        var sales = await _saleService.GetByCustomerAsync(customerId);
        return Ok(sales);
    }

    

    [HttpGet("date/{date}")]
    public async Task<IActionResult> GetByDate(DateTime date)
    {
        var sales = await _saleService.GetByDateAsync(date);
        return Ok(sales);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleRequest request)
    {
        try
        {
            var created = await _saleService.CreateSaleAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.SaleId }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{saleId:int}/cancel")]
    public async Task<IActionResult> Cancel(int saleId)
    {
        try
        {
            var cancelled = await _saleService.CancelSaleAsync(saleId);

            if (!cancelled)
                return NotFound(new { message = "Venta no encontrada." });

            return Ok(new { message = "Venta cancelada correctamente." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}