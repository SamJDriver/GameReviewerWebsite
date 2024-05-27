namespace API.Models
{
    public class CreatePlayRecordJson
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public bool? CompletedFlag { get; set; }
        public int? HoursPlayed { get; set; }
        public int? Rating { get; set; }
        public string? PlayDescription { get; set; }
    }
}