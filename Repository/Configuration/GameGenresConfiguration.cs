using Entities.Models.Joint_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    internal class GameGenresConfiguration : IEntityTypeConfiguration<GameGenres>
    {
        public void Configure(EntityTypeBuilder<GameGenres> builder)
        {
            // Primary key
            builder.HasKey(gg => new { gg.GameId, gg.GenreId });

            // Relationship
            builder.HasOne(gg => gg.Game)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GameId);

            builder.HasOne(gg => gg.Genre)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GenreId);

        }
    }
}
