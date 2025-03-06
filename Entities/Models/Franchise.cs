using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Game franchise (Fallout, GTA, Final Fantasy...)
    public class Franchise
    {
        [Column("FranchiseId")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Franchise Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Franchise Name is 60 characters")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        // Collection of games from the series
        public ICollection<Game>? Games { get; set; }
    }
}