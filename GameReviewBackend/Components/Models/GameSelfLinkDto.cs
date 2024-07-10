using System.ComponentModel.DataAnnotations;

namespace Components.Models
{
    public class GameSelfLinkDto
    {
        public int Id { get; set; }
        public int ParentGameId { get; set; }
        public GameDto ParentGame { get; set; }
        public int ChildGameId { get; set; }
        public GameDto ChildGame { get; set; }
        public int GameSelfLinkTypeLookupId { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}