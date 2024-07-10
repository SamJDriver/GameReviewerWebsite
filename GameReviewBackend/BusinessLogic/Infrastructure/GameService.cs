using BusinessLogic.Abstractions;
using Components.Utilities;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Components.Exceptions;
using System.Reflection.Metadata;
using Components;

namespace BusinessLogic.Infrastructure
{
    public class GameService : IGameService
    {
        private readonly GenericRepository<DockerDbContext> _genericRepository;
        private readonly GameRepository _gameRepository;
        public GameService(GenericRepository<DockerDbContext> genericRepository, GameRepository gameRepository)
        {
            _genericRepository = genericRepository;
            _gameRepository = gameRepository;
        }

        public async Task<int> CreateGame(GameDto game, string userId)
        {
            if (!game.ReleaseDate.ValidateDateOnly())
            {
                throw new DgcException($"Invalid game release date. Ensure release date is between {Components.Constants.minimumReleaseYear} and {DateTime.Now}", DgcExceptionType.ArgumentOutOfRange);
            }

            var gameEntity = new Games().Assign(game);
            DockerDbContext.SetUsername(userId);

            //TODO add validation checks for game?
            int numberOfEntriesWritten = await _genericRepository.InsertRecordAsync(gameEntity);

            if (numberOfEntriesWritten < 1)
            {
                throw new DgcException("The game could not be added.", DgcExceptionType.InvalidOperation);
            }

            return gameEntity.Id;
        }
        public async Task<PagedResult<GameDto>?> GetAllGames(int pageIndex, int pageSize)
        {
            var query = _genericRepository.GetAll<Games>();

            var data = 
                    (await query
                    // .OrderBy(g => g.Title)
                    // .Where(g => g.Cover.Count > 0)
                    .Where(g => g.Title.Contains("League of Legends"))
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

        public async Task<PagedResult<GameDto>> SearchGames(string? searchTerm, int? genreId, int? releaseYear, int pageIndex, int pageSize)
        { 
            
            var genre = genreId != null ? _genericRepository.GetById<GenresLookup>(genreId.Value) : null;
            if (genreId != null && genre == null)
            {
                throw new DgcException("Genre not found.", DgcExceptionType.ResourceNotFound);
            }

            if (releaseYear != null && releaseYear < Constants.minimumReleaseYear || releaseYear > Constants.maximumReleaseYear)
            {
                throw new DgcException("Invalid release year provided.", DgcExceptionType.ArgumentOutOfRange);
            }

            var query = _gameRepository.SearchGames(searchTerm, genreId, releaseYear);

            var games = (await query
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize)
                        .ToListAsync())
                        .Select(g => new GameDto().Assign(g));

            return new PagedResult<GameDto>()
            {
                Data = games,
                TotalRowCount = query.Count(),
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
        }
    }
}
