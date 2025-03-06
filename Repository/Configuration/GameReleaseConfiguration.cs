using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class GameReleaseConfiguration : IEntityTypeConfiguration<GameRelease>
    {
        public void Configure(EntityTypeBuilder<GameRelease> builder)
        {
            // Primary Key
            builder.HasKey(gr => gr.Id);

            // Configure Id as auto-incremented value
            builder.Property(gr => gr.Id)
                .ValueGeneratedOnAdd();

            // Relationships

            // GameI 
            builder.HasOne(gr => gr.Game)
                .WithMany(g => g.GameReleases)
                .HasForeignKey(gr => gr.GameId);

            // Platform
            builder.HasOne(gr => gr.Platform)
                .WithMany(p => p.GameReleases)
                .HasForeignKey(gr => gr.PlatformId);

            // Status
            builder.HasOne(gr => gr.Status)
                .WithMany(s => s.GameReleases)
                .HasForeignKey(gr => gr.StatusId);
        }
    }
}
