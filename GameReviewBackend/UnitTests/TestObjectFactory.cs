using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Components.Models;
using DataAccess.Models.DockerDb;

namespace UnitTests
{
    internal class TestObjectFactory
    {
        protected static readonly Faker _faker = new Faker();

        internal static Games GetMockGameEntity()
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
                CreatedBy = Guid.NewGuid().ToString()
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
    
    }
}