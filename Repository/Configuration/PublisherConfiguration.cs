using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Configuration
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            // Primary key
            builder.HasKey(p => p.Id);

            // Configure Id as auto-incremented value
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            // Relationships with game
            builder.HasMany(p => p.Games)
                .WithOne(g => g.Publisher)
                .HasForeignKey(g => g.PublisherId);

            // With dlc
            builder.HasMany(p => p.Dlcs)
                .WithOne(d => d.Publisher)
                .HasForeignKey(d => d.PublisherId);
        }
    }
}