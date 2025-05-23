﻿using System.ComponentModel.DataAnnotations;

namespace Components.Models
{
    public class CompanyDto
    {
        public int Id { get; set; }
    
        [StringLength(255)]
        public string Name { get; set; } = null!;
    
        public DateOnly FoundedDate { get; set; }
    
        [StringLength(255)]
        public string? ImageFilePath { get; set; }
        
        [StringLength(25)]
        public string CreatedBy { get; set; } = default!;

        public DateTime CreatedDate { get; set; } = default;
    }
}
