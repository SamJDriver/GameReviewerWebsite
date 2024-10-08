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

public class GetAllGamesTest : BaseTest
{
    [Fact]
    public async void Can_Get_AllGames()
    {
        //Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var game1 = TestObjectFactory.GetMockGameEntity();
        var game2 = TestObjectFactory.GetMockGameEntity();
        var game3 = TestObjectFactory.GetMockGameEntity();

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
}
