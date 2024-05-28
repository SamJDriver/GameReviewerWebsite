using Components.Utilities;
using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface ICompanyService
    {
        public CompanyDto GetCompanyById(int companyId);
    }
}
