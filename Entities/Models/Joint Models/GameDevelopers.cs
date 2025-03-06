using System.ComponentModel.DataAnnotations;

namespace Entities.Models.Joint_Models
{
    public class GameDevelopers
    {
        [Required(ErrorMessage = "GameId is a required field")]
        public int GameId { get; set; }
        public Game? Game { get; set; }

        [Required(ErrorMessage = "DeveloperId is a required field")]
        public int DeveloperId { get; set; }
        public Developer? Developer { get; set; }
    }
}
