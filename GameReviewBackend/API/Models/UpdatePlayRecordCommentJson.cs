using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class UpdatePlayRecordCommentJson
    {
        public string CommentText { get; set; } = null!;
    }
}