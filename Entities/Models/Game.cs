using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models.Joint_Models;

namespace Entities.Models
{
    // Game information
    public class Game
    {
        [Column("GameId")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Game Title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Title is 60 characters.")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [ForeignKey(nameof(Franchise))]
        public int? FranchiseId { get; set; }
        public Franchise? Franchise { get; set; }

        [ForeignKey(nameof(Engine))]
        public int? EngineId { get; set; }
        public Engine? Engine { get; set; }

        [ForeignKey(nameof(Publisher))]
        public int? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        // Would only set this once the game was released
        public int? ReleasedOnYear { get; set; }

        public ICollection<Dlc>? Dlc { get; set; } = new List<Dlc>();
        public ICollection<GameRelease> GameReleases { get; set; } = new List<GameRelease>();
        public ICollection<GameDevelopers> GameDevelopers { get; set; } = new List<GameDevelopers>();
        public ICollection<GameGenres> GameGenres { get; set; } = new List<GameGenres>();
    }
}