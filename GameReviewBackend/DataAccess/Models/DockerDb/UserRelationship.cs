using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("user_relationship")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    [Index(nameof(ChildUserId), Name = "child_user_id")]
    [Index(nameof(RelationshipTypeLookupId), Name = "relationship_type_lookup_id")]
    [Index(nameof(CreatedBy), Name = "user_id")]
    public partial class UserRelationship : ITrackable
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("child_user_id")]
        [StringLength(36)]
        public string ChildUserId { get; set; } = null!;
        [Column("relationship_type_lookup_id", TypeName = "int(11)")]
        public int RelationshipTypeLookupId { get; set; }
        [Column("created_by")]
        [StringLength(36)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(RelationshipTypeLookupId))]
        [InverseProperty(nameof(UserRelationshipTypeLookup.UserRelationship))]
        public virtual UserRelationshipTypeLookup RelationshipTypeLookup { get; set; } = null!;
    }
}
