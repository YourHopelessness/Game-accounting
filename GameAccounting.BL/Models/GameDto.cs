namespace GameAccounting.BL.Models
{
    public class GameDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Developer { get; set; }
        public GenreDto[]? Genres { get; set; }
    }
}
