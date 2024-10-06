using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;


namespace Repositories
{
    public class GameRepository : IGameRepository
    {

        private readonly DockerDbContext _dbContext;

        public GameRepository(DockerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Games> SearchGames(string? searchTerm, IEnumerable<int>? genreIds, DateTime? startReleaseDate, DateTime? endReleaseDate)
        {            

            // This is a cursed query. Proceed at your own risk.
            var query =
                from 
                    game in _dbContext.Games.Include(g => g.GamesGenresLookupLink)
                join
                    genreLink in _dbContext.GamesGenresLookupLink
                    on game.Id equals genreLink.GameId
                where 
                    game.ParentGameId == null
                    && (searchTerm == null || game.Title.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase))
                    && (genreIds == null || (game.GamesGenresLookupLink.Any() && 
                        (from genreId in genreIds

                        join genreLink in _dbContext.GamesGenresLookupLink
                        on genreId equals genreLink.GenreLookupId

                        where genreLink.GameId == game.Id
                        select genreLink).Count() == 1
                    ))
                    && (startReleaseDate == null || game.ReleaseDate >= DateOnly.FromDateTime(startReleaseDate.Value))
                    && (endReleaseDate == null || game.ReleaseDate <= DateOnly.FromDateTime(endReleaseDate.Value))
                orderby
                    game.ReleaseDate descending
                select
                    game;

            // var games = query.Distinct().ToList();

            // var gamesFilteredByGenres = _dbContext.Games.ToList().Where(
            //     game => genreIds == null 
            //     || (game.GamesGenresLookupLink.Any() 
            //         && genreIds.All(genreId => game.GamesGenresLookupLink.Select(g => g.GenreLookupId).Contains(genreId))));

            return query;
        }

        public IEnumerable<Games> SearchGamesSP(string? searchTerm, IEnumerable<int>? genreIds, DateTime? startReleaseDate, DateTime? endReleaseDate, int? pageIndex = 0, int? pageSize = 50)
        {

            var pSearchTerm = new MySqlParameter("pSearchTerm", searchTerm);
            var pGenreIds = new MySqlParameter("pGenreIds", genreIds == null ? null : string.Join(",", genreIds));
            var pStartReleaseDate = new MySqlParameter("pStartReleaseDate", startReleaseDate);
            var pEndReleaseDate = new MySqlParameter("pEndReleaseDate", endReleaseDate);

            IEnumerable<Games> games = _dbContext.Games.FromSql(@$"CALL spGameSearch(
                {pSearchTerm},
                {pGenreIds},
                {pStartReleaseDate},
                {pEndReleaseDate},
                {pageIndex},
                {pageSize})").ToList();
            
            return games;
        }

        public IQueryable<Games> GetFriendsGames(string userId)
        {
            var query = 
                from
                    game in _dbContext.Games.Include(g => g.PlayRecords)
                join
                    playRecord in _dbContext.PlayRecords
                    on game.Id equals playRecord.GameId
                join
                    userRelationship in _dbContext.UserRelationship
                    on playRecord.CreatedBy equals userRelationship.ChildUserId
                join
                    userRelationshipTypeLookup in _dbContext.UserRelationshipTypeLookup
                    on userRelationship.UserRelationshipTypeLookupId equals userRelationshipTypeLookup.Id
                where
                    userRelationshipTypeLookup.Code == Components.Constants.LookupCodes.UserRelationshipTypeLookup.FriendCode
                orderby
                    playRecord.CreatedDate descending,
                    playRecord.Rating descending
                select
                    game;
                
            return query;
        }
    }
}