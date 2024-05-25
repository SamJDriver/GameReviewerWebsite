﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("games_developers_link")]
[Index("DeveloperId", Name = "developer_id")]
[Index("GameId", Name = "game_id")]
[Index("Id", Name = "id", IsUnique = true)]
public partial class GamesDevelopersLink
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("game_id", TypeName = "int(11)")]
    public int GameId { get; set; }

    [Column("developer_id", TypeName = "int(11)")]
    public int DeveloperId { get; set; }

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

    [ForeignKey("DeveloperId")]
    [InverseProperty("GamesDevelopersLink")]
    public virtual Developers Developer { get; set; } = null!;

    [ForeignKey("GameId")]
    [InverseProperty("GamesDevelopersLink")]
    public virtual Games Game { get; set; } = null!;
}