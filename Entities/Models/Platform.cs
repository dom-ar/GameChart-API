using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Game platform/store (Steam, Epic, Xbox, Playstation)
    public class Platform
    {
        [Column("PlatformId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Platform Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Platform Name is 60 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Platform description is a required field.")]
        public string? Description { get; set; }

        // Collection of game releases on the platform
        public ICollection<GameRelease>? GameReleases { get; set; }

        // Collection of dlc releases on the platform
        public ICollection<DlcRelease>? DlcReleases { get; set; }

    }
}