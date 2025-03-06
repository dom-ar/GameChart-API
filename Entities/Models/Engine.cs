using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    // Game engine (Unity, Godot, Unreal Engine...)
    public class Engine
    {
        [Column("EngineId")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Engine Name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Engine Name is 60 characters")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public Uri? WebsiteUrl { get; set; }

        // Collection of games using the engine
        public ICollection<Game>? Games { get; set; }
    }
}