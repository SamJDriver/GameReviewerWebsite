using BusinessLogic.Infrastructure;
using DataAccess.Contexts.DockerDb;
using Repositories;
using Microsoft.Graph;
using Components.Models;
using FluentAssertions.Execution;
using FluentAssertions;
using Moq;
using DataAccess.Models.DockerDb;
using Components.Utilities;
using Microsoft.Extensions.Configuration;
using Azure.Identity;
using Microsoft.Kiota.Abstractions.Authentication;
using Components.Exceptions;


namespace UnitTests.Game;

public class GetFriendsGamesTest : BaseTest
{
    private readonly IConfiguration _configuration;

    public GetFriendsGamesTest()
    {
        var builder = new ConfigurationBuilder().AddUserSecrets<GetFriendsGamesTest>();
        _configuration = builder.Build();
    }

    [Fact]
    public async void Can_Get_GetGamesPopularWithFriends()
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
            games.Add(TestObjectFactory.GetMockFriendsGameEntity(userId, friendId));
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

    [Fact]
    public async void Error_NullUser_GetGamesPopularWithFriends()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IGameRepository> mockGameRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var subjectUnderTest = new GameService(mockGenericRepository.Object, mockGameRepository.Object, graphServiceClient);
        
        // Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.GetGamesPopularWithFriends(0, 10, (string)null));
            exception.Message.Should().Be("Can't view friend's games. No user logged in.");
        }
    }
}
