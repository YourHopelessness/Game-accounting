namespace GameAccounting.DAL.Entity
{
    public class Developer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<Game> Games { get; set; }
    }
}
