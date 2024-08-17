using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("game_self_link_type_lookup")]
    public partial class GameSelfLinkTypeLookup : ITrackable
    {
        public GameSelfLinkTypeLookup()
        {
            GameSelfLink = new HashSet<GameSelfLink>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("code")]
        [StringLength(8)]
        public string? Code { get; set; }
        [Column("description")]
        [StringLength(255)]
        public string? Description { get; set; }
        [Column("created_by")]
        [StringLength(60)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [InverseProperty("GameSelfLinkTypeLookup")]
        public virtual ICollection<GameSelfLink> GameSelfLink { get; set; }
    }
}
