using JewelShrinos.Application.DTOs.Request.Customer;
using JewelShrinos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace JewelShrinos.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
   
    [Authorize(Roles = "ADMIN,SELLER")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllAsync();
        return Ok(customers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetByIdAsync(id);
        if (customer is null) return NotFound(new { message = "Cliente no encontrado." });

        return Ok(customer);
    }


   [Authorize(Roles = "ADMIN,SELLER")]
    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var customer = await _customerService.GetByEmailAsync(email);
        if (customer is null) return NotFound(new { message = "Cliente no encontrado." });

        return Ok(customer);
    } 

   [Authorize(Roles = "ADMIN,SELLER")] 
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterCustomerRequest request)
    {
        try
        {
            var created = await _customerService.RegisterAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.CustomerId }, created);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
   
   [Authorize(Roles = "ADMIN,SELLER")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerRequest request)
    {
        try
        {
            var updated = await _customerService.UpdateAsync(id, request);
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
        var deleted = await _customerService.DeleteAsync(id);

        if (!deleted)
            return NotFound(new { message = "Cliente no encontrado." });

        return Ok(new { message = "Cliente desactivado correctamente." });
    }
}