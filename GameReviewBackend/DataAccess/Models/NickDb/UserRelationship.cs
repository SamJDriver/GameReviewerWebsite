﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models.NickDb;

[Table("user_relationship")]
[Index("FriendId", Name = "friend_id")]
[Index("Id", Name = "id", IsUnique = true)]
[Index("RelationshipTypeLookupId", Name = "relationship_type_lookup_id")]
[Index("UserId", Name = "user_id")]
public partial class UserRelationship
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("user_id", TypeName = "int(11)")]
    public int UserId { get; set; }

    [Column("friend_id", TypeName = "int(11)")]
    public int FriendId { get; set; }

    [Column("relationship_type_lookup_id", TypeName = "int(11)")]
    public int RelationshipTypeLookupId { get; set; }

    [Column("created_by")]
    [StringLength(25)]
    public string CreatedBy { get; set; } = null!;

    [Column("created_date", TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [ForeignKey("FriendId")]
    [InverseProperty("UserRelationshipFriend")]
    public virtual Users Friend { get; set; } = null!;

    [ForeignKey("RelationshipTypeLookupId")]
    [InverseProperty("UserRelationship")]
    public virtual UserRelationshipTypeLookup RelationshipTypeLookup { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserRelationshipUser")]
    public virtual Users User { get; set; } = null!;
}