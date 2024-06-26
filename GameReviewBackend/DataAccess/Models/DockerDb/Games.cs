﻿using System;
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
        [Column("image_file_path")]
        [StringLength(255)]
        public string ImageFilePath { get; set; } = null!;
        [Column("description", TypeName = "text")]
        public string Description { get; set; } = null!;
        [Column("created_by")]
        [StringLength(25)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

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
