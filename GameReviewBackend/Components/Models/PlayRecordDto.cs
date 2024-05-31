using System.ComponentModel.DataAnnotations;

namespace Components.Models
{
    public class PlayRecordDto
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public bool? CompletedFlag { get; set; }
        public int? HoursPlayed { get; set; }
        public int? Rating { get; set; }
        [StringLength(255)]
        public string? PlayDescription { get; set; }
        [StringLength(25)]
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = default;
    }
}
