using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CreatePlayRecordJson
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public bool? CompletedFlag { get; set; }
        public int? HoursPlayed { get; set; }
        public int? Rating { get; set; }
        [StringLength(255, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string? PlayDescription { get; set; }

        //TODO:
        //PrivateFlag
        //Start/EndDate
    }
}