using System.ComponentModel.DataAnnotations;
using Components.Exceptions;
using DataAccess.Models.DockerDb;

namespace Components.Models
{
    public class Game_GetList_Dto : BaseDto<Game_GetList_Dto, Games>
    {
        public int GameId { get; set; }

        public int PlayRecordId { get; set; }

        [StringLength(255)]
        public string Title { get; set; } = default!;

        [StringLength(255)]
        public string CoverImageUrl { get; set; } = default!;

        public int Rating { get; set; } = default!;

        [StringLength(255)]
        public string? ReviewerName { get; set; } = default!;

        [StringLength(36)]
        public string ReviewerId { get; set; } = default!;

        public DateTime ReviewDate { get; set; }
    }
}
