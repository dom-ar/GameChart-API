using Entities.Models;
using Entities.Models.Joint_Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new DlcConfiguration());
            modelBuilder.ApplyConfiguration(new DeveloperConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConfiguration());
            modelBuilder.ApplyConfiguration(new FranchiseConfiguration());
            modelBuilder.ApplyConfiguration(new EngineConfiguration());
            modelBuilder.ApplyConfiguration(new GenreConfiguration());
            modelBuilder.ApplyConfiguration(new PlatformConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            modelBuilder.ApplyConfiguration(new GameReleaseConfiguration());
            modelBuilder.ApplyConfiguration(new DlcReleaseConfiguration());
            // Many-to-Many
            modelBuilder.ApplyConfiguration(new GameDevelopersConfiguration());
            modelBuilder.ApplyConfiguration(new GameGenresConfiguration());
            modelBuilder.ApplyConfiguration(new DlcDevelopersConfiguration());
            modelBuilder.ApplyConfiguration(new DlcGenresConfiguration());

        }

        public DbSet<Game>? Games { get; set; }
        public DbSet<Dlc>? Dlcs { get; set; }
        public DbSet<Developer>? Developers { get; set; }
        public DbSet<Publisher>? Publishers { get; set; }
        public DbSet<Franchise>? Franchises { get; set; }
        public DbSet<Engine>? Engines { get; set; }
        public DbSet<Genre>? Genres { get; set; }
        public DbSet<Platform>? Platforms { get; set; }
        public DbSet<Status>? Status { get; set; }
        public DbSet<GameRelease>? GameReleases { get; set; }
        public DbSet<DlcRelease>? DlcReleases { get; set; }
        public DbSet<GameDevelopers>? GameDevelopers { get; set; }
        public DbSet<DlcDevelopers>? DlcDevelopers { get; set; }
        public DbSet<GameGenres>? GameGenres { get; set; }
        public DbSet<DlcGenres>? DlcGenres { get; set; }

    }

}
