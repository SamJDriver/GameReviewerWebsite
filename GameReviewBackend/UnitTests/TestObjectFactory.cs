using Bogus;
using Components.Models;
using DataAccess.Models.DockerDb;

namespace UnitTests
{
    internal class TestObjectFactory
    {
        protected static readonly Faker _faker = new Faker();

        internal static Games GetMockGameEntity(IEnumerable<Cover>? covers = default)
        {
            GamesGenresLookupLink genre = new()
            {
                Id = 0,
                GameId = 0,
                GenreLookupId = _faker.Random.Number(1, 100)

            };

            var gameEntity = new Games()
            {
                Id = _faker.Random.Number(1, 500000),
                Title = _faker.Random.String(0, 255),
                ReleaseDate = DateOnly.FromDateTime(_faker.Date.Between(new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), new DateTime(Components.Constants.MaximumReleaseYear, 1, 1))),
                Description = _faker.Random.String(0, 65535),
                GamesGenresLookupLink = [genre],
                CreatedBy = Guid.NewGuid().ToString(),
                Cover = covers?.ToList()
            };

            return gameEntity;
        }

        internal static GameDto GetMockGameDto()
        {
            GameGenresLookupLinkDto genre = new()
            {
                Id = 0,
                GameId = 0,
                GenreLookupId = _faker.Random.Number(1, 100)

            };

            var gameEntity = new GameDto()
            {
                Id = _faker.Random.Number(1, 500000),
                Title = _faker.Random.String(0, 255),
                ReleaseDate = DateOnly.FromDateTime(_faker.Date.Between(new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), new DateTime(Components.Constants.MaximumReleaseYear, 1, 1))),
                Description = _faker.Random.String(0, 65535),
                GamesGenresLookupLink = [genre],
            };

