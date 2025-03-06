using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Configuration
{
    public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            // Primary key
            builder.HasKey(p => p.Id);

            // Configure Id as auto-incremented value
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            // Relationships with game releases
            builder.HasMany(p => p.GameReleases)
                .WithOne(gr => gr.Platform)
                .HasForeignKey(gr => gr.PlatformId);

            // With dlc releases
            builder.HasMany(p => p.DlcReleases)
                .WithOne(dr => dr.Platform)
                .HasForeignKey(dr => dr.PlatformId);
        }
    }
}