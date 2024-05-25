﻿namespace Components.Models
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Salt { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ImageFilePath { get; set; }
        public string? CreatedBy { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? ObsoleteFlag { get; set; }
        public DateTime? ObsoleteDate { get; set; }

    }
}