using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IIgdbApiService
    {
        public Task QueryApi();

        public Task<string> GetOneGame();
    }
}
