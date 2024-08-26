using System.ComponentModel.DataAnnotations;

namespace Components.Models;

public class Game_Get_VanillaGame_Dto 
{
    public int Id { get; set; }

    [StringLength(255)]
    public string? Title { get; set; }

    [StringLength(255)]
    public string? CoverImageUrl { get; set; }
}

