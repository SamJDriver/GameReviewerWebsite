﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models.NickDb;

[Table("publishers")]
[Index("Id", Name = "id", IsUnique = true)]
public partial class Publishers
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("founded_date")]
    public DateOnly FoundedDate { get; set; }

    [Column("image_file_path")]
    [StringLength(300)]
    public string? ImageFilePath { get; set; }

    [Column("created_by")]
    [StringLength(25)]
    public string CreatedBy { get; set; } = null!;

    [Column("created_date", TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [InverseProperty("Publisher")]
    public virtual ICollection<GamesPublishersLink> GamesPublishersLink { get; set; } = new List<GamesPublishersLink>();
}