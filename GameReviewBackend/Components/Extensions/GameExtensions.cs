using Components.Models;
using DataAccess.Models.DockerDb;

namespace Components.Extensions
{
    public static class GameExtensions
    {
        public static Games Assign(this Games self, GameDto game)
        {
            self.Title = game.Title;
            self.ReleaseDate = game.ReleaseDate;
            self.ImageFilePath = game.ImageFilePath;
            self.Description = game.Description;
            self.CreatedBy = game.CreatedBy;
            self.CreatedDate = game.CreatedDate;
            self.ModifiedBy = game.ModifiedBy;
            self.ModifiedDate = game.ModifiedDate;
            self.ObsoleteFlag = game.ObsoleteFlag;
            self.ObsoleteDate = game.ObsoleteDate;
            return self;
        }

        public static GameDto Assign(this GameDto self, Games game)
        {
            self.Id = game.Id;
            self.Title = game.Title;
            self.ReleaseDate = game.ReleaseDate;
            self.ImageFilePath = game.ImageFilePath;
            self.Description = game.Description;
            self.CreatedBy = game.CreatedBy;
            self.CreatedDate = game.CreatedDate;
            self.ModifiedBy = game.ModifiedBy;
            self.ModifiedDate = game.ModifiedDate;
            self.ObsoleteFlag = game.ObsoleteFlag;
            self.ObsoleteDate = game.ObsoleteDate;
            return self;
        }

        public static PlayRecords Assign(this PlayRecords self, PlayRecordDto playRecord)
        {
            self.UserId = playRecord.UserId;
            self.GameId = playRecord.GameId;
            self.CompletedFlag = playRecord.CompletedFlag;
            self.HoursPlayed = playRecord.HoursPlayed;
            self.Rating = playRecord.Rating;
            self.PlayDescription = playRecord.PlayDescription;
            self.ModifiedBy = playRecord.ModifiedBy;
            self.ModifiedDate = playRecord.ModifiedDate;
            self.ObsoleteFlag = playRecord.ObsoleteFlag;
            self.ObsoleteDate = playRecord.ObsoleteDate;
            return self;
        }

        public static PlayRecordDto Assign(this PlayRecordDto self, PlayRecords playRecord)
        {
            self.Id = playRecord.Id;
            self.UserId = playRecord.UserId;
            self.GameId = playRecord.GameId;
            self.CompletedFlag = playRecord.CompletedFlag;
            self.HoursPlayed = playRecord.HoursPlayed;
            self.Rating = playRecord.Rating;
            self.PlayDescription = playRecord.PlayDescription;
            self.CreatedBy = playRecord.CreatedBy;
            self.CreatedDate = playRecord.CreatedDate;
            self.ModifiedBy = playRecord.ModifiedBy;
            self.ModifiedDate = playRecord.ModifiedDate;
            self.ObsoleteFlag = playRecord.ObsoleteFlag;
            self.ObsoleteDate = playRecord.ObsoleteDate;
            return self;
        }
    }
}
