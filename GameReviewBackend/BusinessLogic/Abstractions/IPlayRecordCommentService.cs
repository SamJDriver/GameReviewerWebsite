using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IPlayRecordCommentService
    {
        public void CreatePlayRecordComment(PlayRecordCommentDto playRecordComment);
        public void UpdatePlayRecordComment(PlayRecordCommentDto playRecordComment);

    }
}
