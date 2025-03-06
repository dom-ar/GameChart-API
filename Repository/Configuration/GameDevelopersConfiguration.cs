using Entities.Models.Joint_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class GameDevelopersConfiguration : IEntityTypeConfiguration<GameDevelopers>
    {
        public void Configure(EntityTypeBuilder<GameDevelopers> builder)
        {
            // Primary key
            builder.HasKey(gd => new { gd.GameId, gd.DeveloperId });

            // Relationship
            builder.HasOne(gd => gd.Game)
                .WithMany(g => g.GameDevelopers)
                .HasForeignKey(gd => gd.GameId);

            builder.HasOne(gd => gd.Developer)
                .WithMany(d => d.GameDevelopers)
                .HasForeignKey(gd => gd.DeveloperId);
        }
    }
}
