﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("games_genres_lookup_link")]
[Index("GameId", Name = "game_id")]
[Index("GenreLookupId", Name = "genre_lookup_id")]
[Index("Id", Name = "id", IsUnique = true)]
public partial class GamesGenresLookupLink
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("game_id", TypeName = "int(11)")]
    public int GameId { get; set; }

    [Column("genre_lookup_id", TypeName = "int(11)")]
    public int GenreLookupId { get; set; }

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

    [ForeignKey("GameId")]
    [InverseProperty("GamesGenresLookupLink")]
    public virtual Games Game { get; set; } = null!;

    [ForeignKey("GenreLookupId")]
    [InverseProperty("GamesGenresLookupLink")]
    public virtual GenresLookup GenreLookup { get; set; } = null!;
}