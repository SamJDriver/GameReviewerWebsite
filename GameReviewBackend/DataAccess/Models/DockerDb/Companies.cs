using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("companies")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    public partial class Companies : ITrackable
    {
        public Companies()
        {
            GamesCompaniesLink = new HashSet<GamesCompaniesLink>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; } = null!;
        [Column("founded_date")]
        public DateOnly FoundedDate { get; set; }
        [Column("image_file_path")]
        [StringLength(255)]
        public string? ImageFilePath { get; set; }
        [Column("developer_flag")]
        public bool DeveloperFlag { get; set; }
        [Column("publisher_flag")]
        public bool PublisherFlag { get; set; }
        [Column("created_by")]
        [StringLength(25)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [InverseProperty("Companies")]
        public virtual ICollection<GamesCompaniesLink> GamesCompaniesLink { get; set; }
    }
}
