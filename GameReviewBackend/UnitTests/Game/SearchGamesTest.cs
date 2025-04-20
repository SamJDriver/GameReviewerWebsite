using BusinessLogic.Abstractions;
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
        Mock<ILookupService> mockLookupService = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        GenresLookup genre = TestObjectFactory.GetMockGenresLookup();
        var minReleaseDate = _faker.Date.Between(new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), new DateTime(Components.Constants.MaximumReleaseYear, 1, 1));
        var maxReleaseDate = _faker.Date.Between(minReleaseDate, new DateTime(Components.Constants.MaximumReleaseYear, 1, 1));
        int pageSize = 10;
        List<Games> games = new List<Games>();
        for (int i = 0; i < pageSize; i++)
        {
            games.Add(TestObjectFactory.GetMockGameEntity());
        }

        mockGenericRepository.Setup(m => m.GetById<GenresLookup>(It.IsAny<int>())).Returns(genre);
        mockGameRepository.Setup(m => m.SearchGames(It.IsAny<string>(), It.IsAny<int[]>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(games.AsQueryable());

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient, mockLookupService.Object);

        // Act
        var searchGames = await subjectUnderTest.SearchGames("Halo", [genre.Id], minReleaseDate, maxReleaseDate, 0, pageSize);

        // Assert
        using (new AssertionScope())
        {
            searchGames!.Items.Count().Should().Be(pageSize);
            mockGameRepository.Verify(m => m.SearchGames(It.IsAny<string>(), It.IsAny<int[]>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
            searchGames!.TotalRowCount.Should().Be(games.Count());
        }
    }

    [Fact]
    public async Task Error_Genre_SearchGames()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        Mock<ILookupService> mockLookupService = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var genreId = _faker.Random.Int(1, 5000);
        var minReleaseDate = _faker.Date.Between(new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), new DateTime(Components.Constants.MaximumReleaseYear, 1, 1));
        var maxReleaseDate = _faker.Date.Between(minReleaseDate, new DateTime(Components.Constants.MaximumReleaseYear, 1, 1));

        mockGenericRepository.Setup(m => m.GetById<GenresLookup>(It.IsAny<int>())).Returns((GenresLookup)null);

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient, mockLookupService.Object);

        // Assert
        using (new AssertionScope())
        {
            await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.SearchGames("Halo", [genreId], minReleaseDate, maxReleaseDate, 0, 10));
        }
    }

    [Fact]
    public async Task Error_ReleaseYear_SearchGames()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        Mock<ILookupService> mockLookupService = new();
        GraphServiceClient graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var genreId = _faker.Random.Int(1, 5000);
        var minReleaseDate = _faker.Date.Between(new DateTime(Components.Constants.MinimumReleaseYear, 1, 1), new DateTime(Components.Constants.MaximumReleaseYear, 1, 1));
        var maxReleaseDate = _faker.Date.Between(minReleaseDate, new DateTime(Components.Constants.MaximumReleaseYear, 1, 1));
        mockGenericRepository.Setup(m => m.GetById<GenresLookup>(It.IsAny<int>())).Returns((GenresLookup)null);

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient, mockLookupService.Object);

        // Act & Assert
        using (new AssertionScope())
        {
            await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.SearchGames("Halo", [genreId], minReleaseDate, maxReleaseDate, 0, 10));
        }
    }
}
