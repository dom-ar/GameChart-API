using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities.Models;
using Entities.Models.Joint_Models;

namespace Repository.Configuration
{
    public class DeveloperConfiguration : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            // Primary key
            builder.HasKey(d => d.Id);

            // Configure Id as auto-incremented value
            builder.Property(d => d.Id)
                .ValueGeneratedOnAdd();

            // Relationships with Game through GameDevelopers
            builder.HasMany(d => d.GameDevelopers)
                .WithOne(gd => gd.Developer)
                .HasForeignKey(gd => gd.DeveloperId);

            // Relationships with Dlc trough DlcDevelopers
            builder.HasMany(d => d.DlcDevelopers)
                .WithOne(dd => dd.Developer)
                .HasForeignKey(dd => dd.DeveloperId);
        }
    }
}