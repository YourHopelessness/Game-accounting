namespace GameAccounting.DAL.Entity
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid DeveloperId { get; set; }

        public Developer Developer { get; set; }
        public IList<Genre> Genres { get; set; }
    }
}