using System.ComponentModel.DataAnnotations;

namespace GameReview.Models
{
    public class UserLoginJson
    {
        [StringLength(25)]
        public string? Username { get; set; } = default!;

        [StringLength(100)]
        public string? Email { get; set; } = default!;

        [Required]
        [StringLength(20)]
        public string Password { get; set; } = default!;


    }
}
