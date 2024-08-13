using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Models
{
    public class GameSelfLinkDto : BaseDto<GameSelfLinkDto, GameSelfLink>
    {
        public int Id { get; set; }
        public int ParentGameId { get; set; }
        public int ChildGameId { get; set; }
        // public GameDto ParentGame { get; set; }
        // public GameDto ChildGame { get; set; }
        public int GameSelfLinkTypeLookupId { get; set; }
    }
}