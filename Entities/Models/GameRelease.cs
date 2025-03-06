using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Game information
    public class GameRelease
    {
        [Column("ReleaseId")]
        public int Id { get; set; }

        // Year, Which quarter, Month, etc. (Not a concrete date) for the platform
        public string? TimeFrame { get; set; }

        // Exact announced release date for the platform 
        public DateTime? ReleaseDate { get; set; }

        // Set store link
        public Uri? StoreUrl { get; set; }

        // Set what game belongs to this release
        [ForeignKey(nameof(Game))]
        [Required(ErrorMessage = "GameId is a required field to link a release.")]
        public int? GameId { get; set; }
        public Game? Game { get; set; }

        // Set what platform the release is for
        [ForeignKey(nameof(Platform))]
        [Required(ErrorMessage = "PlatformId is a required field to link a release.")]
        public int? PlatformId { get; set; }
        public Platform? Platform { get; set; }

        // Set status for the release
        [ForeignKey(nameof(Status))]
        [Required(ErrorMessage = "StatusId is a required field to link a release.")]
        public int? StatusId { get; set; }
        public Status? Status { get; set; }
    }
}