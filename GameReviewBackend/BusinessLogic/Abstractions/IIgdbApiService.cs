using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IIgdbApiService
    {
        public Task<string> QueryApi();

    }
}
