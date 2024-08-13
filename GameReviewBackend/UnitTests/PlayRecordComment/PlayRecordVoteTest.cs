using System.Linq.Expressions;
using BusinessLogic.Infrastructure;
using Components.Exceptions;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Repositories;

namespace UnitTests.PlayRecordComment
{
    public class PlayRecordVoteTest : BaseTest
    {
        [Fact]
        public void Can_UpvoteWithNoPreviousVote_SubmitPlayRecordVote()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var playRecordComment = TestObjectFactory.GetMockPlayRecordComment();
            var userId = _faker.Random.Guid().ToString();

            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordComments, bool>>>()))
                .Returns(playRecordComment);

            // Return null when searching for previous votes
            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordCommentVote, bool>>>()))
                .Returns((PlayRecordCommentVote)null);

            var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

            // Act
            subjectUnderTest.Upvote(playRecordComment.Id, userId);

            // Assert
            using (new AssertionScope())
            {
                mockGenericRepository.Verify(m => m.InsertRecord(It.IsAny<PlayRecordCommentVote>()), Times.Once);
            }
        }

        [Fact]
        public void Can_DownvoteWithNoPreviousVote_SubmitPlayRecordVote()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var playRecordComment = TestObjectFactory.GetMockPlayRecordComment();
            var userId = _faker.Random.Guid().ToString();

            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordComments, bool>>>()))
                .Returns(playRecordComment);

            // Return null when searching for previous votes
            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordCommentVote, bool>>>()))
                .Returns((PlayRecordCommentVote)null);

            var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

            // Act
            subjectUnderTest.Downvote(playRecordComment.Id, userId);

            // Assert
            using (new AssertionScope())
            {
                mockGenericRepository.Verify(m => m.InsertRecord(It.IsAny<PlayRecordCommentVote>()), Times.Once);
            }
        }

        [Fact]
        public void Can_RevokePreviousUpvote_SubmitPlayRecordVote()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var playRecordComment = TestObjectFactory.GetMockPlayRecordComment();
            var existingPlayRecordCommentVote = TestObjectFactory.GetMockPlayRecordCommentVote(numericalValue: 1);
            var userId = _faker.Random.Guid().ToString();

            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordComments, bool>>>()))
                .Returns(playRecordComment);

            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordCommentVote, bool>>>()))
                .Returns(existingPlayRecordCommentVote);

            var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

            // Act
            subjectUnderTest.Upvote(playRecordComment.Id, userId);

            // Assert
            using (new AssertionScope())
            {
                mockGenericRepository.Verify(m => m.DeleteRecord(It.IsAny<PlayRecordCommentVote>()), Times.Once);
            }
        }

        [Fact]
        public void Can_RevokePreviousDownvote_SubmitPlayRecordVote()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var playRecordComment = TestObjectFactory.GetMockPlayRecordComment();
            var existingPlayRecordCommentVote = TestObjectFactory.GetMockPlayRecordCommentVote(numericalValue: -1);
            var userId = _faker.Random.Guid().ToString();

            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordComments, bool>>>()))
                .Returns(playRecordComment);

            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordCommentVote, bool>>>()))
                .Returns(existingPlayRecordCommentVote);

            var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

            // Act
            subjectUnderTest.Downvote(playRecordComment.Id, userId);

            // Assert
            using (new AssertionScope())
            {
                mockGenericRepository.Verify(m => m.DeleteRecord(It.IsAny<PlayRecordCommentVote>()), Times.Once);
            }
        }

        [Fact]
        public void Can_UpvotePreviouslyDownvoted_SubmitPlayRecordVote()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var playRecordComment = TestObjectFactory.GetMockPlayRecordComment();
            var existingPlayRecordCommentVote = TestObjectFactory.GetMockPlayRecordCommentVote(numericalValue: -1);
            var userId = _faker.Random.Guid().ToString();

            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordComments, bool>>>()))
                .Returns(playRecordComment);

            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordCommentVote, bool>>>()))
                .Returns(existingPlayRecordCommentVote);

            var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

            // Act
            subjectUnderTest.Upvote(playRecordComment.Id, userId);

            // Assert
            using (new AssertionScope())
            {
                mockGenericRepository.Verify(m => m.UpdateRecord(It.IsAny<PlayRecordCommentVote>()), Times.Once);
                existingPlayRecordCommentVote.NumericalValue.Should().Be(1);
            }
        }

        [Fact]
        public void Can_DownvotePreviouslyUpvoted_SubmitPlayRecordVote()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var playRecordComment = TestObjectFactory.GetMockPlayRecordComment();
            var existingPlayRecordCommentVote = TestObjectFactory.GetMockPlayRecordCommentVote(numericalValue: 1);
            var userId = _faker.Random.Guid().ToString();

            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordComments, bool>>>()))
                .Returns(playRecordComment);

            mockGenericRepository.Setup(m => m.GetSingleTracked(It.IsAny<Expression<Func<PlayRecordCommentVote, bool>>>()))
                .Returns(existingPlayRecordCommentVote);

            var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

            // Act
            subjectUnderTest.Downvote(playRecordComment.Id, userId);

            // Assert
            using (new AssertionScope())
            {
                mockGenericRepository.Verify(m => m.UpdateRecord(It.IsAny<PlayRecordCommentVote>()), Times.Once);
                existingPlayRecordCommentVote.NumericalValue.Should().Be(-1);
            }
        }

        [Fact]
        public void Error_NullPlayRecordComment_SubmitPlayRecordVote()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();

            mockGenericRepository.Setup(m => m.GetSingleTracked<PlayRecordComments>(It.IsAny<Expression<Func<PlayRecordComments, bool>>>()))
            .Returns((PlayRecordComments)null);

            var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

            // Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.Upvote(1, _faker.Random.Guid().ToString()));
                exception.Message.Should().Be("Can't find comment to vote on.");
            }
        }

        [Fact]
        public void Error_NullUser_SubmitPlayRecordVote()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var playRecordComment = TestObjectFactory.GetMockPlayRecordComment();

            mockGenericRepository.Setup(m => m.GetSingleTracked<PlayRecordComments>(It.IsAny<Expression<Func<PlayRecordComments, bool>>>()))
            .Returns(playRecordComment);

            var subjectUnderTest = new PlayRecordCommentService(mockGenericRepository.Object);

            // Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.Upvote(1, (string)null));
                exception.Message.Should().Be("No user found to vote. Ensure you are logged in.");
            }
        }
    }
}