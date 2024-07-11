using BusinessLogic.Abstractions;
using Components.Exceptions;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Mapster;
using Repositories;

namespace BusinessLogic.Infrastructure
{
    public class PlayRecordService : IPlayRecordService
    {
        private readonly GenericRepository<DockerDbContext> _genericRepository;
        public PlayRecordService(GenericRepository<DockerDbContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public void CreatePlayRecord(CreatePlayRecordDto playRecord, string? userId)
        {
            if (userId == null)
            {
                throw new DgcException("Can't create play record. User not found.", DgcExceptionType.ResourceNotFound);
            }

            var existingGame = _genericRepository.GetById<Games>(playRecord.GameId);
            if (existingGame == default)
            {
                throw new DgcException("Can't create play record. Game not found.", DgcExceptionType.ResourceNotFound);
            }

            if (playRecord.Rating < 0 || playRecord.Rating > 100)
            {
                throw new DgcException("Can't create play record. Rating out of range.", DgcExceptionType.ArgumentOutOfRange);
            }

            var newPlayRecordEntity = playRecord.Adapt<PlayRecords>();

            newPlayRecordEntity.UserId = userId;

            DockerDbContext.SetCreatedByUserId(userId);
            _genericRepository.InsertRecord(newPlayRecordEntity);  
        }

        public void UpdatePlayRecord(int playRecordId, UpdatePlayRecordDto playRecord, string? userId)
        {

            var existingPlayRecord = _genericRepository.GetSingleNoTrack<PlayRecords>(p => p.Id == playRecordId);
            if (existingPlayRecord == default)
            {
                throw new DgcException("Can't update Play Record. Play Record not found.", DgcExceptionType.ResourceNotFound);
            }
            
            if (userId == null || existingPlayRecord.UserId != userId)
            {
                throw new DgcException("Can't update Play Record. User not found.", DgcExceptionType.ResourceNotFound);
            }

            var existingGame = _genericRepository.GetById<Games>(existingPlayRecord.GameId);
            if (existingGame == default)
            {
                throw new DgcException("Can't update Play Record. Game not found.", DgcExceptionType.ResourceNotFound);
            }

            if (playRecord.Rating < 0 || playRecord.Rating > 100)
            {
                throw new DgcException("Can't update play record. Rating out of range.", DgcExceptionType.ArgumentOutOfRange);
            }


            playRecord.Adapt(existingPlayRecord);

            DockerDbContext.SetCreatedByUserId(userId);

            _genericRepository.UpdateRecord(existingPlayRecord);
        }
    }
}
