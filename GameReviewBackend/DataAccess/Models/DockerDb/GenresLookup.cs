using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("genres_lookup")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    public partial class GenresLookup : ITrackable
    {
        public GenresLookup()
        {
            GamesGenresLookupLink = new HashSet<GamesGenresLookupLink>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("code")]
        [StringLength(8)]
        public string? Code { get; set; }
        [Column("description", TypeName = "mediumtext")]
        public string? Description { get; set; }
        [Column("created_by")]
        [StringLength(36)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [InverseProperty("GenreLookup")]
        public virtual ICollection<GamesGenresLookupLink> GamesGenresLookupLink { get; set; }
    }
}
