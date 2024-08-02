using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;

namespace Repositories;

public interface IGameRepository
{
        IQueryable<Games> SearchGames(string? searchTerm, int? genreId, int? releaseYear);
        IQueryable<Games> GetFriendsGames(string userId);
}
