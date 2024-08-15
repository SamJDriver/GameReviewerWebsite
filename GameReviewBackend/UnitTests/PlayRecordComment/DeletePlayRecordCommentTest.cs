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

public class DeletePlayRecordCommentTest : BaseTest
{
    [Fact]
    public void Can_MatchCommentorAndPlayRecordCreator_DeletePlayRecordComment()
    {
        // Assert
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        GraphServiceClient mockGraphServiceClient = new(new AnonymousAuthenticationProvider());
        var userId = _faker.Random.Guid().ToString();
        var mockPlayRecord = TestObjectFactory.GetMockPlayRecord();
        var mockPlayRecordComment = TestObjectFactory.GetMockPlayRecordComment(createdBy: userId, playRecord: mockPlayRecord);

        mockGenericRepository.Setup(m => m.GetById<PlayRecordComments>(It.IsAny<int>())).Returns(mockPlayRecordComment);

        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);
        
        // Arrange 
        subjectUnderTest.DeletePlayRecordComment(mockPlayRecordComment.Id, userId);

        // Assert
        using (new AssertionScope())
        {
            mockGenericRepository.Verify(m => m.DeleteRecordById<PlayRecordComments>(It.IsAny<int>()), Times.Once);
        }
    }

    [Fact]
    public void Error_NullUser_DeletePlayRecordComment()
    {
        // Assert
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        GraphServiceClient mockGraphServiceClient = new(new AnonymousAuthenticationProvider());

        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

        // Arrange & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.DeletePlayRecordComment(1, (string) null));
            exception.Message.Should().Be("Cannot delete comment. No user logged in.");
        }
    }

    [Fact]
    public void Error_NullExistingPlayRecord_CreatePlayRecordComment()
    {
        // Assert
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        GraphServiceClient mockGraphServiceClient = new(new AnonymousAuthenticationProvider());

        mockGenericRepository.Setup(m => m.GetById<PlayRecords>(It.IsAny<int>())).Returns(null as PlayRecords);

        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

        // Arrange & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.CreatePlayRecordComment(TestObjectFactory.GetMockCreatePlayRecordCommentDto(), "test-user"));
            exception.Message.Should().Be("Can't create play record. Play record not found.");
        }
    }
}

