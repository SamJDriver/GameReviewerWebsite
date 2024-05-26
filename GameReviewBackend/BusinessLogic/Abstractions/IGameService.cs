using Components;
using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IGameService
    {
        public int CreateGame(GameDto game, out string? error);
        public Task<PagedResult<GameDto>?> GetAllGames(int pageIndex, int pageSize);
    }
}
