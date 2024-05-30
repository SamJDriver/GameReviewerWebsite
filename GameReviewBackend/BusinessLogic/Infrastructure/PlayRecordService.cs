using BusinessLogic.Abstractions;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
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
        public void CreatePlayRecord(PlayRecordDto playRecord)
        {
            var existingUser = _genericRepository.GetById<Users>(playRecord.UserId);
            if (existingUser == default)
            {
                return;
            }

            var existingGame = _genericRepository.GetById<Games>(playRecord.GameId);
            if (existingGame == default)
            {
                return;
            }

            if (playRecord.Rating < 0 || playRecord.Rating > 100)
            {
                return;
            }

            var newPlayRecordEntity = new PlayRecords().Assign(playRecord);
            newPlayRecordEntity.CreatedBy = existingUser.Username;
            newPlayRecordEntity.CreatedDate = DateTime.Now;
            _genericRepository.InsertRecord(newPlayRecordEntity);  
        }

        public void UpdatePlayRecord(PlayRecordDto playRecord)
        {

            var existingPlayRecord = _genericRepository.GetSingleTracked<PlayRecords>(p =>p.Id == playRecord.Id);
            if (existingPlayRecord == default)
            {
                return;
            }

            //TODO validate that the user in the record is the logged in user.

            var existingUser = _genericRepository.GetById<Users>(existingPlayRecord.UserId);
            if (existingUser == default)
            {
                return;
            }

            var existingGame = _genericRepository.GetById<Games>(existingPlayRecord.GameId);
            if (existingGame == default)
            {
                return;
            }

            if (playRecord.Rating < 0 || playRecord.Rating > 100)
            {
                return;
            }

            existingPlayRecord.Assign(playRecord);
            existingPlayRecord.GameId = existingGame.Id;
            existingPlayRecord.UserId = existingUser.Id;
            existingPlayRecord.CreatedBy = existingUser.Username;
            existingPlayRecord.CreatedDate = DateTime.Now;

            //TODO overrite savechanges for this
            _genericRepository.UpdateRecord(existingPlayRecord);
        }
    }
}
