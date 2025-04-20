using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Models
{
    public class Game_Get_ById_Dto : BaseDto<GameDto, Games>
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Title { get; set; } = null!;

        public DateOnly ReleaseDate { get; set; }

        public IEnumerable<string> ArtworkUrls { get; set; } = default!;

        public string CoverImageUrl { get; set; } = default!;

        public int? ParentId { get; set; }

        public IEnumerable<int>? ChildGameIds { get; set; }

        public IEnumerable<GenreLookupDto> Genres { get; set; } = default!;

        public IEnumerable<Game_Get_ById_CompanyLink_Dto> Companies { get; set; } = default!;

        public IEnumerable<PlatformDto> Platforms { get; set; } = default!;

        [StringLength(65535)]
        public string Description { get; set; } = null!;
    }
}
