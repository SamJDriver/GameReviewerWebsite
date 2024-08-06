using BusinessLogic.Infrastructure;
using Components.Exceptions;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using Moq;
using Repositories;

namespace UnitTests.Game;

public class SearchGamesTest : BaseTest
{
    [Fact]
    public async Task Can_Get_SearchGames()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        GenresLookup genre = TestObjectFactory.GetMockGenresLookup();
        var releaseYear = _faker.Date.Between(new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), new DateTime(Components.Constants.MaximumReleaseYear, 1, 1)).Year;
        int pageSize = 10;
        List<Games> games = new List<Games>();
        for (int i = 0; i < pageSize; i++)
        {
            games.Add(TestObjectFactory.GetMockGameEntity());
        }

        mockGenericRepository.Setup(m => m.GetById<GenresLookup>(It.IsAny<int>())).Returns(genre);
        mockGameRepository.Setup(m => m.SearchGames(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(games.AsQueryable());

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);

        // Act
        var searchGames = await subjectUnderTest.SearchGames("Halo", genre.Id, releaseYear, 0, pageSize);

        // Assert
        using (new AssertionScope())
        {
            searchGames!.Data.Count().Should().Be(pageSize);
            mockGameRepository.Verify(m => m.SearchGames(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            searchGames!.TotalRowCount.Should().Be(games.Count());
        }
    }

    [Fact]
    public async Task Error_Genre_SearchGames()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var genreId = _faker.Random.Int(1, 5000);
        var releaseYear = _faker.Date.Between(new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), new DateTime(Components.Constants.MaximumReleaseYear, 1, 1)).Year;

        mockGenericRepository.Setup(m => m.GetById<GenresLookup>(It.IsAny<int>())).Returns((GenresLookup)null);

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);

        // Assert
        using (new AssertionScope())
        {
            await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.SearchGames("Halo", genreId, releaseYear, 0, 10));
        }
    }

    [Fact]
    public async Task Error_ReleaseYear_SearchGames()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var genreId = _faker.Random.Int(1, 5000);
        var releaseYear = _faker.Date.Between(new DateTime(1900, 1, 1), new DateTime(Components.Constants.MinimumReleaseYear, 1, 1)).Year;

        mockGenericRepository.Setup(m => m.GetById<GenresLookup>(It.IsAny<int>())).Returns((GenresLookup)null);

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);

        // Assert
        using (new AssertionScope())
        {
            await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.SearchGames("Halo", genreId, releaseYear, 0, 10));
        }
    }
}
