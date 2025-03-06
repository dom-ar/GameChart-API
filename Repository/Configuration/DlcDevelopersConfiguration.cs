using Entities.Models.Joint_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class DlcDevelopersConfiguration : IEntityTypeConfiguration<DlcDevelopers>
    {
        public void Configure(EntityTypeBuilder<DlcDevelopers> builder)
        {
            // Primary key
            builder.HasKey(dd => new { dd.DlcId, dd.DeveloperId });

            // Relationship
            builder.HasOne(dd => dd.Dlc)
                .WithMany(d => d.DlcDevelopers)
                .HasForeignKey(dd => dd.DlcId);

            builder.HasOne(dd => dd.Developer)
                .WithMany(d => d.DlcDevelopers)
                .HasForeignKey(dd => dd.DeveloperId);
        }
    }
}