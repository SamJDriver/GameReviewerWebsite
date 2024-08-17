using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("cover")]
    [Index(nameof(GameId), Name = "game_id")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    public partial class Cover : ITrackable
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("game_id", TypeName = "int(11)")]
        public int GameId { get; set; }
        [Column("alpha_channel_flag")]
        public bool AlphaChannelFlag { get; set; }
        [Column("animated_flag")]
        public bool AnimatedFlag { get; set; }
        [Column("height", TypeName = "int(11)")]
        public int Height { get; set; }
        [Column("width", TypeName = "int(11)")]
        public int Width { get; set; }
        [Column("image_url")]
        [StringLength(255)]
        public string ImageUrl { get; set; } = null!;
        [Column("created_by")]
        [StringLength(60)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty(nameof(Games.Cover))]
        public virtual Games Game { get; set; } = null!;
    }
}
