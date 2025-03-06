using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Game release status [Announced, Released, Delayed, Cancelled, etc...]
    public class Status
    {
        [Column("StatusId")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Status Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Status Name is 60 characters")]
        public string? Name { get; set; }

        // Collection of games with this status
        public ICollection<GameRelease>? GameReleases { get; set; }

        // Collection of dlcs with this status
        public ICollection<DlcRelease>? DlcReleases { get; set; }
    }
}