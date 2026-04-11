namespace JewelShrinos.Application.DTOs.Response.Auth;

public class CurrentUserResponse
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Role { get; set; } = null!;
}