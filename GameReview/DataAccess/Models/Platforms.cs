﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
    [Table("platforms")]
    [Index("Id", Name = "id", IsUnique = true)]
    public partial class Platforms
    {
        public Platforms()
        {
            GamesPlatformsLink = new HashSet<GamesPlatformsLink>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [Column("release_date")]
        public DateOnly ReleaseDate { get; set; }
        [Column("image_file_path")]
        [StringLength(255)]
        public string ImageFilePath { get; set; } = null!;
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

        [InverseProperty("Platform")]
        public virtual ICollection<GamesPlatformsLink> GamesPlatformsLink { get; set; }
    }
}