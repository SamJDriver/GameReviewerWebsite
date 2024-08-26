using System.ComponentModel.DataAnnotations;
using DataAccess.Models.DockerDb;

namespace Components.Models;

public class PlatformDto : BaseDto<PlatformDto, Platforms> 
{
  public int Id { get; set; }

  [StringLength(255)]
  public string Name { get; set; } = null!;
  
  public DateOnly ReleaseDate { get; set; }

  [StringLength(255)]
  public string ImageFilePath { get; set; } = null!;
}

