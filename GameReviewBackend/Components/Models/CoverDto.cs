using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Models
{
    public class CoverDto : BaseDto<CoverDto, Cover>
    {
        public int? Id { get; set; }

        public int GameId { get; set; }

        public bool AlphaChannelFlag { get; set; }

        public bool AnimatedFlag { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}