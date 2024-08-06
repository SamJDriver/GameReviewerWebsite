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

public class CreateGameTest : BaseTest
{
    [Fact]
    public async Task Can_Create_Game()
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
}
