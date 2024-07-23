using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IPlayRecordService
    {
        public Task GetPlayRecords(string? userId);
        public void CreatePlayRecord(CreatePlayRecordDto playRecord, string? userId);
        public void UpdatePlayRecord(int playRecordId, UpdatePlayRecordDto playRecord, string? userId);

    }
}
