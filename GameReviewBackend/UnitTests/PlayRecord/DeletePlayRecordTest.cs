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

namespace UnitTests.PlayRecord;

public class DeletePlayRecordTest : BaseTest
{
    [Fact]
    public void Can_DeletePlayRecord()
    {
        //Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
        var mockGameEntity = TestObjectFactory.GetMockGameEntity();
        var mockPlayRecord = TestObjectFactory.GetMockCreatePlayRecordDto(gameId: mockGameEntity.Id);

        mockGenericRepository.Setup(m => m.GetById<Games>(It.IsAny<int>())).Returns(mockGameEntity);

        var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

        // Act
        subjectUnderTest.CreatePlayRecord(mockPlayRecord, _faker.Random.Guid().ToString());

        // Assert
        using (new AssertionScope())
        {
            mockGenericRepository.Verify(m => m.InsertRecord(It.IsAny<PlayRecords>()), Times.Once);
        }
    }
    
    [Fact]
    public void Error_NullUser_DeletePlayRecord()
    {
        //Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
        var mockPlayRecord = TestObjectFactory.GetMockCreatePlayRecordDto();

        var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

        // Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.DeletePlayRecord(_faker.Random.Int(), (string)null));
            exception.Message.Should().Be("Cannot delete Play Record. No user logged in.");
        }
    }

    [Fact]
    public void Error_NullPlayRecord_DeletePlayRecord()
    {
        //Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
        var mockPlayRecord = TestObjectFactory.GetMockCreatePlayRecordDto();

        mockGenericRepository.Setup(m => m.GetById<PlayRecords>(It.IsAny<int>())).Returns((PlayRecords)null);

        var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

        // Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.DeletePlayRecord(_faker.Random.Int(), (string)_faker.Random.Guid().ToString()));
            exception.Message.Should().Be("Cannot delete play record. No Record found.");
        }
    }

    [Fact]
    public void Error_MismatchedUser_DeletePlayRecord()
    {
        //Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
        var mockPlayRecord = TestObjectFactory.GetMockPlayRecord(createdBy: "Test User");

        mockGenericRepository.Setup(m => m.GetById<PlayRecords>(It.IsAny<int>())).Returns(mockPlayRecord);

        var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

        // Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.DeletePlayRecord(_faker.Random.Int(), "Different User"));
            exception.Message.Should().Be("Cannot delete play record. Cannot update another user's play record.");
        }
    }
}
