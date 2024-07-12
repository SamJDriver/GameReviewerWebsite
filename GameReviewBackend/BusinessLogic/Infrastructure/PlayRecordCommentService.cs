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
    public class PlayRecordCommentService : IPlayRecordCommentService
    {
        private readonly GenericRepository<DockerDbContext> _genericRepository;
        public PlayRecordCommentService(GenericRepository<DockerDbContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public void CreatePlayRecordComment(CreatePlayRecordCommentDto playRecordComment, string? userId)
        {
            if (userId == null)
            {
                throw new DgcException("Can't create play record comment. User not found.", DgcExceptionType.ResourceNotFound);
            }

            var existingPlayRecord = _genericRepository.GetById<PlayRecords>(playRecordComment.PlayRecordId);
            if (existingPlayRecord == default)
            {
                throw new DgcException("Can't create play record. Play record not found.", DgcExceptionType.ResourceNotFound);
            }

            var newplayRecordCommentEntity = playRecordComment.Adapt<PlayRecordComments>();
            newplayRecordCommentEntity.UserId = userId;

            DockerDbContext.SetCreatedByUserId(userId);
            _genericRepository.InsertRecord(newplayRecordCommentEntity);  
        }

        public void UpdatePlayRecordComment(int playRecordCommentId, UpdatePlayRecordCommentDto playRecordComment, string? userId)
        {

            var existingPlayRecordComment = _genericRepository.GetSingleTracked<PlayRecordComments>(p =>p.Id == playRecordCommentId);
            if (existingPlayRecordComment == default)
            {
                throw new DgcException("Can't update Play Record comment. Play Record comment not found.", DgcExceptionType.ResourceNotFound);
            }

            if (userId == null || existingPlayRecordComment.UserId != userId)
            {
                throw new DgcException("Can't update Play Record comment. User not found.", DgcExceptionType.ResourceNotFound);
            }

            var existingPlayRecord = _genericRepository.GetSingleNoTrack<PlayRecords>(p => p.Id == existingPlayRecordComment.PlayRecordId);
            if (existingPlayRecord == default || existingPlayRecordComment.PlayRecordId != existingPlayRecord.Id)
            {
                throw new DgcException("Can't find play record with comment.", DgcExceptionType.ResourceNotFound);
            }

            playRecordComment.Adapt(existingPlayRecordComment);
            
            DockerDbContext.SetCreatedByUserId(userId);
            _genericRepository.UpdateRecord(existingPlayRecordComment);
        }
    }
}
