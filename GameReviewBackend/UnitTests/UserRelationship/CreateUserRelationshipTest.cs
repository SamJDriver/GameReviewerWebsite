using Azure.Identity;
using BusinessLogic.Infrastructure;
using Components.Exceptions;
using DataAccess.Contexts.DockerDb;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Moq;
using Repositories;
using Microsoft.Graph.Users.Item;
using static Microsoft.Graph.Privacy.SubjectRightsRequests.Item.Approvers.Item.UserItemRequestBuilder;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Serialization.Json;
using Microsoft.Kiota.Abstractions.Store;
using DataAccess.Models.DockerDb;
using System.Linq.Expressions;
using Bogus.DataSets;

namespace UnitTests.UserRelationship;

public class CreateUserRelationshipTest : BaseTest
{

    private readonly IConfiguration _configuration;

    public CreateUserRelationshipTest()
    {
        var builder = new ConfigurationBuilder().AddUserSecrets<CreateUserRelationshipTest>();
        _configuration = builder.Build();
    }

    [Fact]
    public async Task Can_CreateNewRecord_CreateUserRelationship()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IRequestAdapter> mockRequestAdapter = Create();
        var graphServiceClient = new GraphServiceClient(mockRequestAdapter.Object);

        mockRequestAdapter.Setup(adapter => adapter.SendAsync(
            It.Is<RequestInformation>(info => info.HttpMethod == Method.GET),
            User.CreateFromDiscriminatorValue,
            It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(new User { DisplayName = "Test User", });

        var mockIncomingUserRelationshipTypeLookup = TestObjectFactory.GetMockUserRelationshipTypeLookup(id: 1, code: Components.Constants.LookupCodes.UserRelationshipTypeLookup.FriendCode);

        mockGenericRepository.Setup(m => m.GetById<UserRelationshipTypeLookup>(It.IsAny<int>())).Returns(mockIncomingUserRelationshipTypeLookup);
        mockGenericRepository.Setup(m => m.GetSingleTracked<DataAccess.Models.DockerDb.UserRelationship>(
            It.IsAny<Expression<Func<DataAccess.Models.DockerDb.UserRelationship, bool>>>()
        )).Returns((DataAccess.Models.DockerDb.UserRelationship)null);


        var parentUserId = _faker.Random.Guid().ToString();
        var childUserId = _faker.Random.Guid().ToString();

        var subjectUnderTest = new UserRelationshipService(graphServiceClient, mockGenericRepository.Object);

        // Act
        await subjectUnderTest.CreateUserRelationship(parentUserId, childUserId, mockIncomingUserRelationshipTypeLookup.Id);

        // Assert
        using (new AssertionScope())
        {
            mockGenericRepository.Verify(m => m.InsertRecord(It.IsAny<DataAccess.Models.DockerDb.UserRelationship>()), Times.Once);
        }
    }

    [Fact]
    public async Task Can_UpdateExistingRelationship_CreateUserRelationship()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IRequestAdapter> mockRequestAdapter = Create();
        var graphServiceClient = new GraphServiceClient(mockRequestAdapter.Object);

        mockRequestAdapter.Setup(adapter => adapter.SendAsync(
            It.Is<RequestInformation>(info => info.HttpMethod == Method.GET),
            Microsoft.Graph.Models.User.CreateFromDiscriminatorValue,
            It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(new Microsoft.Graph.Models.User { DisplayName = "Test User", });

        var mockIncomingUserRelationshipTypeLookup = TestObjectFactory.GetMockUserRelationshipTypeLookup(id: 1, code: Components.Constants.LookupCodes.UserRelationshipTypeLookup.FriendCode);
        var mockExistingUserRelationshipTypeLookup = TestObjectFactory.GetMockUserRelationshipTypeLookup(id: 2, code: Components.Constants.LookupCodes.UserRelationshipTypeLookup.IgnoreCode);
        var mockExistingUserRelationship = TestObjectFactory.GetMockUserRelationship(userRelationshipTypeLookupId: 2, userRelationshipTypeLookup: mockExistingUserRelationshipTypeLookup);

        mockGenericRepository.Setup(m => m.GetById<UserRelationshipTypeLookup>(It.IsAny<int>())).Returns(mockIncomingUserRelationshipTypeLookup);
        mockGenericRepository.Setup(m => m.GetSingleTracked<DataAccess.Models.DockerDb.UserRelationship>(
            It.IsAny<Expression<Func<DataAccess.Models.DockerDb.UserRelationship, bool>>>()
        )).Returns(mockExistingUserRelationship);


        var parentUserId = _faker.Random.Guid().ToString();
        var childUserId = _faker.Random.Guid().ToString();

        var subjectUnderTest = new UserRelationshipService(graphServiceClient, mockGenericRepository.Object);

        // Act
        await subjectUnderTest.CreateUserRelationship(parentUserId, childUserId, mockIncomingUserRelationshipTypeLookup.Id);

        // Assert
        using (new AssertionScope())
        {
            mockGenericRepository.Verify(m => m.UpdateRecord(mockExistingUserRelationship), Times.Once);
            mockExistingUserRelationship.UserRelationshipTypeLookupId.Should().Be(mockIncomingUserRelationshipTypeLookup.Id);
        }
    }

    [Fact]
    public async Task Error_AddSelf_CreateUserRelationship()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
        var userId = _faker.Random.Guid().ToString();

        var subjectUnderTest = new UserRelationshipService(graphServiceClient, mockGenericRepository.Object);

        //Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.CreateUserRelationship(userId, userId, 1));
            exception.Message.Should().Be("Cannot add yourself.");
        }
    }

