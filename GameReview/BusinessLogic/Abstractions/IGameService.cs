using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IGameService
    {
        public int CreateGame(GameDto game, out string? error);
        public IEnumerable<GameDto> GetGames();
    }
}
