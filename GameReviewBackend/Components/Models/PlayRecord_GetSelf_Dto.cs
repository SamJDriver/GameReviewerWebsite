using System.ComponentModel.DataAnnotations;

namespace Components.Models
{
    public class PlayRecord_GetSelf_Dto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string GameTitle { get; set; } = default!;
        public string? CoverImageUrl { get; set; }
        public bool? CompletedFlag { get; set; }
        public int? HoursPlayed { get; set; }
        public int? Rating { get; set; }
        public string? PlayDescription { get; set; }
        public DateTime CreatedDate { get; set; } = default;
    }
}
