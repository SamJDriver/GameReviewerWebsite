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


namespace UnitTests.Game;

public class GameTests : BaseTest
{
    private readonly Mock<IGenericRepository<DockerDbContext>> _mockGenericRepository;
    private readonly Mock<IGameRepository> _mockGameRepository;
    private readonly GraphServiceClient _graphServiceClient;

    public GameTests()
    {
        this._mockGenericRepository = new Mock<IGenericRepository<DockerDbContext>>();
        this._mockGameRepository = new Mock<IGameRepository>();
        this._graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
    }

    [Fact]
    public async Task Can_Create_Game()
    {
        //Arrange
        var expecteCreatedBy = Guid.NewGuid().ToString();
        var gameDto = getMockGameDto();

        _mockGenericRepository.Setup(m => m.InsertRecordAsync(It.IsAny<Games>())).Returns(Task.FromResult(1));
        var subjectUnderTest = new GameService(_mockGenericRepository.Object, _mockGameRepository.Object, _graphServiceClient);

        //Act
        var newId = await subjectUnderTest.CreateGame(gameDto, expecteCreatedBy);

        //Assert
        using (new AssertionScope())
        {
            _mockGenericRepository.Verify(m => m.InsertRecordAsync(It.IsAny<Games>()), Times.Once);
            gameDto.Id.Should().Be(newId);
        }
    }

    [Fact]
    public void Can_Get_GameById()
    {

        //Arrange
        var gameEntity = getMockGameEntity(); 

        _mockGenericRepository.Setup(m => m.GetById<Games>(It.IsAny<int>())).Returns(gameEntity);

        var subjectUnderTest = new GameService(_mockGenericRepository.Object, _mockGameRepository.Object, _graphServiceClient);

        //Act
        var retrievedGame = subjectUnderTest.GetGameById(gameEntity.Id);

        //Assert
        using (new AssertionScope())
        {
            _mockGenericRepository.Verify(m => m.GetById<Games>(It.IsAny<int>()), Times.Once);
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
        var game1 = getMockGameEntity();
        var game2 = getMockGameEntity();
        var game3 = getMockGameEntity();

        _mockGenericRepository.Setup(m => m.GetAll<Games>()).Returns(new []{ game1, game2, game3 }.AsAsyncQueryable());
        
        var subjectUnderTest = new GameService(_mockGenericRepository.Object, _mockGameRepository.Object, _graphServiceClient);

        //Act
       PagedResult<GameDto>? retrievedGames = await subjectUnderTest.GetAllGames(0, 10);

        //Assert
        using (new AssertionScope())
        {
            _mockGenericRepository.Verify(m => m.GetAll<Games>(), Times.Once);
            retrievedGames!.Data.Count().Should().Be(3);
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
}
