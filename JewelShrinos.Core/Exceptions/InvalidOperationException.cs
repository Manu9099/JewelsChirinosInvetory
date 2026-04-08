namespace JewelShrinos.Core.Exceptions
{
    /// <summary>
    /// Excepción para operaciones inválidas
    /// </summary>
    public class InvalidOperationException : Exception
    {
        public InvalidOperationException(string message) 
            : base(message) { }
    }
}