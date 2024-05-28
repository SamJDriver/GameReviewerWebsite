﻿using System.ComponentModel.DataAnnotations;
using Components.Exceptions;

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
    
        public bool DeveloperFlag { get; set; }
    
        public bool PublisherFlag { get; set; }
        
        [StringLength(25)]
        public string? ModifiedBy { get; set; }

        public DateTime RowStart { get; set; }
        public DateTime RowEnd { get; set; }
    }
}
