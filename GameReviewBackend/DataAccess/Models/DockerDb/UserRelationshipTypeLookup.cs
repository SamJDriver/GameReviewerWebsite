using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("user_relationship_type_lookup")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    public partial class UserRelationshipTypeLookup : ITrackable
    {
        public UserRelationshipTypeLookup()
        {
            UserRelationship = new HashSet<UserRelationship>();
        }

        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; } = null!;
        [Column("code")]
        [StringLength(8)]
        public string Code { get; set; } = null!;
        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = null!;
        [Column("created_by")]
        [StringLength(36)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [InverseProperty("RelationshipTypeLookup")]
        public virtual ICollection<UserRelationship> UserRelationship { get; set; }
    }
}
