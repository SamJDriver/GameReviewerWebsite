﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models.DockerDb;

[Table("users")]
[Index("Id", Name = "id", IsUnique = true)]
public partial class Users
{
    [Key]
    [Column("id", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(25)]
    public string Username { get; set; } = null!;

    [Column("password_hash")]
    [StringLength(64)]
    public string PasswordHash { get; set; } = null!;

    [Column("salt")]
    [StringLength(50)]
    public string? Salt { get; set; }

    [Column("admin_flag")]
    public bool AdminFlag { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("image_file_path")]
    [StringLength(255)]
    public string? ImageFilePath { get; set; }

    [Column("created_by")]
    [StringLength(25)]
    public string CreatedBy { get; set; } = null!;

    [Column("created_date", TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column("modified_by")]
    [StringLength(25)]
    public string? ModifiedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("obsolete_flag")]
    public bool ObsoleteFlag { get; set; }

    [Column("obsolete_date", TypeName = "datetime")]
    public DateTime? ObsoleteDate { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<PlayRecordComments> PlayRecordComments { get; set; } = new List<PlayRecordComments>();

    [InverseProperty("User")]
    public virtual ICollection<PlayRecords> PlayRecords { get; set; } = new List<PlayRecords>();

    [InverseProperty("Friend")]
    public virtual ICollection<UserRelationship> UserRelationshipFriend { get; set; } = new List<UserRelationship>();

    [InverseProperty("User")]
    public virtual ICollection<UserRelationship> UserRelationshipUser { get; set; } = new List<UserRelationship>();
}