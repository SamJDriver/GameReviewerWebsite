using System.ComponentModel.DataAnnotations;

namespace Components.Models
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Salt { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ImageFilePath { get; set; }
        
        [StringLength(25)]
        public string CreatedBy { get; set; } = default!;
        public DateTime CreatedDate { get; set; } = default;

    }
}