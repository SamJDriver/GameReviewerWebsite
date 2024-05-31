using System.ComponentModel.DataAnnotations;
using Components.Exceptions;

namespace API.Models
{
    public class CreateGameJson
    {
        [StringLength(255)]
        public string Title { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        [StringLength(65535)]
        public string Description { get; set; } = null!;
    }
}