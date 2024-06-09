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
            return self;
        }
        public static PlayRecordComments Assign(this PlayRecordComments self, PlayRecordCommentDto playRecordComment)
        {
            self.UserId = playRecordComment.UserId;
            self.PlayRecordId = playRecordComment.PlayRecordId;
            self.CommentText = playRecordComment.CommentText;
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
            return self;
        }
    }
}
