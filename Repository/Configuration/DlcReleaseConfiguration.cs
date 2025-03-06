using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class DlcReleaseConfiguration : IEntityTypeConfiguration<DlcRelease>
    {
        public void Configure(EntityTypeBuilder<DlcRelease> builder)
        {
            // Primary Key
            builder.HasKey(dr => dr.Id);

            // Configure Id as auto-incremented value
            builder.Property(dr => dr.Id)
                .ValueGeneratedOnAdd();

            // Relationships

            // Dlc
            builder.HasOne(dr => dr.Dlc)
                .WithMany(d => d.DlcReleases)
                .HasForeignKey(dr => dr.DlcId);

            // Platform
            builder.HasOne(dr => dr.Platform)
                .WithMany(p => p.DlcReleases)
                .HasForeignKey(dr => dr.PlatformId);

            // Status
            builder.HasOne(dr => dr.Status)
                .WithMany(s => s.DlcReleases)
                .HasForeignKey(dr => dr.StatusId);
        }
    }
}