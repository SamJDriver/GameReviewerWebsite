using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Models
{
    public class UpdatePlayRecordDto : BaseDto<UpdatePlayRecordDto, PlayRecords>
    {
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
