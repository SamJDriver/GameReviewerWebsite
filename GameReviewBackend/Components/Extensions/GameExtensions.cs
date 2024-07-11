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
            self.Description = game.Description;
            self.Artwork = game.Artwork != null ? game.Artwork.Select(a => new Artwork().Assign(a)).ToList() : null;
            self.Cover = game.Cover != null ? game.Cover.Select(c => new Cover().Assign(c)).ToList() : null;
            return self;
        }

        public static GameDto Assign(this GameDto self, Games game)
        {
            self.Id = game.Id;
            self.Title = game.Title;
            self.ReleaseDate = game.ReleaseDate;
            self.Description = game.Description;
            self.Artwork = game.Artwork != null ? game.Artwork.Select(a => new ArtworkDto().Assign(a)).ToList() : null;
            self.Cover = game.Cover != null ? game.Cover.Select(a => new CoverDto().Assign(a)).ToList() : null;
            // self.ParentLinks = game.GameSelfLinkParentGame != null ? game.GameSelfLinkParentGame.Select(g => new GameSelfLinkDto().Assign(g)).ToList() : null;
            // self.ChildLinks = game.GameSelfLinkChildGame != null ? game.GameSelfLinkChildGame.Select(g => new GameSelfLinkDto().Assign(g)).ToList() : null;
            return self;
        }

        public static PlayRecords Assign(this PlayRecords self, PlayRecordDto playRecord)
        {
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
            self.GameId = playRecord.GameId;
            self.CompletedFlag = playRecord.CompletedFlag;
            self.HoursPlayed = playRecord.HoursPlayed;
            self.Rating = playRecord.Rating;
            self.PlayDescription = playRecord.PlayDescription;
            return self;
        }

        public static GameSelfLink Assign(this GameSelfLink self, GameSelfLinkDto gameSelfLink)
        {
            self.ParentGameId = gameSelfLink.ParentGameId;
            self.ChildGameId = gameSelfLink.ChildGameId;
            self.GameSelfLinkTypeLookupId = gameSelfLink.GameSelfLinkTypeLookupId;
            return self;
        }

        public static GameSelfLinkDto Assign(this GameSelfLinkDto self, GameSelfLink gameSelfLink)
        {
            self.Id = gameSelfLink.Id;
            self.ParentGameId = gameSelfLink.ParentGameId;
            self.ChildGameId = gameSelfLink.ChildGameId;
            self.GameSelfLinkTypeLookupId = gameSelfLink.GameSelfLinkTypeLookupId;
            return self;
        }
    }
}
