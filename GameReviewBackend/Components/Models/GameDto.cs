using System.ComponentModel.DataAnnotations;
using Components.Exceptions;

namespace Components.Models
{
    public class GameDto
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Title { get; set; } = null!;

        public DateOnly ReleaseDate { get; set; }

        [StringLength(255)]
        public string ImageFilePath { get; set; } = null!;

        [StringLength(65535)]
        public string Description { get; set; } = null!;

        [StringLength(25)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
