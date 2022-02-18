namespace GameAccounting.BL.Models
{
    public class GameToAddDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public Guid? Developer { get; set; }
        public int[]? Genres { get; set; }
    }
}
