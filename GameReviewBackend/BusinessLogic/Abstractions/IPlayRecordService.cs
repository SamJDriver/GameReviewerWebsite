using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IPlayRecordService
    {
        public void CreatePlayRecord(PlayRecordDto playRecord);
        public void UpdatePlayRecord(PlayRecordDto playRecord);

    }
}
