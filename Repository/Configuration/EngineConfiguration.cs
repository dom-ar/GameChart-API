using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Configuration
{
    public class EngineConfiguration : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            // Primary key
            builder.HasKey(e => e.Id);

            // Configure Id as auto-incremented value
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            // Relationships with game
            builder.HasMany(e => e.Games)
                .WithOne(g => g.Engine)
                .HasForeignKey(g => g.EngineId);
        }
    }
}
