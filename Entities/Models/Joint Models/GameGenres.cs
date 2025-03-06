using System.ComponentModel.DataAnnotations;

namespace Entities.Models.Joint_Models
{
    public class GameGenres
    {
        [Required(ErrorMessage = "GameId is a required field")]
        public int GameId { get; set; }
        public Game? Game { get; set; }

        [Required(ErrorMessage = "GenreId is a required field")]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
