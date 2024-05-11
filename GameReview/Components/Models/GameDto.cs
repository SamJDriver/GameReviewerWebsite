using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Components.Models
{
    public class GameDto
    {
        public int? Id { get; set; }
        public string Title { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        public string ImageFilePath { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool ObsoleteFlag { get; set; }
        public DateTime? ObsoleteDate { get; set; }
    }
}
