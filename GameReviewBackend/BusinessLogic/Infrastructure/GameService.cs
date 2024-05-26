using BusinessLogic.Abstractions;
using Components;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.EntityFrameworkCore;
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


        public async Task<PagedResult<GameDto>?> GetAllGames(int pageIndex, int pageSize)
        {
            var query = _genericRepository.GetAll<Games>();

            var data = 
                    await query
                    .OrderBy(g => g.Title)
                    .Skip(pageIndex*pageIndex)
                    .Take(pageSize)
                    .Select(g => new GameDto().Assign(g))
                    .ToListAsync();

            return new PagedResult<GameDto>()
            {
                Data = data,
                TotalRowCount = query.Count(),
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
        }
    }
}
