using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Configuration
{
    public class FranchiseConfiguration : IEntityTypeConfiguration<Franchise>
    {
        public void Configure(EntityTypeBuilder<Franchise> builder)
        {
            // Primary key
            builder.HasKey(f => f.Id);

            // Configure Id as auto-incremented value
            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd();

            // Relationships with game [Multiple games to franchise]
            builder.HasMany(f => f.Games)
                .WithOne(g => g.Franchise)
                .HasForeignKey(g => g.FranchiseId);
        }
    }
}