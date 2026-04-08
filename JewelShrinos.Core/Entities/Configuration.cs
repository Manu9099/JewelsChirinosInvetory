namespace JewelShrinos.Core.Entities
{
    public class Configuration
    {
        public int ConfigId { get; set; }
        public string Key { get; set; } = null!; // UNIQUE
        public string? Value { get; set; }
        public string? Description { get; set; }
        public string? DataType { get; set; } // STRING, NUMBER, BOOLEAN, DATE
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
 