            return gameEntity;
        }

        internal static CreatePlayRecordDto GetMockCreatePlayRecordDto(
            int? gameId = default,
            bool? completeFlag = default,
            int? hoursPlayed = default,
            int? rating = default,
            string? playDescription = default
        )
        {
            return new CreatePlayRecordDto
            {
                GameId = gameId ?? _faker.Random.Number(1, 500000),
                CompletedFlag = completeFlag ?? _faker.Random.Bool(),
                HoursPlayed = hoursPlayed ?? _faker.Random.Number(1, 100),
                Rating = rating ?? _faker.Random.Number(0, 100),
                PlayDescription = playDescription ?? _faker.Random.String(0, 65535),
            };
        }

        internal static UpdatePlayRecordDto GetMockUpdatePlayRecordDto(
            bool? completeFlag = default,
            int? hoursPlayed = default,
            int? rating = default,
            string? playDescription = default
        )
        {
            return new UpdatePlayRecordDto
            {
                CompletedFlag = completeFlag ?? _faker.Random.Bool(),
                HoursPlayed = hoursPlayed ?? _faker.Random.Number(1, 100),
                Rating = rating ?? _faker.Random.Number(0, 100),
                PlayDescription = playDescription ?? _faker.Random.String(0, 65535),
            };
        }

        internal static Games GetMockFriendsGameEntity(string userId, string friendId)
        {

            // Set up friend relationship
            var friendRelationshipTypeLookupId = _faker.Random.Number(1, 100);
            UserRelationshipTypeLookup userRelationshipType = new()
            {
                Id = friendRelationshipTypeLookupId,
                Name = "Friend",
                Code = Components.Constants.LookupCodes.UserRelationshipTypeLookup.FriendCode,
                Description = "Friend",
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            };

            DataAccess.Models.DockerDb.UserRelationship userRelationship = new()
            {
                Id = _faker.Random.Number(1, 500000),
                ChildUserId = friendId,
                UserRelationshipTypeLookupId = friendRelationshipTypeLookupId,
                CreatedBy = userId,
                CreatedDate = DateTime.Now
            };

            // Create friend's play record
            var gameId = _faker.Random.Number(1, 500000);

            PlayRecords playRecord = new()
            {
                Id = _faker.Random.Number(1, 500000),
                GameId = gameId,
                Rating = _faker.Random.Number(1, 100),
                CreatedBy = friendId,
                CreatedDate = DateTime.Now
            };


            GamesGenresLookupLink genre = new()
            {
                Id = 0,
                GameId = 0,
                GenreLookupId = _faker.Random.Number(1, 100)
            };

            Games gameEntity = new()
            {
                Id = gameId,
                Title = _faker.Random.String(0, 255),
                ReleaseDate = DateOnly.FromDateTime(_faker.Date.Between(new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), new DateTime(Components.Constants.MaximumReleaseYear, 1, 1))),
                Description = _faker.Random.String(0, 65535),
                GamesGenresLookupLink = [genre],
                CreatedBy = Guid.NewGuid().ToString(),
                PlayRecords = [playRecord],
            };

            return gameEntity;
        }

        internal static DataAccess.Models.DockerDb.UserRelationship GetMockUserRelationship(
            int? id = default,
            string? childUserId = default,
            int? userRelationshipTypeLookupId = default,
            string? createdBy = default,
            DateTime? createdDate = default,
            UserRelationshipTypeLookup? userRelationshipTypeLookup = default
        )
        {
            DataAccess.Models.DockerDb.UserRelationship userRelationship = new()
            {
                Id = id ?? _faker.Random.Number(1, 100),
                ChildUserId = childUserId ?? _faker.Random.Guid().ToString(),
                UserRelationshipTypeLookupId = userRelationshipTypeLookupId ?? _faker.Random.Number(1, 100),
                CreatedBy = createdBy ?? _faker.Random.Guid().ToString(),
                CreatedDate = createdDate ?? DateTime.Now,
                UserRelationshipTypeLookup = userRelationshipTypeLookup ?? GetMockUserRelationshipTypeLookup()
            };
            return userRelationship;
        }

        internal static UserRelationshipTypeLookup GetMockUserRelationshipTypeLookup(
            int? id = default,
            string? name = default,
            string? code = default,
            string? description = default,
            string? createdBy = default,
            DateTime? createdDate = default
        )
        {
            UserRelationshipTypeLookup userRelationshipType = new()
            {
                Id = id ?? _faker.Random.Number(1, 100),
                Name = name ?? _faker.Random.String(0, 255),
                Code = code ?? _faker.Random.String(0, 8),
                Description = description ?? _faker.Random.String(0, 255),
                CreatedBy = createdBy ?? _faker.Random.Guid().ToString(),
                CreatedDate = createdDate ?? DateTime.Now
            };
            return userRelationshipType;
        }

        internal static Game_PlayRecordList_Dto GetMockGameGetListDto()
        {
            Game_PlayRecordList_Dto game = new()
            {
                GameId = _faker.Random.Number(1, 500000),
                PlayRecordId = _faker.Random.Number(1, 500000),
                Title = _faker.Random.String(0, 255),
                CoverImageUrl = _faker.Random.String(0, 255),
                // Rating = _faker.Random.Number(1, 100),
                // ReviewerName = _faker.Random.String(0, 255),
                // ReviewerId = Guid.NewGuid().ToString(),
                // ReviewDate = _faker.Date.Between(new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), DateTime.Now)
            };
            return game;
        }

        internal static GenresLookup GetMockGenresLookup()
        {
            GenresLookup genre = new()
            {
                Id = _faker.Random.Number(1, 100),
                Name = _faker.Random.String(0, 255),
                Code = _faker.Random.String(0, 8),
                Description = _faker.Random.String(0, 255),
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            };
            return genre;
        }

        internal static PlayRecords GetMockPlayRecord(
            int? id = default,
            int? gameId = default, 
            int? rating = default, 
            string? createdBy = default, 
            DateTime? createdDate = default,
            Games? game = default,
            IEnumerable<PlayRecordComments>? playRecordComments = default)
        {
            PlayRecords playRecord = new()
            {
                Id = id ?? _faker.Random.Number(1, 500000),
                GameId = game != null ? game.Id : gameId ?? _faker.Random.Number(1, 500000),
                HoursPlayed = _faker.Random.Number(1, 1000),
                PlayDescription = _faker.Random.String(0, 65535),
                CompletedFlag = _faker.Random.Bool(),
                Rating = rating ?? _faker.Random.Number(1, 100),
                CreatedBy = createdBy ?? Guid.NewGuid().ToString(),
                CreatedDate = createdDate ?? _faker.Date.Between(DateTime.Now, new DateTime(3000, 1, 1)),
                Game = game,
                PlayRecordComments = playRecordComments?.ToList()
            };
            return playRecord;
        }

        internal static Cover GetMockCover()
        {
            Cover cover = new()
            {
                Id = _faker.Random.Number(1, 500000),
                GameId = _faker.Random.Number(1, 500000),
                AlphaChannelFlag = _faker.Random.Bool(),
                AnimatedFlag = _faker.Random.Bool(),
                Height = _faker.Random.Number(1, 5000),
                Width = _faker.Random.Number(1, 5000),
                ImageUrl = _faker.Random.String(0, 255),
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            };
            return cover;
        }

        internal static PlayRecordComments GetMockPlayRecordComment(
            int? id = default,
            int? playRecordId = default,
            string? commentText = default,
            string? createdBy = default,
            DateTime? createdDate = default,
            PlayRecords? playRecord = default
        )
        {
            PlayRecordComments review = new()
            {
                Id = id ?? _faker.Random.Number(1, 500000),
                PlayRecordId = playRecordId ?? _faker.Random.Number(1, 500000),
                CommentText = commentText ?? _faker.Random.String(0, 65535),
                CreatedBy = createdBy ?? _faker.Random.Guid().ToString(),
                CreatedDate = createdDate ?? DateTime.Now,
                PlayRecord = playRecord 
            };
            return review;
        }

        internal static CreatePlayRecordCommentDto GetMockCreatePlayRecordCommentDto()
        {
            CreatePlayRecordCommentDto review = new()
            {
                PlayRecordId = _faker.Random.Number(1, 500000),
                CommentText = _faker.Random.String(0, 65535),
            };
            return review;
        }

        internal static UpdatePlayRecordCommentDto GetMockUpdatePlayRecordCommentDto(
            string? commentText = default
        )
        {
            UpdatePlayRecordCommentDto review = new()
            {
                CommentText = commentText ?? _faker.Random.String(0, 65535)
            };
            return review;
        }

        internal static PlayRecordCommentVote GetMockPlayRecordCommentVote(
            int? id = default,
            int? playRecordCommentId = default,
            int? numericalValue = default,
            string? emojiValue = default,
            string? createdBy = default,
            DateTime? createdDate = default
        )
        {
            PlayRecordCommentVote vote = new()
            {
                Id = id ?? _faker.Random.Number(1, 500000),
                PlayRecordCommentId = playRecordCommentId ?? _faker.Random.Number(1, 500000),
                NumericalValue = numericalValue ?? _faker.Random.Int(-1, 1),
                EmojiValue = emojiValue ?? _faker.Random.String(0, 255),
                CreatedBy = createdBy ?? _faker.Random.Guid().ToString(),
                CreatedDate = createdDate ?? DateTime.Now
            };
            return vote;
        }

        internal static GenresLookup GetMockGenresLookupDto(
            int? id = default,
            string? name = default,
            string? code = default,
            string? description = default
        )
        {
            GenresLookup genre = new()
            {
                Id = id ?? _faker.Random.Number(1, 100),
                Name = name ?? _faker.Random.String(0, 255),
                Code = code ?? _faker.Random.String(0, 8),
                Description = description ?? _faker.Random.String(0, 255)
            };
            return genre;
        }

        internal static Companies GetMockCompany(
            int? id = default,
            string? name = default,
            DateOnly? foundedDate = default,
            string? imageFilePath = default,
            bool? developerFlag = default,
            bool? publisherFlag = default,
            string? createdBy = default,
            DateTime? createdDate = default
        )
        {
            Companies company = new()
            {
                Id = id ?? _faker.Random.Number(1, 500000),
                Name = name ?? _faker.Random.String(0, 255),
                FoundedDate = foundedDate ?? _faker.Date.RecentDateOnly(100),
                ImageFilePath = imageFilePath ?? _faker.Random.String(0, 255),
                DeveloperFlag = developerFlag ?? _faker.Random.Bool(),
                PublisherFlag = publisherFlag ?? _faker.Random.Bool(),
                CreatedBy = createdBy ?? _faker.Random.Guid().ToString(),
                CreatedDate = createdDate ?? DateTime.Now
            };
            return company;
        }
    
    }
}