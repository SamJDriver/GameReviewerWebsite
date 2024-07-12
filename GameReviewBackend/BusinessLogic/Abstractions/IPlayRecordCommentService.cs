using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IPlayRecordCommentService
    {
        public void CreatePlayRecordComment(CreatePlayRecordCommentDto playRecordComment, string? userId);
        public void UpdatePlayRecordComment(int playRecordCommentId, UpdatePlayRecordCommentDto playRecordComment, string? userId);

    }
}
