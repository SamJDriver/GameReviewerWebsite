using BusinessLogic.Abstractions;
using Components.Utilities;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Components.Exceptions;
using Mapster;
using Microsoft.Graph;
using Microsoft.Graph.DirectoryObjects.GetByIds;

namespace BusinessLogic.Infrastructure
{
    public class GameService : IGameService
    {
        private readonly IGenericRepository<DockerDbContext> _genericRepository;
        private readonly IGameRepository _gameRepository;
        private readonly GraphServiceClient _graphServiceClient;

        public GameService(IGenericRepository<DockerDbContext> genericRepository, IGameRepository gameRepository, GraphServiceClient graphServiceClient)
        {
            _genericRepository = genericRepository;
            _gameRepository = gameRepository;
            _graphServiceClient = graphServiceClient;
        }

        public async Task<int> CreateGame(GameDto game, string? userId)
        {
            if (!game.ReleaseDate.ValidateDateOnly())
            {
                throw new DgcException($"Invalid game release date. Ensure release date is between {Components.Constants.MinimumReleaseYear} and {new DateTime(Components.Constants.MaximumReleaseYear, 1, 1)}", DgcExceptionType.ArgumentOutOfRange);
            }

            if (userId == null)
            {
                throw new DgcException("Can't create game. No user logged in.", DgcExceptionType.Unauthorized);
            }

            var gameEntity = game.Adapt<Games>();

            DockerDbContext.SetCreatedByUserId(userId);
            var createdGameCount = await _genericRepository.InsertRecordAsync(gameEntity);

            if (createdGameCount < 1)
            {
                throw new DgcException("Can't create game. Game not created.", DgcExceptionType.InvalidOperation);
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
                    // .Where(g => g.Title.Contains("Halo"))
                    .Skip(pageIndex * pageIndex)
                    .Take(pageSize)
                    .ToListAsync())
                    .Adapt<IEnumerable<GameDto>>();

            return new PagedResult<GameDto>()
            {
                Data = data,
                TotalRowCount = query.Count(),
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
        }

        public async Task<PagedResult<Game_GetList_Dto>?> GetGamesPopularWithFriends(int pageIndex, int pageSize, string? userId)
        {
            if (userId is null)
            {
                throw new DgcException("Can't view friend's games. No user logged in.", DgcExceptionType.Unauthorized);
            }

            var query = _gameRepository.GetFriendsGames(userId);
            var data = (await query
                    .Skip(pageIndex * pageIndex)
                    .Take(pageSize)
                    .ToListAsync())
                    .Adapt<Game_GetList_Dto[]>();

            if (data.Length == 0)
            {
                return null;
            }

            var requestBody = new GetByIdsPostRequestBody()
            { 
                Ids = data.Select(d => d.ReviewerId).ToList<string>(),
                Types = new List<string> { "user" }
            };
            
            var result = (await _graphServiceClient.DirectoryObjects.GetByIds.PostAsGetByIdsPostResponseAsync(requestBody))?.Value;
            
            return new PagedResult<Game_GetList_Dto>()
            {
                Data = data.Select(d =>  { 

                    d.ReviewerName = (result?.FirstOrDefault(r => r.Id == d.ReviewerId) as Microsoft.Graph.Models.User)?.DisplayName;
                    return d; 
                }),
                TotalRowCount = query.Count(),
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
        }

        public Game_Get_ById_Dto GetGameById(int gameId)
        {
            var game = _genericRepository.GetById<Games>(gameId);
            if (game == null)
            {
                throw new DgcException("Can't retrieve game. Id not found.", DgcExceptionType.ResourceNotFound);
            }

            var gameDto = game.Adapt<Game_Get_ById_Dto>();
            return gameDto;
        }

        public async Task<PagedResult<GameDto>> SearchGames(string? searchTerm, int? genreId, int? releaseYear, int pageIndex, int pageSize)
        {
            var genre = genreId != null ? _genericRepository.GetById<GenresLookup>(genreId.Value) : null;
            if (genreId != null && genre == null)
            {
                throw new DgcException("Genre not found.", DgcExceptionType.ResourceNotFound);
            }

            if (releaseYear != null && releaseYear < Components.Constants.MinimumReleaseYear || releaseYear > Components.Constants.MaximumReleaseYear)
            {
                throw new DgcException("Invalid release year provided.", DgcExceptionType.ArgumentOutOfRange);
            }

            var query = _gameRepository.SearchGames(searchTerm, genreId, releaseYear);

            var games = query
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize)
                        .ToList()
                        .Adapt<IEnumerable<GameDto>>();

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
