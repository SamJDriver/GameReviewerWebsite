﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models.DockerDb;

[Table("play_record_comments")]
[Index("Id", Name = "id", IsUnique = true)]
[Index("PlayRecordId", Name = "play_record_id")]
[Index("UserId", Name = "user_id")]
public partial class PlayRecordComments
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("user_id", TypeName = "int(11)")]
    public int UserId { get; set; }

    [Column("play_record_id", TypeName = "int(11)")]
    public int PlayRecordId { get; set; }

    [Column("comment_text", TypeName = "mediumtext")]
    public string CommentText { get; set; } = null!;

    [Column("upvote_count", TypeName = "int(11)")]
    public int UpvoteCount { get; set; }

    [Column("downvote_count", TypeName = "int(11)")]
    public int DownvoteCount { get; set; }

    [Column("created_by")]
    [StringLength(25)]
    public string CreatedBy { get; set; } = null!;

    [Column("created_date", TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column("modified_by")]
    [StringLength(25)]
    public string? ModifiedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("obsolete_flag")]
    public bool ObsoleteFlag { get; set; }

    [Column("obsolete_date", TypeName = "datetime")]
    public DateTime? ObsoleteDate { get; set; }

    [ForeignKey("PlayRecordId")]
    [InverseProperty("PlayRecordComments")]
    public virtual PlayRecords PlayRecord { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("PlayRecordComments")]
    public virtual Users User { get; set; } = null!;
}