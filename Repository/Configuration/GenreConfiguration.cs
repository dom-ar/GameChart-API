using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Models;

namespace Repository.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            // Primary key
            builder.HasKey(g => g.Id);

            // Configure Id as auto-incremented value
            builder.Property(g => g.Id)
                .ValueGeneratedOnAdd();

            // Relationships with Game through GameGenres
            builder.HasMany(g => g.GameGenres)
                .WithOne(gg => gg.Genre)
                .HasForeignKey(gg => gg.GenreId);

            // With dlc trough dlcGenres
            builder.HasMany(g => g.DlcGenres)
                .WithOne(dg => dg.Genre)
                .HasForeignKey(dg => dg.GenreId);
        }
    }
}