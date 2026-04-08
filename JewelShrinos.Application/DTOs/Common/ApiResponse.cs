namespace JewelShrinos.Application.DTOs.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? ErrorCode { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }
 
        public static ApiResponse<T> Ok(T? data, string message = "Operación exitosa")
            => new() { Success = true, Data = data, Message = message };
 
        public static ApiResponse<T> Error(string message, string? errorCode = null, Dictionary<string, string[]>? errors = null)
            => new() { Success = false, Message = message, ErrorCode = errorCode, Errors = errors };
    }
}
 