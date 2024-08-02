using BusinessLogic.Abstractions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Repositories;
using Components.Exceptions;
using Mapster;

namespace BusinessLogic.Infrastructure
{
    public class CompanyService : ICompanyService
    {
        private readonly IGenericRepository<DockerDbContext> _genericRepository;
        public CompanyService(IGenericRepository<DockerDbContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public CompanyDto GetCompanyById(int companyId)
        {
            var company = _genericRepository.GetById<Companies>(companyId);
            if (company == null)
            {
                throw new DgcException("Company not found.", DgcExceptionType.ResourceNotFound);
            }

            var companyDto = company.Adapt<CompanyDto>();
            return companyDto;
        }
    }
}
