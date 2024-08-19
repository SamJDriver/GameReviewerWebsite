using DataAccess.Models.DockerDb;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts.DockerDb
{
    public interface IDockerDbContext
    {
        DbSet<Artwork> Artwork { get; set; }
        DbSet<Companies> Companies { get; set; }
        DbSet<Cover> Cover { get; set; }
        DbSet<GameSelfLink> GameSelfLink { get; set; }
        DbSet<GameSelfLinkTypeLookup> GameSelfLinkTypeLookup { get; set; }
        DbSet<Games> Games { get; set; }
        DbSet<GamesCompaniesLink> GamesCompaniesLink { get; set; }
        DbSet<GamesGenresLookupLink> GamesGenresLookupLink { get; set; }
        DbSet<GamesPlatformsLink> GamesPlatformsLink { get; set; }
        DbSet<GenresLookup> GenresLookup { get; set; }
        DbSet<Platforms> Platforms { get; set; }
        DbSet<PlayRecordCommentVote> PlayRecordCommentVote { get; set; }
        DbSet<PlayRecordComments> PlayRecordComments { get; set; }
        DbSet<PlayRecords> PlayRecords { get; set; }
        DbSet<UserRelationship> UserRelationship { get; set; }
        DbSet<UserRelationshipTypeLookup> UserRelationshipTypeLookup { get; set; }
        DbSet<Users> Users { get; set; }
        
        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void Dispose();
    }
}