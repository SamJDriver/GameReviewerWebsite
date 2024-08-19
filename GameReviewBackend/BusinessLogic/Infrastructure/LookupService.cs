using BusinessLogic.Abstractions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Mapster;
using Repositories;

namespace BusinessLogic.Infrastructure
{
    public class LookupService : ILookupService
    {
        private readonly IGenericRepository<DockerDbContext> _genericRepository;

        public LookupService(IGenericRepository<DockerDbContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public IEnumerable<GenreLookupDto> GetGenreLookups()
        {
            var genreLookups = _genericRepository.GetAll<GenresLookup>().Select(g => g.Adapt<GenreLookupDto>()).ToArray();
            return genreLookups;
        }
    }
}