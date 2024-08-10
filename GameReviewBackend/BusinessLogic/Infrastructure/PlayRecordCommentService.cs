using BusinessLogic.Abstractions;
using Components.Exceptions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Mapster;
using Repositories;

namespace BusinessLogic.Infrastructure
{
    public class PlayRecordCommentService : IPlayRecordCommentService
    {
        private readonly IGenericRepository<DockerDbContext> _genericRepository;
        public PlayRecordCommentService(IGenericRepository<DockerDbContext> genericRepository)
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
            DockerDbContext.SetCreatedByUserId(userId);
            _genericRepository.InsertRecord(newplayRecordCommentEntity);

            var newCommentVote = new PlayRecordCommentVote()
            {
                PlayRecordCommentId = newplayRecordCommentEntity.Id,
                NumericalValue = 1,
            };
            DockerDbContext.SetCreatedByUserId(userId);
            _genericRepository.InsertRecord(newCommentVote);
        }

        public void UpdatePlayRecordComment(int playRecordCommentId, UpdatePlayRecordCommentDto playRecordComment, string? userId)
        {

            var existingPlayRecordComment = _genericRepository.GetSingleTracked<PlayRecordComments>(p => p.Id == playRecordCommentId);
            if (existingPlayRecordComment == default)
            {
                throw new DgcException("Can't update Play Record comment. Play Record comment not found.", DgcExceptionType.ResourceNotFound);
            }

            if (userId == null || existingPlayRecordComment.CreatedBy != userId)
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
            submitPlayRecordVote(true, playRecordCommentId, userId);
        }

        public void Downvote(int playRecordCommentId, string? userId)
        {
            submitPlayRecordVote(false, playRecordCommentId, userId);
        }

        private void submitPlayRecordVote(bool upvoteFlag, int playRecordCommentId, string? userId)
        {

            var numericalValue = upvoteFlag ? 1 : -1;

            var existingPlayRecordComment = _genericRepository.GetSingleTracked<PlayRecordComments>(p => p.Id == playRecordCommentId);
            if (existingPlayRecordComment == default)
            {
                throw new DgcException("Can't find comment to vote on.", DgcExceptionType.ResourceNotFound);
            }

            if (userId == null)
            {
                throw new DgcException("No user found to vote. Ensure you are logged in.", DgcExceptionType.Unauthorized);
            }

            var existingCommentVote = _genericRepository.GetSingleTracked<PlayRecordCommentVote>(p => p.CreatedBy == userId && p.PlayRecordCommentId == existingPlayRecordComment.Id);

            if (existingCommentVote != null)
            {
                // Already upvoted 1-> upvoted again = 0
                // Already downvoted -1 -> downvoted again = 0
                // Already upvoted 1 -> downvoted = -1
                // Already downvoted -1 -> upvoted = 1
                if (existingCommentVote.NumericalValue == numericalValue)
                {
                    _genericRepository.DeleteRecord(existingCommentVote);
                }
                else if (existingCommentVote.NumericalValue == numericalValue * -1)
                {
                    existingCommentVote.NumericalValue *= -1;
                    _genericRepository.UpdateRecord(existingCommentVote);

                }
                else
                {
                    existingCommentVote.NumericalValue = numericalValue;
                    _genericRepository.UpdateRecord(existingCommentVote);
                }
            }
            else
            {
                var newCommentVote = new PlayRecordCommentVote()
                {
                    PlayRecordCommentId = existingPlayRecordComment.Id,
                    NumericalValue = upvoteFlag ? 1 : -1,
                };

                DockerDbContext.SetCreatedByUserId(userId);
                _genericRepository.InsertRecord(newCommentVote);
            }

        }
    }
}
