using BusinessLogic.Abstractions;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts;
using DataAccess.Models;
using Repositories;

namespace BusinessLogic.Infrastructure
{
    public class GameService : IGameService
    {
        GenericRepository<NickDbContext> _genericRepository;
        public GameService(GenericRepository<NickDbContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public int CreateGame(GameDto game, out string? error)
        {
            var gameEntity = new Games().Assign(game);
            //TODO add validation checks for game?
            _genericRepository.InsertRecord(gameEntity);
            error = null;
            return gameEntity.Id;
        }


        public IEnumerable<GameDto> GetGames()
        {
            return _genericRepository.GetAll<Games>().Select(g => new GameDto().Assign(g)).ToList(); ;
        }
    }
}
