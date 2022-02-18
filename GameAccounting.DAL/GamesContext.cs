using GameAccounting.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace GameAccounting.DAL
{
    public partial class GamesContext : DbContext
    {
        public DbSet<Game> Game { get; set;}
        public DbSet<Genre> Genre { get; set;}
        public DbSet<Developer> Developer { get; set; }

        public GamesContext(DbContextOptions<GamesContext> opt)
            : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(e =>
            {
                e.HasKey(g => g.Id);
                e.Property(g => g.Name)
                    .IsRequired();
                e.Property(g => g.ReleaseDate)
                    .IsRequired();

                e.HasOne(g => g.Developer)
                    .WithMany(g => g.Games)
                    .HasForeignKey(g => g.DeveloperId);
                e.HasMany(g => g.Genres)
                    .WithMany(g => g.Games);
            });
            modelBuilder.Entity<Genre>(e =>
            {
                e.HasKey(g => g.Id);
                e.Property(g => g.Name)
                 .IsRequired();
            });
            modelBuilder.Entity<Developer>(e =>
            {
                e.HasKey(d => d.Id);
                e.Property(d => d.Name)
                 .IsRequired();
            });
        }
    }
}