using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
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

            UserRelationship userRelationship = new()
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

        internal static Game_GetList_Dto GetMockGameGetListDto()
        {
            Game_GetList_Dto game = new()
            {
                GameId = _faker.Random.Number(1, 500000),
                PlayRecordId = _faker.Random.Number(1, 500000),
                Title = _faker.Random.String(0, 255),
                CoverImageUrl = _faker.Random.String(0, 255),
                Rating = _faker.Random.Number(1, 100),
                ReviewerName = _faker.Random.String(0, 255),
                ReviewerId = Guid.NewGuid().ToString(),
                ReviewDate = _faker.Date.Between(new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), DateTime.Now)
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
    
    }
}