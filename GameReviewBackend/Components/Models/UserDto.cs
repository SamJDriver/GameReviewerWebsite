using System.ComponentModel.DataAnnotations;
using Microsoft.Graph.Models;

namespace Components.Models
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public ProfilePhoto Photo { get; set; } = null!;
        public string Country { get; set; } = null!;

    }
}