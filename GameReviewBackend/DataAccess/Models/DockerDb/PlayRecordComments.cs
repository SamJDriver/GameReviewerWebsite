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
    [Index(nameof(CreatedBy), Name = "created_by")]
    public partial class PlayRecordComments : ITrackable
    {
        public PlayRecordComments()
        {
            PlayRecordCommentVote = new HashSet<PlayRecordCommentVote>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("play_record_id", TypeName = "int(11)")]
        public int PlayRecordId { get; set; }
        [Column("comment_text", TypeName = "mediumtext")]
        public string CommentText { get; set; } = null!;
        [Column("created_by")]
        [StringLength(36)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(PlayRecordId))]
        [InverseProperty(nameof(PlayRecords.PlayRecordComments))]
        public virtual PlayRecords PlayRecord { get; set; } = null!;
        [InverseProperty("PlayRecordComment")]
        public virtual ICollection<PlayRecordCommentVote> PlayRecordCommentVote { get; set; }
    }
}
