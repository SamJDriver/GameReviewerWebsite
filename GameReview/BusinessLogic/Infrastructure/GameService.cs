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

        public IEnumerable<GameDto> GetGames()
        {
            var xd = _genericRepository.GetAll<TestGame>().ToList();

            var games = _genericRepository.GetAll<GameRaw>().Select(g => new GameDto().Assign(g)).ToList();
            return games;
        }
    }
}
