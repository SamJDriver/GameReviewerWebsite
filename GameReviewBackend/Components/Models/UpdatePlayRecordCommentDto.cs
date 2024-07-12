using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Models
{
    public class UpdatePlayRecordCommentDto : BaseDto<UpdatePlayRecordCommentDto, PlayRecordComments>
    {
        [StringLength(65535)]
        public string CommentText { get; set; } = null!;
    }
}