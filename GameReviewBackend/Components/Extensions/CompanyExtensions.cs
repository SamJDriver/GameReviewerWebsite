using Components.Models;
using DataAccess.Models.DockerDb;

namespace Components.Extensions
{
    public static class CompanyExtensions
    {
        public static Companies Assign(this Companies self, CompanyDto company)
        {
            self.Name = company.Name;
            self.FoundedDate = company.FoundedDate;
            self.ImageFilePath = company.ImageFilePath;
            self.DeveloperFlag = company.DeveloperFlag;
            self.PublisherFlag = company.PublisherFlag;
            return self;
        }

        public static CompanyDto Assign(this CompanyDto self, Companies company)
        {
            self.Id = company.Id;
            self.Name = company.Name;
            self.FoundedDate = company.FoundedDate;
            self.ImageFilePath = company.ImageFilePath;
            self.DeveloperFlag = company.DeveloperFlag;
            self.PublisherFlag = company.PublisherFlag;
            self.CreatedBy = company.CreatedBy;
            self.CreatedDate = company.CreatedDate;
            return self;
        }
    }
}
