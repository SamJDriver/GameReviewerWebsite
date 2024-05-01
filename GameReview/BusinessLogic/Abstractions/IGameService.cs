using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IGameService
    {
        public IEnumerable<GameDto> GetGames();
    }
}
