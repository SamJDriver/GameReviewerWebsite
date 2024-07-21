using System.ComponentModel.DataAnnotations;
using Microsoft.Graph.Models;

namespace Components.Models
{
    public class UserRelationshipCreateDto
    {
        [StringLength(100)]
        public string ChildUserId { get; set; } = null!;
        public int UserRelationshipTypeLookupId { get; set; }
    }
}