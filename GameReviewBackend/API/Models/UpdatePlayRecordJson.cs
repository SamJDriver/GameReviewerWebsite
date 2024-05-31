using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UpdatePlayRecordJson
    {
        public bool? CompletedFlag { get; set; }
        public int? HoursPlayed { get; set; }
        public int? Rating { get; set; }
        [StringLength(255, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string? PlayDescription { get; set; }
    }
}