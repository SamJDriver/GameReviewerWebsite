using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Models
{
    public class GameGenresLookupLinkDto : BaseDto<GameGenresLookupLinkDto, GamesGenresLookupLink>
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        // public GameDto Game { get; set; }
        public int GenreLookupId { get; set; }
        public GenreLookupDto GenreLookup { get; set; }
    }
}
