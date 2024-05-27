using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Components.Models
{
    public class PlayRecordDto
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
        public bool? CompletedFlag { get; set; }
        public int? HoursPlayed { get; set; }
        public int? Rating { get; set; }
        [StringLength(255)]
        public string? PlayDescription { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool ObsoleteFlag { get; set; }
        public DateTime? ObsoleteDate { get; set; }
    }
}
