using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("games_platforms_link")]
    [Index(nameof(GameId), Name = "game_id")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    [Index(nameof(PlatformId), Name = "platform_id")]
    public partial class GamesPlatformsLink : ITrackable
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("game_id", TypeName = "int(11)")]
        public int GameId { get; set; }
        [Column("platform_id", TypeName = "int(11)")]
        public int PlatformId { get; set; }
        [Column("release_date")]
        public DateOnly? ReleaseDate { get; set; }
        [Column("created_by")]
        [StringLength(36)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty(nameof(Games.GamesPlatformsLink))]
        public virtual Games Game { get; set; } = null!;
        [ForeignKey(nameof(PlatformId))]
        [InverseProperty(nameof(Platforms.GamesPlatformsLink))]
        public virtual Platforms Platform { get; set; } = null!;
    }
}
