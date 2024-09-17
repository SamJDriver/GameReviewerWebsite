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
        private readonly ILookupService _lookupService;

        public GameService(IGenericRepository<DockerDbContext> genericRepository, IGameRepository gameRepository, GraphServiceClient graphServiceClient, ILookupService lookupService)
        {
            _genericRepository = genericRepository;
            _gameRepository = gameRepository;
            _graphServiceClient = graphServiceClient;
            _lookupService = lookupService;
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
        public async Task<PagedResult<Game_Get_VanillaGame_Dto>?> GetAllGames(int pageIndex, int pageSize)
        {
            var query = _genericRepository.GetAll<Games>();

            var data =
                    (await query
                   .Skip(pageIndex * pageIndex)
                    .Take(pageSize)
                    .ToListAsync())
                    .Adapt<IEnumerable<Game_Get_VanillaGame_Dto>>();

            return new PagedResult<Game_Get_VanillaGame_Dto>()
            {
                Items = data,
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
                Items = data.Select(d =>  { 

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

        public async Task<PagedResult<Game_Get_VanillaGame_Dto>> SearchGames(string? searchTerm, IEnumerable<int>? genreIds, DateTime? startReleaseDate, DateTime? endReleaseDate, int pageIndex, int pageSize)
        {
            IEnumerable<GenresLookup?>? genres = genreIds != null ? genreIds.Select(g => _genericRepository.GetById<GenresLookup>(g)) : null;

            if ((genres ?? []).Any(g => g == null) && genreIds != null)
            {
                throw new DgcException("Invalid genre provided.", DgcExceptionType.ResourceNotFound);
            }

            DateRangeDto dateRange = _lookupService.GetReleaseYearsRange();
            if (startReleaseDate != null && (startReleaseDate.Value.Year < dateRange.StartYearLimit || startReleaseDate.Value.Year > dateRange.EndYearLimit))
            {
                throw new DgcException("Start date is out of valid date range.", DgcExceptionType.ArgumentOutOfRange);
            }

            if (endReleaseDate != null && (endReleaseDate.Value.Year < dateRange.StartYearLimit || endReleaseDate.Value.Year > dateRange.EndYearLimit))
            {
                throw new DgcException("End date is out of valid date range.", DgcExceptionType.ArgumentOutOfRange);
            }

            if (startReleaseDate != null && endReleaseDate != null && startReleaseDate > endReleaseDate)
            {
                throw new DgcException("Start date is after end date.", DgcExceptionType.ArgumentOutOfRange);
            }

            var query = _gameRepository.SearchGames(searchTerm, genreIds, startReleaseDate, endReleaseDate);

            var games = query
                        .Skip(pageIndex * pageSize)
                        .Take(pageSize)
                        .ToList()
                        .Adapt<IEnumerable<Game_Get_VanillaGame_Dto>>();

            return new PagedResult<Game_Get_VanillaGame_Dto>()
            {
                Items = games,
                TotalRowCount = query.Count(),
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
        }
    }
}
