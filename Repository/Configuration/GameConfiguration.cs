using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            //Primary key
            builder.HasKey(g => g.Id);

            //Auto-incremented value
            builder.Property(g => g.Id)
                .ValueGeneratedOnAdd();

            //Relationships

            // Genre trough GameGenres
            builder.HasMany(g => g.GameGenres)
                .WithOne(gg => gg.Game)
                .HasForeignKey(gg => gg.GameId);

            // Developers trough GameDevelopers
            builder.HasMany(g => g.GameDevelopers)
                .WithOne(gd => gd.Game)
                .HasForeignKey(gd => gd.GameId);

            // Franchise
            builder.HasOne(g => g.Franchise)
                .WithMany(f => f.Games)
                .HasForeignKey(f => f.FranchiseId);

            // Engine
            builder.HasOne(g => g.Engine)
                .WithMany(e => e.Games)
                .HasForeignKey(e => e.EngineId);

            // Publisher
            builder.HasOne(g => g.Publisher)
                .WithMany(p => p.Games)
                .HasForeignKey(p => p.PublisherId);

            // Game Releases
            builder.HasMany(g => g.GameReleases)
                .WithOne(gr => gr.Game)
                .HasForeignKey(gr => gr.GameId);

            // Dlc
            builder.HasMany(g => g.Dlc)
                .WithOne(d => d.Game)
                .HasForeignKey(d => d.GameId);

        }
    }
}
