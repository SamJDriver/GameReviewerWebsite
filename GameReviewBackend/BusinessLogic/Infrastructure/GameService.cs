using BusinessLogic.Abstractions;
using Components.Utilities;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Components.Exceptions;

namespace BusinessLogic.Infrastructure
{
    public class GameService : IGameService
    {
        private readonly GenericRepository<DockerDbContext> _genericRepository;
        public GameService(GenericRepository<DockerDbContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public int CreateGame(GameDto game)
        {
            if (!game.ReleaseDate.ValidateDateOnly())
            {
                throw new DgcException($"Invalid game release date. Ensure release date is between {Components.Constants.minimumReleaseYear} and {DateTime.Now}", DgcExceptionType.ArgumentOutOfRange);
            }

            var gameEntity = new Games().Assign(game);

            //TODO add validation checks for game?
            _genericRepository.InsertRecord(gameEntity);
            return gameEntity.Id;
        }
        public async Task<PagedResult<GameDto>?> GetAllGames(int pageIndex, int pageSize)
        {
            var query = _genericRepository.GetAll<Games>();

            var data = 
                    (await query
                    .OrderBy(g => g.Title)
                    .Skip(pageIndex*pageIndex)
                    .Take(pageSize)
                    .ToListAsync())
                    .Select(g => new GameDto().Assign(g));

            return new PagedResult<GameDto>()
            {
                Data = data,
                TotalRowCount = query.Count(),
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
        }
        
        public GameDto GetGameById(int gameId)
        {
            var game = _genericRepository.GetById<Games>(gameId);
            if (game == null)
            {
                throw new DgcException("Can't retrieve game. Id not found.", DgcExceptionType.ResourceNotFound);
            }

            var gameDto = new GameDto().Assign(game);
            return gameDto;
        }
    }
}
