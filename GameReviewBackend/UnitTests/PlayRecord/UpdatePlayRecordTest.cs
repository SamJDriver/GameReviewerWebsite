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

namespace UnitTests.PlayRecord
{
    public class UpdatePlayRecordTest : BaseTest
    {
        [Fact]
        public async Task Can_UpdatePlayRecord()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            var userId = _faker.Random.Guid().ToString();

            var mockGameEntity = TestObjectFactory.GetMockGameEntity();
            var mockNewPlayRecord = TestObjectFactory.GetMockUpdatePlayRecordDto();
            var mockExistingPlayRecord = TestObjectFactory.GetMockPlayRecord(gameId: mockGameEntity.Id, createdBy: userId);

            mockGenericRepository.Setup(m => m.GetSingleNoTrack<PlayRecords>(It.IsAny<Expression<Func<PlayRecords, bool>>>())).Returns(mockExistingPlayRecord);
            mockGenericRepository.Setup(m => m.GetById<Games>(It.IsAny<int>())).Returns(mockGameEntity);

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            // Act
            subjectUnderTest.UpdatePlayRecord(mockExistingPlayRecord.Id, mockNewPlayRecord, userId);

            // Assert
            using (new AssertionScope())
            {
                mockGenericRepository.Verify(m => m.UpdateRecord(It.IsAny<PlayRecords>()), Times.Once);
            }
        }

        [Fact]
        public async Task Error_NullPlayRecordFound_UpdatePlayRecord()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            var mockPlayRecord = TestObjectFactory.GetMockUpdatePlayRecordDto();

            mockGenericRepository.Setup(m => m.GetSingleNoTrack<PlayRecords>(It.IsAny<Expression<Func<PlayRecords, bool>>>())).Returns((PlayRecords)null);

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            // Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() =>subjectUnderTest.UpdatePlayRecord(_faker.Random.Int(1, 1000), mockPlayRecord, _faker.Random.Guid().ToString()));
                exception.Message.Should().Be("Can't update Play Record. Play Record not found.");
            }
        }

        [Fact]
        public async Task Error_NullUser_UpdatePlayRecord()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            var mockPlayRecord = TestObjectFactory.GetMockUpdatePlayRecordDto();
            var mockPlayRecordEntity = TestObjectFactory.GetMockPlayRecord();

            mockGenericRepository.Setup(m => m.GetSingleNoTrack<PlayRecords>(It.IsAny<Expression<Func<PlayRecords, bool>>>())).Returns(mockPlayRecordEntity);

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            // Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.UpdatePlayRecord(_faker.Random.Int(1, 1000), mockPlayRecord, (string)null));
                exception.Message.Should().Be("Can't update Play Record. User not found.");
            }
        }

        [Fact]
        public async Task Error_OtherUsersPlayRecord_UpdatePlayRecord()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            var userId = _faker.Random.Guid().ToString();

            var mockNewPlayRecord = TestObjectFactory.GetMockUpdatePlayRecordDto();
            var mockExistingPlayRecord = TestObjectFactory.GetMockPlayRecord( createdBy: userId);

            mockGenericRepository.Setup(m => m.GetById<Games>(It.IsAny<int>())).Returns(TestObjectFactory.GetMockGameEntity());
            mockGenericRepository.Setup(m => m.GetSingleNoTrack<PlayRecords>(It.IsAny<Expression<Func<PlayRecords, bool>>>())).Returns(mockExistingPlayRecord);

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            // Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() =>subjectUnderTest.UpdatePlayRecord(mockExistingPlayRecord.Id, mockNewPlayRecord,_faker.Random.Guid().ToString()));
                exception.Message.Should().Be("You cannot update another user's play record.");
            }
        }

        [Fact]
        public async Task Error_NullGame_UpdatePlayRecord()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            var userId = _faker.Random.Guid().ToString();

            var mockNewPlayRecord = TestObjectFactory.GetMockUpdatePlayRecordDto();
            var mockExistingPlayRecord = TestObjectFactory.GetMockPlayRecord(createdBy: userId);

            mockGenericRepository.Setup(m => m.GetSingleNoTrack<PlayRecords>(It.IsAny<Expression<Func<PlayRecords, bool>>>())).Returns(mockExistingPlayRecord);
            mockGenericRepository.Setup(m => m.GetById<Games>(It.IsAny<int>())).Returns((Games)null);

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            //Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() =>subjectUnderTest.UpdatePlayRecord(mockExistingPlayRecord.Id, mockNewPlayRecord, userId));
                exception.Message.Should().Be("Can't update Play Record. Game not found.");
            }
        }

        [Fact]
        public async Task Error_InvalidRating_UpdatePlayRecord()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            var userId = _faker.Random.Guid().ToString();

            var outOfRangeRating = _faker.Random.Int(0, 1) == 1 ? _faker.Random.Int(-100, -1) : _faker.Random.Int(101, 1000);
            var mockNewPlayRecord = TestObjectFactory.GetMockUpdatePlayRecordDto(rating: outOfRangeRating);
            var mockExistingPlayRecord = TestObjectFactory.GetMockPlayRecord(createdBy: userId);

            mockGenericRepository.Setup(m => m.GetSingleNoTrack<PlayRecords>(It.IsAny<Expression<Func<PlayRecords, bool>>>())).Returns(mockExistingPlayRecord);
            mockGenericRepository.Setup(m => m.GetById<Games>(It.IsAny<int>())).Returns(TestObjectFactory.GetMockGameEntity());

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            //Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() =>subjectUnderTest.UpdatePlayRecord(mockExistingPlayRecord.Id, mockNewPlayRecord, userId));
                exception.Message.Should().Be("Can't update play record. Rating out of range.");
            }
        }
    }
}