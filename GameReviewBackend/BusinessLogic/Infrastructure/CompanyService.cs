using BusinessLogic.Abstractions;
using Components.Utilities;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Components.Exceptions;

namespace BusinessLogic.Infrastructure
{
    public class CompanyService : ICompanyService
    {
        private readonly GenericRepository<DockerDbContext> _genericRepository;
        public CompanyService(GenericRepository<DockerDbContext> genericRepository)
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

            var companyDto = new CompanyDto().Assign(company);
            return companyDto;
        }
    }
}
