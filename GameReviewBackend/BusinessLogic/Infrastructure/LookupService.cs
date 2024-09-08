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

        public DateRangeDto GetReleaseYearsRange()
        {
            var releaseYears = _genericRepository.GetAll<Games>().Select(g => g.ReleaseDate.Year);
            var minReleaseYear = releaseYears.Where(y => y > 1).Min();
            var maxReleaseYear = releaseYears.Max();

            return new DateRangeDto(){ StartYearLimit = minReleaseYear, EndYearLimit = maxReleaseYear };
        }
    }
}
