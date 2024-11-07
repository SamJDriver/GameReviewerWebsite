using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Models;

public class Game_PlayRecordList_Dto : BaseDto<Game_PlayRecordList_Dto, Games>
{
    public int GameId { get; set; }

    public int PlayRecordId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = default!;

    [StringLength(255)]
    public string CoverImageUrl { get; set; } = default!;

    public PlayRecordDto[] PlayRecords { get; set; }
}

