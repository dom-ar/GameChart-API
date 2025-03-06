using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class DlcConfiguration : IEntityTypeConfiguration<Dlc>
    {
        public void Configure(EntityTypeBuilder<Dlc> builder)
        {
            // Primary key
            builder.HasKey(d => d.Id);

            // Configure Id as auto-incremented value
            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            // Relationship with game
            builder.HasOne(d => d.Game)
                .WithMany(g => g.Dlc)
                .HasForeignKey(d => d.GameId);

            // Dlc releases
            builder.HasMany(g => g.DlcReleases)
                .WithOne(dr => dr.Dlc)
                .HasForeignKey(dr => dr.DlcId);

            // Developers trough DlcDevelopers
            builder.HasMany(d => d.DlcDevelopers)
                .WithOne(dd => dd.Dlc)
                .HasForeignKey(dd => dd.DlcId);

            // Publisher
            builder.HasOne(d => d.Publisher)
                .WithMany(p => p.Dlcs)
                .HasForeignKey(p => p.PublisherId);

            // Genre trough DlcGenre
            builder.HasMany(d => d.DlcGenres)
                .WithOne(dg => dg.Dlc)
                .HasForeignKey(dg => dg.DlcId);
        }
    }
}
