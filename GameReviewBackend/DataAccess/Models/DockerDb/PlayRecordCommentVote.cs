using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("play_record_comment_vote")]
    [Index(nameof(CreatedBy), Name = "created_by")]
    [Index(nameof(PlayRecordCommentId), Name = "play_record_comment_id")]
    public partial class PlayRecordCommentVote : ITrackable
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("play_record_comment_id", TypeName = "int(11)")]
        public int PlayRecordCommentId { get; set; }
        [Column("numerical_value", TypeName = "int(11)")]
        public int? NumericalValue { get; set; }
        [Column("emoji_value")]
        [StringLength(50)]
        public string? EmojiValue { get; set; }
        [Column("created_by")]
        [StringLength(60)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(PlayRecordCommentId))]
        [InverseProperty(nameof(PlayRecordComments.PlayRecordCommentVote))]
        public virtual PlayRecordComments PlayRecordComment { get; set; } = null!;
    }
}
