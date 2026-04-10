using JewelShrinos.Application.DTOs.Request.Auth;
using JewelShrinos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelShrinos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        try
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize]
    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetById(int userId)
    {
        var result = await _authService.GetByIdAsync(userId);
        if (result is null) return NotFound(new { message = "Usuario no encontrado." });

        return Ok(result);
    }

    [Authorize]
    [HttpPost("{userId:int}/change-password")]
    public async Task<IActionResult> ChangePassword(int userId, [FromBody] ChangePasswordRequest request)
    {
        try
        {
            var ok = await _authService.ChangePasswordAsync(userId, request);
            if (!ok) return NotFound(new { message = "Usuario no encontrado." });

            return Ok(new { message = "Contraseña actualizada correctamente." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost("{userId:int}/disable")]
    public async Task<IActionResult> Disable(int userId)
    {
        var ok = await _authService.DisableAsync(userId);
        if (!ok) return NotFound(new { message = "Usuario no encontrado." });

        return Ok(new { message = "Usuario desactivado correctamente." });
    }
}