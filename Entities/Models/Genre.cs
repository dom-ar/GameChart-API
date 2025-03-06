using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models.Joint_Models;

namespace Entities.Models
{
    // Game genre (Action, RPG, Strategy...)
    public class Genre
    {
        [Column("GenreId")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Genre Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Genre Name is 60 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Genre description is a required field.")]
        public string? Description { get; set; }

        // Collection of games with this genre
        public ICollection<GameGenres>? GameGenres { get; set; }

        // Collection of dlc with this genre
        public ICollection<DlcGenres>? DlcGenres { get; set; }
    }
}