    [Fact]
    public async Task Error_UnauthenticatedParentUserId_CreateUserRelationship()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();

        var options = new ClientSecretCredentialOptions { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud };
        var clientSecretCredential = new ClientSecretCredential(
            _configuration["AzureAd:TenantId"], _configuration["AzureAd:ClientId"], _configuration["AzureAd:ClientSecret"], options);
        GraphServiceClient graphServiceClient = new GraphServiceClient(clientSecretCredential, new[] { "https://graph.microsoft.com/.default" });

        var parentUserId = "testParentUserId";
        var childUserId = _faker.Random.Guid().ToString();

        var subjectUnderTest = new UserRelationshipService(graphServiceClient, mockGenericRepository.Object);

        //Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.CreateUserRelationship(parentUserId, childUserId, 1));
            exception.Message.Should().Be("Could not authenticate requesting user.");
        }
    }

    [Fact]
    public async Task Error_InvalidType_CreateUserRelationship()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IRequestAdapter> mockRequestAdapter = Create();
        var graphServiceClient = new GraphServiceClient(mockRequestAdapter.Object);

        mockRequestAdapter.Setup(adapter => adapter.SendAsync(
            // Needs to be correct HTTP Method of the desired method 👇🏻
            It.Is<RequestInformation>(info => info.HttpMethod == Method.GET),
            // Needs to be method from 👇🏻 object type that will be returned from the SDK method.
            Microsoft.Graph.Models.User.CreateFromDiscriminatorValue,
            It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Microsoft.Graph.Models.User
            {
                DisplayName = "Test User",
            });

        mockGenericRepository.Setup(m => m.GetSingleTracked<UserRelationshipTypeLookup>(It.IsAny<Expression<Func<UserRelationshipTypeLookup, bool>>>()))
        .Returns((UserRelationshipTypeLookup)null);


        var parentUserId = _faker.Random.Guid().ToString();
        var childUserId = _faker.Random.Guid().ToString();

        var subjectUnderTest = new UserRelationshipService(graphServiceClient, mockGenericRepository.Object);

        //Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.CreateUserRelationship(parentUserId, childUserId, 1));
            exception.Message.Should().Be("Invalid relationship type defined.");
        }
    }

    [Fact]
    public async Task Error_ExistingRelationship_CreateUserRelationship()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        Mock<IRequestAdapter> mockRequestAdapter = Create();
        var graphServiceClient = new GraphServiceClient(mockRequestAdapter.Object);

        mockRequestAdapter.Setup(adapter => adapter.SendAsync(
            It.Is<RequestInformation>(info => info.HttpMethod == Method.GET),
            User.CreateFromDiscriminatorValue,
            It.IsAny<Dictionary<string, ParsableFactory<IParsable>>>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(new User { DisplayName = "Test User", });

        var mockIncomingUserRelationshipTypeLookup = TestObjectFactory.GetMockUserRelationshipTypeLookup(id: 1, name: "Test User Relationship Type");
        var mockExistingUserRelationship = TestObjectFactory.GetMockUserRelationship(userRelationshipTypeLookupId: 1, userRelationshipTypeLookup: mockIncomingUserRelationshipTypeLookup);

        mockGenericRepository.Setup(m => m.GetById<UserRelationshipTypeLookup>(It.IsAny<int>())).Returns(mockIncomingUserRelationshipTypeLookup);
        mockGenericRepository.Setup(m => m.GetSingleTracked<DataAccess.Models.DockerDb.UserRelationship>(
            It.IsAny<Expression<Func<DataAccess.Models.DockerDb.UserRelationship, bool>>>()
        )).Returns(mockExistingUserRelationship);


        var parentUserId = _faker.Random.Guid().ToString();
        var childUserId = _faker.Random.Guid().ToString();

        var subjectUnderTest = new UserRelationshipService(graphServiceClient, mockGenericRepository.Object);

        // Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.CreateUserRelationship(parentUserId, childUserId, mockIncomingUserRelationshipTypeLookup.Id));
            exception.Message.Should().Be($"User is already on your {mockExistingUserRelationship.UserRelationshipTypeLookup.Name} list.");
        }
    }

    public static Mock<IRequestAdapter> Create(MockBehavior mockBehavior = MockBehavior.Strict)
    {
        var mockSerializationWriterFactory = new Mock<ISerializationWriterFactory>();
        mockSerializationWriterFactory.Setup(factory => factory.GetSerializationWriter(It.IsAny<string>()))
            .Returns((string _) => new JsonSerializationWriter());

        var mockRequestAdapter = new Mock<IRequestAdapter>(mockBehavior);
        // The first path element must have four characters to mimic v1.0 or beta
        // This is especially needed to mock batch requests.
        mockRequestAdapter.SetupGet(adapter => adapter.BaseUrl).Returns("http://graph.test.internal/mock");
        mockRequestAdapter.SetupSet(adapter => adapter.BaseUrl = It.IsAny<string>());
        mockRequestAdapter.Setup(adapter => adapter.EnableBackingStore(It.IsAny<IBackingStoreFactory>()));
        mockRequestAdapter.SetupGet(adapter => adapter.SerializationWriterFactory).Returns(mockSerializationWriterFactory.Object);

        return mockRequestAdapter;
    }
}

