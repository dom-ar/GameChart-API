using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models.Joint_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    internal class DlcGenresConfiguration : IEntityTypeConfiguration<DlcGenres>
    {
        public void Configure(EntityTypeBuilder<DlcGenres> builder)
        {
            // Primary key
            builder.HasKey(dg => new { dg.DlcId, dg.GenreId });

            // Relationship
           builder.HasOne(dg => dg.Dlc)
                .WithMany(d => d.DlcGenres)
                .HasForeignKey(dg => dg.DlcId);

            builder.HasOne(dg => dg.Genre)
                .WithMany(g => g.DlcGenres)
                .HasForeignKey(dg => dg.GenreId);

        }
    }
}