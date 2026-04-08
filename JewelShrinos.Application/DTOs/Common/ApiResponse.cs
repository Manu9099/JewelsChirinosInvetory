namespace JewelShrinos.Application.DTOs.Common;
 
/// <summary>
/// Envoltorio estándar para todas las respuestas de la API.
/// Usado en todos los controllers.
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public T? Data { get; init; }
    public List<string>? Errors { get; init; }
 
    // Factory methods — evitan instanciar con object initializers
    public static ApiResponse<T> Ok(T data, string? message = null) =>
        new() { Success = true, Data = data, Message = message };
 
    public static ApiResponse<T> Fail(string message, List<string>? errors = null) =>
        new() { Success = false, Message = message, Errors = errors };
}
 