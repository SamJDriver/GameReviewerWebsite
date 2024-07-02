using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Components.Models
{
    public class ArtworkDto
    {
        public int? Id { get; set; }

        public int GameId { get; set; }

        public bool AlphaChannelFlag { get; set; }

        public bool AnimatedFlag { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public string ImageUrl { get; set; } = null!;

        [StringLength(25)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}