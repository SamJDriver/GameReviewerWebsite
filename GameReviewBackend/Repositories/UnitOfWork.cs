using System;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;

namespace Repositories
{
    public class UnitOfWork : IDisposable
    {
        private readonly DockerDbContext context;
        private GenericDataAccess<Artwork> artworkRepository;
        private GenericDataAccess<Companies> companiesRepository;
        private GenericDataAccess<Cover> coverRepository;
        private GenericDataAccess<Games> gamesRepository;
        private GenericDataAccess<GamesCompaniesLink> gamesCompaniesLinkRepository;
        private GenericDataAccess<GamesGenresLookupLink> gamesGenresLookupLinkRepository;
        private GenericDataAccess<GamesPlatformsLink> gamesPlatformsLinkRepository;
        private GenericDataAccess<GameSelfLink> gameSelfLinkRepository;
        private GenericDataAccess<GameSelfLinkTypeLookup> gameSelfLinkTypeLookupRepository;
        private GenericDataAccess<GenresLookup> genresLookupRepository;
        private GenericDataAccess<Platforms> platformsRepository;

        public UnitOfWork(DockerDbContext context)
        {
            this.context = context;
        }


        public GenericDataAccess<Artwork> ArtworkRepository
        {
            get
            { return this.artworkRepository == null ? new GenericDataAccess<Artwork>(context) : artworkRepository; }
        }

        public GenericDataAccess<Companies> CompaniesRepository
        {
            get
            { return this.companiesRepository == null ? new GenericDataAccess<Companies>(context) : companiesRepository; }
        }
        
        public GenericDataAccess<Cover> CoverRepository
        {
            get
            { return this.coverRepository == null ? new GenericDataAccess<Cover>(context) : coverRepository; }
        }
        public GenericDataAccess<Games> GamesRepository
        {
            get
            { return this.gamesRepository == null ? new GenericDataAccess<Games>(context) : gamesRepository; }
        }
        public GenericDataAccess<GamesCompaniesLink> GamesCompaniesLinkRepository
        {
            get
            { return this.gamesCompaniesLinkRepository == null ? new GenericDataAccess<GamesCompaniesLink>(context) : gamesCompaniesLinkRepository; }
        }
        public GenericDataAccess<GamesGenresLookupLink> GamesGenresLookupLinkRepository
        {
            get
            { return this.gamesGenresLookupLinkRepository == null ? new GenericDataAccess<GamesGenresLookupLink>(context) : gamesGenresLookupLinkRepository; }
        }
        public GenericDataAccess<GamesPlatformsLink> GamesPlatformsLinkRepository
        {
            get
            { return this.gamesPlatformsLinkRepository == null ? new GenericDataAccess<GamesPlatformsLink>(context) : gamesPlatformsLinkRepository; }
        }
        public GenericDataAccess<GameSelfLink> GameSelfLinkRepository
        {
            get
            { return this.gameSelfLinkRepository == null ? new GenericDataAccess<GameSelfLink>(context) : gameSelfLinkRepository; }
        }
        public GenericDataAccess<GameSelfLinkTypeLookup> GameSelfLinkTypeLookupRepository
        {
            get
            { return this.gameSelfLinkTypeLookupRepository == null ? new GenericDataAccess<GameSelfLinkTypeLookup>(context) : gameSelfLinkTypeLookupRepository; }
        }
        public GenericDataAccess<GenresLookup> GenresLookupRepository
        {
            get
            { return this.genresLookupRepository == null ? new GenericDataAccess<GenresLookup>(context) : genresLookupRepository; }
        }
        public GenericDataAccess<Platforms> PlatformsRepository
        {
            get
            { return this.platformsRepository == null ? new GenericDataAccess<Platforms>(context) : platformsRepository; }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}