﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("play_records")]
    [Index(nameof(CreatedBy), Name = "created_by")]
    [Index(nameof(GameId), Name = "game_id")]
    public partial class PlayRecords : ITrackable
    {
        public PlayRecords()
        {
            PlayRecordComments = new HashSet<PlayRecordComments>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
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
        [Column("start_date", TypeName = "datetime")]
        public DateTime? StartDate { get; set; }

        [Column("end_date", TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        [Column("created_by")]
        [StringLength(36)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("last_updated_date", TypeName = "datetime")]
        public DateTime? LastUpdatedDate { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty(nameof(Games.PlayRecords))]
        public virtual Games Game { get; set; } = null!;
        [InverseProperty("PlayRecord")]
        public virtual ICollection<PlayRecordComments> PlayRecordComments { get; set; }
    }
}
