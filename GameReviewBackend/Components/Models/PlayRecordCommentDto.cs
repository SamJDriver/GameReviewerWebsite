using System.ComponentModel.DataAnnotations;

namespace Components.Models
{
    public class PlayRecordCommentDto
    {
        public int? Id { get; set; }
        public int PlayRecordId { get; set; }
        [StringLength(65535)]
        public string CommentText { get; set; } = null!;
        [StringLength(36)]
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = default;

        public int NumericalValue { get; set; }
    }
}
