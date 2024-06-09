using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using DataAccess.Abstractions;

namespace DataAccess.Models.DockerDb
{
    [Table("users")]
    [Index(nameof(Id), Name = "id", IsUnique = true)]
    public partial class Users : ITrackable
    {
        public Users()
        {
            PlayRecordComments = new HashSet<PlayRecordComments>();
            PlayRecords = new HashSet<PlayRecords>();
            UserRelationshipFriend = new HashSet<UserRelationship>();
            UserRelationshipUser = new HashSet<UserRelationship>();
        }

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

        [InverseProperty("User")]
        public virtual ICollection<PlayRecordComments> PlayRecordComments { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<PlayRecords> PlayRecords { get; set; }
        [InverseProperty(nameof(UserRelationship.Friend))]
        public virtual ICollection<UserRelationship> UserRelationshipFriend { get; set; }
        [InverseProperty(nameof(UserRelationship.User))]
        public virtual ICollection<UserRelationship> UserRelationshipUser { get; set; }
    }
}
