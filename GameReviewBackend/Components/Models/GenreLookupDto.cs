using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Models
{
    public class GenreLookupDto : BaseDto<GenreLookupDto, GenresLookup>
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(8)]
        public string? Code { get; set; }

        public string? Description { get; set; }
    }
}