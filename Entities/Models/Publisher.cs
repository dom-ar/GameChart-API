using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Game publishers (2K Games, Devolver Digital, Rockstar Games)
    public class Publisher
    {
        [Column("PublisherId")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Publisher Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Publisher Name is 60 characters")]
        public string? Name { get; set; }

        public string? Country { get; set; }

        // Collection of published games
        public ICollection<Game>? Games { get; set; }

        // Collection of published dlc
        public ICollection<Dlc>? Dlcs { get; set; }
    }
}