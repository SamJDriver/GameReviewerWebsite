using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("user_relationship")]
    [Index(nameof(UserRelationshipTypeLookupId), Name = "user_relationship_ibfk_1")]
    public partial class UserRelationship : ITrackable
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("child_user_id")]
        [StringLength(60)]
        public string ChildUserId { get; set; } = null!;
        [Column("user_relationship_type_lookup_id", TypeName = "int(11)")]
        public int UserRelationshipTypeLookupId { get; set; }
        [Column("created_by")]
        [StringLength(60)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(UserRelationshipTypeLookupId))]
        [InverseProperty("UserRelationship")]
        public virtual UserRelationshipTypeLookup UserRelationshipTypeLookup { get; set; } = null!;
    }
}
