namespace JewelShrinos.Core.Exceptions
{
    /// <summary>
    /// Excepción para errores de validación
    /// </summary>
    public class ValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; set; }
 
        public ValidationException(string message) 
            : base(message)
        {
            Errors = new Dictionary<string, string[]>();
        }
 
        public ValidationException(Dictionary<string, string[]> errors) 
            : base("Errores de validación")
        {
            Errors = errors;
        }
    }
}