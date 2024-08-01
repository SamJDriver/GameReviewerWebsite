using BusinessLogic.Infrastructure;
using DataAccess.Contexts.DockerDb;
using Repositories;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using Components.Models;
using FluentAssertions.Execution;
using FluentAssertions;
using Moq;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models.DockerDb;


namespace UnitTests.Game;

public class GameTests : BaseTest
{
    [Fact]
    public async Task Can_Create_Game()
    {
        //Arrange
        var expectedGameTitle = _faker.Random.String(0, 255); //varchar(255)
        var expectedReleaseDate = DateOnly.FromDateTime(
            _faker.Date.Between(
                new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), new DateTime(Components.Constants.MaximumReleaseYear, 1, 1)));
        var expectedDescription = _faker.Random.String(0, 65535);//Text
        var expecteCreatedBy = Guid.NewGuid().ToString();

        GameGenresLookupLinkDto genre = new()
        {
            Id = 0,
            GameId = 0,
            GenreLookupId = _faker.Random.Number(1, 100)

        };
        GameDto gameDto = new()
        {
            Title = expectedGameTitle,
            ReleaseDate = expectedReleaseDate,
            Description = expectedDescription,
            GamesGenresLookupLink = [genre]
        };

        var mockSet = new Mock<DbSet<Games>>();
        var mockContext = new Mock<DockerDbContext>();
        mockContext.Setup(m => m.Games).Returns(mockSet.Object);
        mockContext.Setup(m => m.Set<Games>()).Returns(mockSet.Object);
        mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

        var authClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
        var subjectUnderTest = new GameService(new GenericRepository<DockerDbContext>(mockContext.Object), new GameRepository(mockContext.Object), authClient);

        //Act
        var newId = await subjectUnderTest.CreateGame(gameDto, expecteCreatedBy);

        //Assert
        using (new AssertionScope())
        {
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            mockSet.Verify(m => m.AddAsync(It.IsAny<Games>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }

}
