namespace JewelShrinos.Core.Exceptions
{
    /// <summary>
    /// Excepción para errores de integración con SUNAT
    /// </summary>
    public class SunatException : Exception
    {
        public string? SunatErrorCode { get; set; }
        public string? SunatErrorMessage { get; set; }
 
        public SunatException(string message) : base(message) { }
 
        public SunatException(string message, string errorCode, string errorMessage) 
            : base(message)
        {
            SunatErrorCode = errorCode;
            SunatErrorMessage = errorMessage;
        }
    }
}