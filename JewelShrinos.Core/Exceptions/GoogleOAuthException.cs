namespace JewelShrinos.Core.Exceptions
{
    /// <summary>
    /// Excepción para errores de Google OAuth
    /// </summary>
    public class GoogleOAuthException : Exception
    {
        public string? GoogleErrorCode { get; set; }
 
        public GoogleOAuthException(string message) : base(message) { }
 
        public GoogleOAuthException(string message, string errorCode) 
            : base(message)
        {
            GoogleErrorCode = errorCode;
        }
    }
}