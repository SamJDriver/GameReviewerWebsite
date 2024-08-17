using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("games_companies_link")]
    [Index(nameof(CompaniesId), Name = "companies_id")]
    [Index(nameof(GamesId), Name = "games_id")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    public partial class GamesCompaniesLink : ITrackable
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
        [StringLength(60)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(CompaniesId))]
        [InverseProperty("GamesCompaniesLink")]
        public virtual Companies Companies { get; set; } = null!;
        [ForeignKey(nameof(GamesId))]
        [InverseProperty("GamesCompaniesLink")]
        public virtual Games Games { get; set; } = null!;
    }
}
