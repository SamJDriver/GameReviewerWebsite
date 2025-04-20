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
    public class GetSelfPlayRecordsTest : BaseTest
    {
        [Fact]
        public async Task Can_GetSelfPlayRecords()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            var userId = Guid.NewGuid().ToString();
            MapsterTestConfiguration.GetMapper();

            List<PlayRecords> playRecords = new();
            int playRecordCount = _faker.Random.Number(0, 10000);

            for (int i = 0; i < playRecordCount; i++)
            {
                var mockCovers = new List<Cover>() { TestObjectFactory.GetMockCover() };
                var mockGame = TestObjectFactory.GetMockGameEntity(covers: mockCovers);
                var mockPlayRecord = TestObjectFactory.GetMockPlayRecord(game: mockGame, createdBy: userId);
                playRecords.Add(mockPlayRecord);
            }

            mockGenericRepository.Setup(m => m.GetMany<PlayRecords>(It.IsAny<Expression<Func<PlayRecords, bool>>>())).Returns(playRecords.AsAsyncQueryable());

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            //Act
            var retrievedRecords = await subjectUnderTest.GetPlayRecords(null, userId);

            //Assert
            using (new AssertionScope())
            {
                mockGenericRepository.Verify(m => m.GetMany(It.IsAny<Expression<Func<PlayRecords, bool>>>()), Times.Once);
                retrievedRecords.Count().Should().Be(playRecordCount);
            }
        }

        [Fact]
        public async Task Error_NullUser_GetSelfPlayRecords()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            //Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = await Assert.ThrowsAsync<DgcException>(() => subjectUnderTest.GetPlayRecords(null, (string)null));
                exception.Message.Should().Be("Can't create play record. User not found.");
            }

        }
    }
}