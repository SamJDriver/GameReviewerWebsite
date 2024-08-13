using System.ComponentModel.DataAnnotations;
using Components.Exceptions;
using DataAccess.Models.DockerDb;

namespace Components.Models
{
    public class GameDto : BaseDto<GameDto, Games>
    {
        public int? Id { get; set; }

        [StringLength(255)]
        public string Title { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        public IEnumerable<ArtworkDto>? Artwork { get; set; }
        public IEnumerable<CoverDto>? Cover { get; set; }
        public int? ParentId { get; set; }
        public IEnumerable<GameSelfLinkDto>? GameSelfLinkParentGame { get; set; }
        public IEnumerable<GameSelfLinkDto>? GameSelfLinkChildGame { get; set; }
        public IEnumerable<GameGenresLookupLinkDto> GamesGenresLookupLink { get; set; }

        [StringLength(65535)]
        public string Description { get; set; } = null!;
    }
}
