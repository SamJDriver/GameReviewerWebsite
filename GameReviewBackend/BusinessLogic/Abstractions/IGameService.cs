using Components.Utilities;
using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IGameService
    {
        public int CreateGame(GameDto game);
        public Task<PagedResult<GameDto>?> GetAllGames(int pageIndex, int pageSize);
        public Task<GameDto> GetGameById(int gameId);
    }
}
