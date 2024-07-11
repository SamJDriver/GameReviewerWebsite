using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IPlayRecordService
    {
        public void CreatePlayRecord(CreatePlayRecordDto playRecord, string? userId);
        public void UpdatePlayRecord(int playRecordId, UpdatePlayRecordDto playRecord, string? userId);

    }
}
