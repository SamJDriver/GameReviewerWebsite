using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("game_self_link")]
    [Index(nameof(GameSelfLinkTypeLookupId), Name = "game_self_link_game_self_link_type_lookup_link_ibfk_1")]
    [Index(nameof(ParentGameId), Name = "game_self_link_games_link_ibfk_1")]
    [Index(nameof(ChildGameId), Name = "game_self_link_games_link_ibfk_2")]
    public partial class GameSelfLink : ITrackable
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("parent_game_id", TypeName = "int(11)")]
        public int ParentGameId { get; set; }
        [Column("child_game_id", TypeName = "int(11)")]
        public int ChildGameId { get; set; }
        [Column("game_self_link_type_lookup_id", TypeName = "int(11)")]
        public int GameSelfLinkTypeLookupId { get; set; }
        [Column("created_by")]
        [StringLength(255)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(ChildGameId))]
        [InverseProperty(nameof(Games.GameSelfLinkChildGame))]
        public virtual Games ChildGame { get; set; } = null!;
        [ForeignKey(nameof(GameSelfLinkTypeLookupId))]
        [InverseProperty("GameSelfLink")]
        public virtual GameSelfLinkTypeLookup GameSelfLinkTypeLookup { get; set; } = null!;
        [ForeignKey(nameof(ParentGameId))]
        [InverseProperty(nameof(Games.GameSelfLinkParentGame))]
        public virtual Games ParentGame { get; set; } = null!;
    }
}
