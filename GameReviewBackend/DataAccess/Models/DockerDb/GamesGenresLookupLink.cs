using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("games_genres_lookup_link")]
    [Index(nameof(GameId), Name = "game_id")]
    [Index(nameof(GenreLookupId), Name = "genre_lookup_id")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    public partial class GamesGenresLookupLink : ITrackable
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("game_id", TypeName = "int(11)")]
        public int GameId { get; set; }
        [Column("genre_lookup_id", TypeName = "int(11)")]
        public int GenreLookupId { get; set; }
        [Column("created_by")]
        [StringLength(60)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty(nameof(Games.GamesGenresLookupLink))]
        public virtual Games Game { get; set; } = null!;
        [ForeignKey(nameof(GenreLookupId))]
        [InverseProperty(nameof(GenresLookup.GamesGenresLookupLink))]
        public virtual GenresLookup GenreLookup { get; set; } = null!;
    }
}
