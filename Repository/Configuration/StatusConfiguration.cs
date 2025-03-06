using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Configuration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            // Primary key
            builder.HasKey(s => s.Id);

            // Configure Id as auto-incremented value
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd();

            // Relationships with game releases
            builder.HasMany(s => s.GameReleases)
                .WithOne(gr => gr.Status)
                .HasForeignKey(gr => gr.StatusId);

            // With dlc releases
            builder.HasMany(p => p.DlcReleases)
                .WithOne(dr => dr.Status)
                .HasForeignKey(dr => dr.StatusId);
        }
    }
}