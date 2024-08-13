using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.Graph.Models;

namespace Components.Models
{
    public class PlayRecord_GetById_Dto
    {
        public int Id { get; set; }
        
        public int GameId { get; set; }
        
        public string GameTitle { get; set; } = default!;
        
        public string GameDescription { get; set; } = default!;
        
        public string? CoverImageUrl { get; set; }
        
        public bool? CompletedFlag { get; set; }
        
        public int? HoursPlayed { get; set; }
        
        public int? Rating { get; set; }
        
        public string? PlayDescription { get; set; }

        [StringLength(36)]
        public string CreatedBy { get; set; } = default!;
        
        public DateTime CreatedDate { get; set; } = default;

        public IEnumerable<PlayRecordCommentDto> PlayRecordComments { get; set; } = default!; 
    }
}
