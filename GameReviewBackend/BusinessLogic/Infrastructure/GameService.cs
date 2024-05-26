using BusinessLogic.Abstractions;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Repositories;

namespace BusinessLogic.Infrastructure
{
    public class GameService : IGameService
    {
        GenericRepository<DockerDbContext> _genericRepository;
        public GameService(GenericRepository<DockerDbContext> genericRepository)
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
            return _genericRepository.GetAll<Games>().Take(10).Select(g => new GameDto().Assign(g)).ToList(); ;
        }
    }
}
