using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("platforms")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    public partial class Platforms : ITrackable
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
        [StringLength(60)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [InverseProperty("Platform")]
        public virtual ICollection<GamesPlatformsLink> GamesPlatformsLink { get; set; }
    }
}
