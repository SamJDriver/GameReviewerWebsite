using BusinessLogic.Infrastructure;
using DataAccess.Contexts.DockerDb;
using Repositories;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using Components.Models;
using FluentAssertions.Execution;
using FluentAssertions;
using Moq;
using DataAccess.Models.DockerDb;
using Components.Utilities;
using Microsoft.Extensions.Configuration;
using Azure.Identity;


namespace UnitTests.Game;

public class GameTests : BaseTest
{

    private readonly IConfiguration _configuration;

    public GameTests()
    {
        var builder = new ConfigurationBuilder().AddUserSecrets<GameTests>();
        _configuration = builder.Build();
    }

    [Fact]
    public async Task Can_Create_Game()
    {
        //Arrange
        var expecteCreatedBy = Guid.NewGuid().ToString();
        var gameDto = getMockGameDto();

        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        mockGenericRepository.Setup(m => m.InsertRecordAsync(It.IsAny<Games>())).Returns(Task.FromResult(1));
        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);

        //Act
        var newId = await subjectUnderTest.CreateGame(gameDto, expecteCreatedBy);

        //Assert
        using (new AssertionScope())
        {
            mockGenericRepository.Verify(m => m.InsertRecordAsync(It.IsAny<Games>()), Times.Once);
            gameDto.Id.Should().Be(newId);
        }
    }

    [Fact]
    public void Can_Get_GameById()
    {

        //Arrange
        var gameEntity = getMockGameEntity();
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        mockGenericRepository.Setup(m => m.GetById<Games>(It.IsAny<int>())).Returns(gameEntity);
        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);

        //Act
        var retrievedGame = subjectUnderTest.GetGameById(gameEntity.Id);

        //Assert
        using (new AssertionScope())
        {
            mockGenericRepository.Verify(m => m.GetById<Games>(It.IsAny<int>()), Times.Once);
            retrievedGame.Id.Should().Be(gameEntity.Id);
            retrievedGame.Title.Should().Be(gameEntity.Title);
            retrievedGame.ReleaseDate.Should().Be(gameEntity.ReleaseDate);
            retrievedGame.Description.Should().Be(gameEntity.Description);
        }
    }

    [Fact]
    public async void Can_Get_AllGames()
    {
        //Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var game1 = getMockGameEntity();
        var game2 = getMockGameEntity();
        var game3 = getMockGameEntity();



        mockGenericRepository.Setup(m => m.GetAll<Games>()).Returns(new[] { game1, game2, game3 }.AsAsyncQueryable());
        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);

        //Act
        PagedResult<GameDto>? retrievedGames = await subjectUnderTest.GetAllGames(0, 10);

        //Assert
        using (new AssertionScope())
        {
            mockGenericRepository.Verify(m => m.GetAll<Games>(), Times.Once);
            retrievedGames!.Data.Count().Should().Be(3);
        }
    }

    [Fact]
    public async void Can_Get_Friends_Games()
    {
        //Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();

        var options = new ClientSecretCredentialOptions { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud };
        var clientSecretCredential = new ClientSecretCredential(_configuration["AzureAd:TenantId"], _configuration["AzureAd:ClientId"], _configuration["AzureAd:ClientSecret"], options);
        GraphServiceClient graphServiceClient = new GraphServiceClient(clientSecretCredential, new[] { "https://graph.microsoft.com/.default" });
        MapsterTestConfiguration.GetMapper();

        var userId = Guid.NewGuid().ToString();
        var friendId = Guid.NewGuid().ToString();

        int pageSize = 10;

        List<Games> games = new();
        for (int i = 0; i < pageSize+1; i++)
        {
            games.Add(getMockFriendsGameEntity(userId, friendId));
        }

        mockGameRepository.Setup(m => m.GetFriendsGames(It.IsAny<string>())).Returns(games.AsAsyncQueryable());

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);

        //Act
        PagedResult<Game_GetList_Dto>? retrievedGames = await subjectUnderTest.GetGamesPopularWithFriends(0, pageSize, userId);

        //Assert
        using (new AssertionScope())
        {
            mockGameRepository.Verify(m => m.GetFriendsGames(It.IsAny<string>()), Times.Once);
            retrievedGames!.Data.Count().Should().Be(pageSize);
            retrievedGames!.Data.First().Title.Should().Be(games.First().Title);
            retrievedGames!.Data.First().ReviewerId.Should().Be(games.First().PlayRecords.First().CreatedBy);
        }
    }

    private Games getMockGameEntity()
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

    private GameDto getMockGameDto()
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

    private Games getMockFriendsGameEntity(string userId, string friendId)
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
    private Game_GetList_Dto getMockGameGetListDto()
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
}
