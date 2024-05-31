﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models.DockerDb;

[Table("games_companies_link")]
[Index("CompaniesId", Name = "companies_id")]
[Index("GamesId", Name = "games_id")]
[Index("Id", Name = "id", IsUnique = true)]
public partial class GamesCompaniesLink
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("games_id", TypeName = "int(11)")]
    public int GamesId { get; set; }

    [Column("companies_id", TypeName = "int(11)")]
    public int CompaniesId { get; set; }

    [Column("developer_flag")]
    public bool DeveloperFlag { get; set; }

    [Column("publisher_flag")]
    public bool PublisherFlag { get; set; }

    [Column("porting_flag")]
    public bool PortingFlag { get; set; }

    [Column("supporting_flag")]
    public bool SupportingFlag { get; set; }

    [Column("created_by")]
    [StringLength(25)]
    public string CreatedBy { get; set; } = null!;

    [Column("created_date", TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [ForeignKey("CompaniesId")]
    [InverseProperty("GamesCompaniesLink")]
    public virtual Companies Companies { get; set; } = null!;

    [ForeignKey("GamesId")]
    [InverseProperty("GamesCompaniesLink")]
    public virtual Games Games { get; set; } = null!;
}