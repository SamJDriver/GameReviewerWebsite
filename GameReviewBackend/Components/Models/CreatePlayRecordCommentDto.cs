using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Models
{
    public class CreatePlayRecordCommentDto : BaseDto<CreatePlayRecordCommentDto, PlayRecords>
    {
        public int PlayRecordId { get; set; }

        [StringLength(65535)]
        public string CommentText { get; set; } = null!;
    }
}