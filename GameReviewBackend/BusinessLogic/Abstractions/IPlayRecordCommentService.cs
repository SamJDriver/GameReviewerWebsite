using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IPlayRecordCommentService
    {
        public void CreatePlayRecordComment(CreatePlayRecordCommentDto playRecordComment, string? userId);
        public void UpdatePlayRecordComment(int playRecordCommentId, UpdatePlayRecordCommentDto playRecordComment, string? userId);
        public void Upvote(int playRecordCommentId, string? userId);
        public void Downvote(int playRecordCommentId, string? userId);
        public void DeletePlayRecordComment(int playRecordCommentId, string? userId);
    }
}
