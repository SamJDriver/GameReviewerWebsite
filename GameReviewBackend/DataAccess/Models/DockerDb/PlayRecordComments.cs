using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("play_record_comments")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    [Index(nameof(PlayRecordId), Name = "play_record_id")]
    public partial class PlayRecordComments : ITrackable
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("user_id")]
        [StringLength(36)]
        public string UserId { get; set; } = null!;
        [Column("play_record_id", TypeName = "int(11)")]
        public int PlayRecordId { get; set; }
        [Column("comment_text", TypeName = "mediumtext")]
        public string CommentText { get; set; } = null!;
        [Column("upvote_count", TypeName = "int(11)")]
        public int UpvoteCount { get; set; }
        [Column("downvote_count", TypeName = "int(11)")]
        public int DownvoteCount { get; set; }
        [Column("created_by")]
        [StringLength(36)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(PlayRecordId))]
        [InverseProperty(nameof(PlayRecords.PlayRecordComments))]
        public virtual PlayRecords PlayRecord { get; set; } = null!;
    }
}
