using System.Linq.Expressions;
using BusinessLogic.Infrastructure;
using Components.Exceptions;
using Components.Utilities;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions.Authentication;
using Moq;
using Repositories;

namespace UnitTests.PlayRecordComment;

public class CreatePlayRecordCommentTest : BaseTest
{
    [Fact]
    public void Can_CreatePlayRecordComment()
    {
        // Assert
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        GraphServiceClient mockGraphServiceClient = new(new AnonymousAuthenticationProvider());
        var mockPlayRecordComment = TestObjectFactory.GetMockCreatePlayRecordCommentDto();
        var mockPlayRecord = TestObjectFactory.GetMockPlayRecord();

        mockGenericRepository.Setup(m => m.GetById<PlayRecords>(It.IsAny<int>())).Returns(mockPlayRecord);

        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);
        
        // Arrange 
        subjectUnderTest.CreatePlayRecordComment(mockPlayRecordComment, _faker.Random.Guid().ToString());

        // Assert
        using (new AssertionScope())
        {
            mockGenericRepository.Verify(m => m.InsertRecord(It.IsAny<PlayRecordComments>()), Times.Once);
            mockGenericRepository.Verify(m => m.InsertRecord(It.IsAny<PlayRecordCommentVote>()), Times.Once);
        }
    }

    [Fact]
    public void Error_NullUser_CreatePlayRecordComment()
    {
        // Assert
        Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
        GraphServiceClient mockGraphServiceClient = new(new AnonymousAuthenticationProvider());

        var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

        var playRecordCommentDto = TestObjectFactory.GetMockCreatePlayRecordCommentDto();

        // Arrange & Assert
        using (new AssertionScope())
        {
            DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.CreatePlayRecordComment(playRecordCommentDto, (string) null));
            exception.Message.Should().Be("Can't create play record comment. User not found.");
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

