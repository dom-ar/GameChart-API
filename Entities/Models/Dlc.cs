using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models.Joint_Models;

namespace Entities.Models
{
    // DLC information
    public class Dlc
    {
        [Column("DlcId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "DLC Title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        // Would only set this once the game was released
        public int? ReleasedOnYear { get; set; }

        [ForeignKey(nameof(Game))]
        [Required(ErrorMessage = "GameId is a required field.")]
        public int GameId { get; set; }
        public Game? Game { get; set; }

        [ForeignKey(nameof(Publisher))]
        [Required(ErrorMessage = "PublisherId is a required field.")]
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        // Collection of releases for the dlc
        public ICollection<DlcRelease> DlcReleases { get; set; } = new List<DlcRelease>();

        // Collection of dev for the dlc
        public ICollection<DlcDevelopers> DlcDevelopers { get; set; } = new List<DlcDevelopers>();

        // Collection of Genres for the dlc
        public ICollection<DlcGenres> DlcGenres { get; set; } = new List<DlcGenres>();
    }
}