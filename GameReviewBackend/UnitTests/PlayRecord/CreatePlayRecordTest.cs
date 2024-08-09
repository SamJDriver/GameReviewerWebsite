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

namespace UnitTests.PlayRecord
{
    public class CreatePlayRecordTest : BaseTest
    {
        [Fact]
        public void Can_CreatePlayRecord()
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
        public void Error_NullUser_CreatePlayRecord()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            var mockPlayRecord = TestObjectFactory.GetMockCreatePlayRecordDto();

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            // Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.CreatePlayRecord(mockPlayRecord, (string)null));
                exception.Message.Should().Be("Can't create play record. User not found.");
            }
        }

        [Fact]
        public void Error_NullGame_CreatePlayRecord()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            var mockPlayRecord = TestObjectFactory.GetMockCreatePlayRecordDto();

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            // Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() =>subjectUnderTest.CreatePlayRecord(mockPlayRecord, (string)_faker.Random.Guid().ToString()));
                exception.Message.Should().Be("Can't create play record. Game not found.");
            }
        }

        [Fact]
        public void Error_RatingOutOfRange_CreatePlayRecord()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            var mockGameEntity = TestObjectFactory.GetMockGameEntity();
            var mockPlayRecord = TestObjectFactory.GetMockCreatePlayRecordDto(gameId: mockGameEntity.Id);
            mockPlayRecord.Rating = _faker.Random.Int(0,1) == 1? _faker.Random.Int(-100, -1) : _faker.Random.Int(101, 1000);

            mockGenericRepository.Setup(m => m.GetById<Games>(It.IsAny<int>())).Returns(mockGameEntity);

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            // Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() => subjectUnderTest.CreatePlayRecord(mockPlayRecord, (string)_faker.Random.Guid().ToString()));
                exception.Message.Should().Be("Can't create play record. Rating out of range.");
            }
        }
    }
}