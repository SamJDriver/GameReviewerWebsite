using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface ILookupService
    {
        public IEnumerable<GenreLookupDto> GetGenreLookups();
        public DateRangeDto GetReleaseYearsRange();

    }
}
