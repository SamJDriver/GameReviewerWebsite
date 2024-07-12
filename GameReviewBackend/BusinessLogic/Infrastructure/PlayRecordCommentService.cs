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
                throw new DgcException("Can't create play record comment. User not found.", DgcExceptionType.Unauthorized);
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
                throw new DgcException("Can't update Play Record comment. User not found.", DgcExceptionType.Unauthorized);
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

        public void Upvote(int playRecordCommentId, string? userId)
        {
            //TODO: Upvotes and downvotes will need to be separated into their own table for further validations. 
            // That way a user won't be able to spam upvote/downvote something.
            var existingPlayRecordComment = _genericRepository.GetSingleTracked<PlayRecordComments>(p => p.Id == playRecordCommentId);
            if (existingPlayRecordComment == default)
            {
                throw new DgcException("Can't find comment to upvote.", DgcExceptionType.ResourceNotFound);
            }

            if (userId == null)
            {
                throw new DgcException("No user found to upvote comment. Ensure you are logged in.", DgcExceptionType.Unauthorized);
            }

            existingPlayRecordComment.UpvoteCount += 1;
            _genericRepository.UpdateRecord(existingPlayRecordComment);

        }

        public void Downvote(int playRecordCommentId, string? userId)
        {
            var existingPlayRecordComment = _genericRepository.GetSingleTracked<PlayRecordComments>(p => p.Id == playRecordCommentId);
            if (existingPlayRecordComment == default)
            {
                throw new DgcException("Can't find comment to upvote.", DgcExceptionType.ResourceNotFound);
            }

            if (userId == null)
            {
                throw new DgcException("No user found to upvote comment. Ensure you are logged in.", DgcExceptionType.Unauthorized);
            }

            existingPlayRecordComment.DownvoteCount += 1;
            _genericRepository.UpdateRecord(existingPlayRecordComment);
        }
    }
}
