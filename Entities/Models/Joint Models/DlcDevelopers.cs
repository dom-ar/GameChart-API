using System.ComponentModel.DataAnnotations;

namespace Entities.Models.Joint_Models
{
    public class DlcDevelopers
    {
        [Required(ErrorMessage = "DlcId is a required field")]
        public int DlcId { get; set; }
        public Dlc? Dlc { get; set; }

        [Required(ErrorMessage = "DeveloperId is a required field")]
        public int DeveloperId { get; set; }
        public Developer? Developer { get; set; }
    }
}