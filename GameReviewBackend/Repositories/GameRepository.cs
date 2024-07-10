using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.EntityFrameworkCore;


namespace Repositories
{
    public class GameRepository
    {

        private readonly DockerDbContext _dbContext;

        public GameRepository(DockerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Games> SearchGames(string? searchTerm, int? genreId, int? releaseYear)
        {
            if (searchTerm != null)
            {
                searchTerm = searchTerm.ToLower();
            }

            var query = 
                from 
                    game in _dbContext.Games
                join
                    gameGenresLookupLink in _dbContext.GamesGenresLookupLink.Include(g => g.GenreLookup)
                    on game.Id equals gameGenresLookupLink.GameId
                join
                    genre in _dbContext.GenresLookup
                    on gameGenresLookupLink.GenreLookupId equals genre.Id
                where 
                    game.ParentId == null  
                    && (searchTerm != null ? game.Title.ToLower().Contains(searchTerm) : true)
                    && (genreId != null ? genre.Id == genreId : true)
                    && (releaseYear != null ? game.ReleaseDate.Year == releaseYear : true)
                orderby
                    game.ReleaseDate descending
                select
                    game;

            return query;
        }
    }
}