using Components.Utilities;
using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IGameService
    {
        public Task<int> CreateGame(GameDto game, string? userId);
        public Task<PagedResult<Game_Get_VanillaGame_Dto>?> GetAllGames(int pageIndex, int pageSize);
        public Task<PagedResult<Game_GetList_Dto>?> GetGamesPopularWithFriends(int pageIndex, int pageSize, string? userId);
        public Game_Get_ById_Dto GetGameById(int gameId);
        public Task<PagedResult<GameDto>> SearchGames(string? searchTerm, IEnumerable<int>? genreId, DateTime? startReleaseDate, DateTime? endReleaseDate, int pageIndex, int pageSize);
    }
}
