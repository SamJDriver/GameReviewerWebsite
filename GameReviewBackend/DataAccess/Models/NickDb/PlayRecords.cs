﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models.NickDb;

[Table("play_records")]
[Index("GameId", Name = "game_id")]
[Index("Id", Name = "id", IsUnique = true)]
[Index("UserId", Name = "user_id")]
public partial class PlayRecords
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("user_id", TypeName = "char(16)")]
    public string UserId { get; set; }

    [Column("game_id", TypeName = "int(11)")]
    public int GameId { get; set; }

    [Column("completed_flag")]
    public bool? CompletedFlag { get; set; }

    [Column("hours_played", TypeName = "int(11)")]
    public int? HoursPlayed { get; set; }

    [Column("rating", TypeName = "int(11)")]
    public int? Rating { get; set; }

    [Column("play_description", TypeName = "mediumtext")]
    public string? PlayDescription { get; set; }

    [Column("created_by")]
    [StringLength(25)]
    public string CreatedBy { get; set; } = null!;

    [Column("created_date", TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column("modified_by")]
    [StringLength(25)]
    public string? ModifiedBy { get; set; }

    [Column("modified_date")]
    public DateOnly? ModifiedDate { get; set; }

    [Column("obsolete_flag")]
    public bool ObsoleteFlag { get; set; }

    [Column("obsolete_date")]
    public DateOnly? ObsoleteDate { get; set; }

    [ForeignKey("GameId")]
    [InverseProperty("PlayRecords")]
    public virtual Games Game { get; set; } = null!;

    [InverseProperty("PlayRecord")]
    public virtual ICollection<PlayRecordComments> PlayRecordComments { get; set; } = new List<PlayRecordComments>();

    [ForeignKey("UserId")]
    [InverseProperty("PlayRecords")]
    public virtual Users User { get; set; } = null!;
}