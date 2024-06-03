using BusinessLogic.Abstractions;
using Components.Exceptions;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
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
        public void CreatePlayRecordComment(PlayRecordCommentDto playRecordComment)
        {
            var existingUser = _genericRepository.GetById<Users>(playRecordComment.UserId);
            if (existingUser == default)
            {
                throw new DgcException("Can't create play record comment. User not found.", DgcExceptionType.ResourceNotFound);
            }

            var existingPlayRecord = _genericRepository.GetById<PlayRecords>(playRecordComment.PlayRecordId);
            if (existingPlayRecord == default)
            {
                throw new DgcException("Can't create play record. Play record not found.", DgcExceptionType.ResourceNotFound);
            }

            

            var newplayRecordCommentEntity = new PlayRecordComments().Assign(playRecordComment);
            _genericRepository.InsertRecord(newplayRecordCommentEntity);  
        }

        public void UpdatePlayRecordComment(PlayRecordCommentDto playRecordComment)
        {

            var existingPlayRecordComment = _genericRepository.GetSingleTracked<PlayRecordComments>(p =>p.Id == playRecordComment.Id);
            if (existingPlayRecordComment == default)
            {
                throw new DgcException("Can't update Play Record comment. Play Record comment not found.", DgcExceptionType.ResourceNotFound);
            }

            //TODO validate that the user in the record is the logged in user.

            var existingUser = _genericRepository.GetById<Users>(existingPlayRecordComment.UserId);
            if (existingUser == default)
            {
                throw new DgcException("Can't update Play Record comment. User not found.", DgcExceptionType.ResourceNotFound);
            }

            existingPlayRecordComment.Assign(playRecordComment);
            existingPlayRecordComment.PlayRecordId = existingPlayRecordComment.Id;
            existingPlayRecordComment.UserId = existingUser.Id;

            //TODO overrite savechanges for this
            _genericRepository.UpdateRecord(existingPlayRecordComment);
        }
    }
}
