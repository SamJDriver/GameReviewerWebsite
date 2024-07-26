using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface IPlayRecordService
    {
        public Task<IEnumerable<PlayRecord_GetSelf_Dto>> GetSelfPlayRecords(string? userId);
        public Task<PlayRecord_GetById_Dto> GetPlayRecordById(int playRecordId, string? userId);
        public void CreatePlayRecord(CreatePlayRecordDto playRecord, string? userId);
        public void UpdatePlayRecord(int playRecordId, UpdatePlayRecordDto playRecord, string? userId);

    }
}
