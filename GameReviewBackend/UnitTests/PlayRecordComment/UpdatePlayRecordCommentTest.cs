using System.Linq.Expressions;
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

namespace UnitTests.PlayRecordComment;

public class UpdatePlayRecordCommentTest : BaseTest
{
    [Fact]
    public void Can_UpdatePlayRecordComment()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var userId = _faker.Random.Guid().ToString();
        
        var mockNewPlayRecordCommentDto = TestObjectFactory.GetMockUpdatePlayRecordCommentDto();
        var playRecordToUpdateId = _faker.Random.Int();
        var mockExistingPlayRecordComment = TestObjectFactory.GetMockPlayRecordComment(playRecordId: playRecordToUpdateId ,createdBy: userId);
        var mockExistingPlayRecord = TestObjectFactory.GetMockPlayRecord(id: playRecordToUpdateId);

        mockGenericRepository.Setup(m => m.GetSingleTracked<PlayRecordComments>(It.IsAny<Expression<Func<PlayRecordComments, bool>>>())).Returns(mockExistingPlayRecordComment);
        mockGenericRepository.Setup(m => m.GetSingleNoTrack<PlayRecords>(It.IsAny<Expression<Func<PlayRecords, bool>>>())).Returns(mockExistingPlayRecord);
        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

        // Act
        subjectUnderTest.UpdatePlayRecordComment(playRecordToUpdateId, mockNewPlayRecordCommentDto, userId);

        // Assert
        using (new AssertionScope())
        {
            mockGenericRepository.Verify(m => m.UpdateRecord(It.IsAny<PlayRecordComments>()), Times.Once);
            mockExistingPlayRecordComment.CommentText.Should().Be(mockNewPlayRecordCommentDto.CommentText);
        }
    }

    [Fact]
    public void Error_PlayRecordCommentNotFound_UpdatePlayRecordComment()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
        var mockPlayRecordComment = TestObjectFactory.GetMockUpdatePlayRecordCommentDto();

        mockGenericRepository.Setup(m => m.GetSingleTracked<PlayRecordComments>(It.IsAny<Expression<Func<PlayRecordComments, bool>>>())).Returns(null as PlayRecordComments);

        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

        // Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.UpdatePlayRecordComment(1, mockPlayRecordComment, _faker.Random.Guid().ToString()));
            exception.Message.Should().Be("Can't update Play Record comment. Play Record comment not found.");
        }
    }

    [Fact]
    public void Error_NullUser_UpdatePlayRecordComment()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
        var mockPlayRecordComment = TestObjectFactory.GetMockPlayRecordComment();
        var mockNewPlayRecordCommentDto = TestObjectFactory.GetMockUpdatePlayRecordCommentDto();

        mockGenericRepository.Setup(m => m.GetSingleTracked<PlayRecordComments>(It.IsAny<Expression<Func<PlayRecordComments, bool>>>())).Returns(mockPlayRecordComment);
        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

        // Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.UpdatePlayRecordComment(1, mockNewPlayRecordCommentDto, (string) null));
            exception.Message.Should().Be("Can't update Play Record comment. User not found.");
        }
    }

    [Fact]
    public void Error_DifferentUser_UpdatePlayRecordComment()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var userId = _faker.Random.Guid().ToString();
        
        var mockPlayRecordComment = TestObjectFactory.GetMockPlayRecordComment();
        var mockNewPlayRecordCommentDto = TestObjectFactory.GetMockUpdatePlayRecordCommentDto();

        mockGenericRepository.Setup(m => m.GetSingleTracked<PlayRecordComments>(It.IsAny<Expression<Func<PlayRecordComments, bool>>>())).Returns(mockPlayRecordComment);
        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

        // Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.UpdatePlayRecordComment(1, mockNewPlayRecordCommentDto, userId));
            exception.Message.Should().Be("Can't update Play Record comment. User not found.");
        }
    }

    [Fact]
    public void Error_PlayRecordNotFound_UpdatePlayRecordComment()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var userId = _faker.Random.Guid().ToString();
        
        var mockPlayRecordComment = TestObjectFactory.GetMockPlayRecordComment(createdBy: userId);
        var mockNewPlayRecordCommentDto = TestObjectFactory.GetMockUpdatePlayRecordCommentDto();

        mockGenericRepository.Setup(m => m.GetSingleTracked<PlayRecordComments>(It.IsAny<Expression<Func<PlayRecordComments, bool>>>())).Returns(mockPlayRecordComment);
        mockGenericRepository.Setup(m => m.GetSingleTracked<PlayRecords>(It.IsAny<Expression<Func<PlayRecords, bool>>>())).Returns(null as PlayRecords);
        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

        // Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.UpdatePlayRecordComment(1, mockNewPlayRecordCommentDto, userId));
            exception.Message.Should().Be("Can't find play record with comment.");
        }
    }

    [Fact]
    public void Error_IdMismatch_UpdatePlayRecordComment()
    {
        // Arrange
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

        var userId = _faker.Random.Guid().ToString();
        
        var mockNewPlayRecordCommentDto = TestObjectFactory.GetMockUpdatePlayRecordCommentDto();
        var mockExistingPlayRecordComment = TestObjectFactory.GetMockPlayRecordComment(playRecordId: 1 ,createdBy: userId);
        var mockExistingPlayRecord = TestObjectFactory.GetMockPlayRecord(id: 2);

        mockGenericRepository.Setup(m => m.GetSingleTracked<PlayRecordComments>(It.IsAny<Expression<Func<PlayRecordComments, bool>>>())).Returns(mockExistingPlayRecordComment);
        mockGenericRepository.Setup(m => m.GetSingleNoTrack<PlayRecords>(It.IsAny<Expression<Func<PlayRecords, bool>>>())).Returns(mockExistingPlayRecord);
        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

        // Act & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.UpdatePlayRecordComment(1, mockNewPlayRecordCommentDto, userId));
            exception.Message.Should().Be("Can't find play record with comment.");
        }
    }
}
