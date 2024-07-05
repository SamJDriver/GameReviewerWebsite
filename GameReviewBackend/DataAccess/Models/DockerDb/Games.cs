using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("games")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    public partial class Games : ITrackable
    {
        public Games()
        {
            Artwork = new HashSet<Artwork>();
            Cover = new HashSet<Cover>();
            GameSelfLinkChildGame = new HashSet<GameSelfLink>();
            GameSelfLinkParentGame = new HashSet<GameSelfLink>();
            GamesCompaniesLink = new HashSet<GamesCompaniesLink>();
            GamesGenresLookupLink = new HashSet<GamesGenresLookupLink>();
            GamesPlatformsLink = new HashSet<GamesPlatformsLink>();
            PlayRecords = new HashSet<PlayRecords>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("title")]
        [StringLength(255)]
        public string Title { get; set; } = null!;
        [Column("release_date")]
        public DateOnly ReleaseDate { get; set; }
        [Column("description", TypeName = "text")]
        public string Description { get; set; } = null!;
        [Column("created_by")]
        [StringLength(255)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [InverseProperty("Game")]
        public virtual ICollection<Artwork> Artwork { get; set; }
        [InverseProperty("Game")]
        public virtual ICollection<Cover> Cover { get; set; }
        [InverseProperty(nameof(GameSelfLink.ChildGame))]
        public virtual ICollection<GameSelfLink> GameSelfLinkChildGame { get; set; }
        [InverseProperty(nameof(GameSelfLink.ParentGame))]
        public virtual ICollection<GameSelfLink> GameSelfLinkParentGame { get; set; }
        [InverseProperty("Games")]
        public virtual ICollection<GamesCompaniesLink> GamesCompaniesLink { get; set; }
        [InverseProperty("Game")]
        public virtual ICollection<GamesGenresLookupLink> GamesGenresLookupLink { get; set; }
        [InverseProperty("Game")]
        public virtual ICollection<GamesPlatformsLink> GamesPlatformsLink { get; set; }
        [InverseProperty("Game")]
        public virtual ICollection<PlayRecords> PlayRecords { get; set; }
    }
}
