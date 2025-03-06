using System.ComponentModel.DataAnnotations;

namespace Entities.Models.Joint_Models
{
    public class DlcGenres
    {
        [Required(ErrorMessage = "DlcId is a required field")]
        public int DlcId { get; set; }
        public Dlc? Dlc { get; set; }

        [Required(ErrorMessage = "GenreId is a required field")]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}