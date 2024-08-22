namespace Components.Models;

public class Game_Get_ById_CompanyLink_Dto
{
        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = default!;

        public string CompanyImageFilePath { get; set; } = default!;

        public bool DeveloperFlag { get; set; }

        public bool PublisherFlag { get; set; }
        
        public bool PortingFlag { get; set; }

        public bool SupportingFlag { get; set; }
}
