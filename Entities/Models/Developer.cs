using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models.Joint_Models;

namespace Entities.Models
{
    // Game developer (Valve, Nintendo, Bandai Namco...)
    public class Developer
    {
        [Column("DeveloperId")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Developer Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Developer Name is 60 characters")]
        public string? Name { get; set; }

        public string? Location { get; set; }

        public string? Description { get; set; }

        public Uri? WebsiteUrl { get; set; }

        // Collection of games from the developer
        public ICollection<GameDevelopers>? GameDevelopers { get; set; }

        // Collection of dlc from the developer
        public ICollection<DlcDevelopers>? DlcDevelopers { get; set; }
    }
}
