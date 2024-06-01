using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CreatePlayRecordCommentJson
    {
        public int PlayRecordId { get; set; }
        [StringLength(65535)]
        public string CommentText { get; set; } = null!;
        public int UpvoteCount { get; set; } = 0;
        public int DownvoteCount { get; set; } = 0;
        [StringLength(25)]
    }
}