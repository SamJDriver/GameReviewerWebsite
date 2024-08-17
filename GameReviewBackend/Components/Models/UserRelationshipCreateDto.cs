using System.ComponentModel.DataAnnotations;
using Microsoft.Graph.Models;

namespace Components.Models
{
    public class UserRelationship_Create_Dto
    {
        [StringLength(60)]
        public string ChildUserId { get; set; } = null!;
        public int UserRelationshipTypeLookupId { get; set; }
    }
}