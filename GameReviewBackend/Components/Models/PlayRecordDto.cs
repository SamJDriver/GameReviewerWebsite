using DataAccess.Models.DockerDb;

namespace Components.Models;

public class PlayRecordDto : BaseDto<PlayRecordDto, PlayRecords>
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public bool? CompletedFlag { get; set; }
    public int? HoursPlayed { get; set; }
    public int? Rating { get; set; }
    public string? PlayDescription { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string? CreatedByName { get; set; } = null!;
}

