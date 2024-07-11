using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("user_relationship")]
    [Index(nameof(FriendId), Name = "friend_id")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    [Index(nameof(RelationshipTypeLookupId), Name = "relationship_type_lookup_id")]
    [Index(nameof(UserId), Name = "user_id")]
    public partial class UserRelationship : ITrackable
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("user_id")]
        [StringLength(36)]
        public string UserId { get; set; } = null!;
        [Column("friend_id", TypeName = "int(11)")]
        public int FriendId { get; set; }
        [Column("relationship_type_lookup_id", TypeName = "int(11)")]
        public int RelationshipTypeLookupId { get; set; }
        [Column("created_by")]
        [StringLength(36)]
        public string CreatedBy { get; set; } = null!;
        [Column("created_date", TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey(nameof(FriendId))]
        [InverseProperty(nameof(Users.UserRelationship))]
        public virtual Users Friend { get; set; } = null!;
        [ForeignKey(nameof(RelationshipTypeLookupId))]
        [InverseProperty(nameof(UserRelationshipTypeLookup.UserRelationship))]
        public virtual UserRelationshipTypeLookup RelationshipTypeLookup { get; set; } = null!;
    }
}
