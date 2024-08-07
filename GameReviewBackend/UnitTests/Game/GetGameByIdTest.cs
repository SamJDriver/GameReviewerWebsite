using BusinessLogic.Infrastructure;
using DataAccess.Contexts.DockerDb;
using Repositories;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using FluentAssertions.Execution;
using FluentAssertions;
using Moq;
using DataAccess.Models.DockerDb;

namespace UnitTests.Game;

public class GetGameByIdTest : BaseTest
{    
    [Fact]
    public void Can_GetGameById()
    {
        //Arrange
        var gameEntity = TestObjectFactory.GetMockGameEntity();
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
}
