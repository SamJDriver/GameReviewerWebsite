using BusinessLogic.Infrastructure;
using DataAccess.Contexts.DockerDb;
using Repositories;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using FluentAssertions.Execution;
using FluentAssertions;
using Moq;
using DataAccess.Models.DockerDb;
using Components.Exceptions;
using Components.Models;

namespace UnitTests.Game;

public class CreateGameTest : BaseTest
{
    [Fact]
    public async Task Can_CreateGame()
    {
        //Arrange
        var expecteCreatedBy = Guid.NewGuid().ToString();
        var gameDto = TestObjectFactory.GetMockGameDto();

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
    public async Task Error_InvalidDate_CreateGame()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);
        var game = TestObjectFactory.GetMockGameDto();
        game.ReleaseDate = _faker.Date.BetweenDateOnly(new DateOnly(1900, 1, 1), new DateOnly(Components.Constants.MinimumReleaseYear, 1, 1));

        // Act & Assert
        using (new AssertionScope())
        {
            await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.CreateGame(game, Guid.NewGuid().ToString()));
        }
    }

    [Fact]
    public async Task Error_NullUser_CreateGame()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);
        var game = TestObjectFactory.GetMockGameDto();

        // Act & Assert
        using (new AssertionScope())
        {
            var ex = await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.CreateGame(game, (string)null));
            ex.Message.Should().Be("Can't create game. No user logged in.");
        }
    }

    [Fact]
    public async Task Error_NoGameAdded_CreateGame()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);
        var game = TestObjectFactory.GetMockGameDto();
        var userId = Guid.NewGuid().ToString();

        mockGenericRepository.Setup(m => m.InsertRecordAsync(It.IsAny<Games>)).ReturnsAsync(0);

        // Act & Assert
        using (new AssertionScope())
        {
            var ex = await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.CreateGame(game, userId));
            ex.Message.Should().Be("Can't create game. Game not created.");
        }
    }
}
