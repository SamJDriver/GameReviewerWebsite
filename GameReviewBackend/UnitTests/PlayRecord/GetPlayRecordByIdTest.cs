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
    public class GetPlayRecordByIdTest : BaseTest
    {
        [Fact]
        public async Task Can_GetPlayRecordById()
        {
            //Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var graphServiceClient = new GraphServiceClient(new AnonymousAuthenticationProvider());
            MapsterTestConfiguration.GetMapper();

            var mockCovers = new List<Cover>() { TestObjectFactory.GetMockCover() };
            var mockGame = TestObjectFactory.GetMockGameEntity(covers: mockCovers);
            PlayRecords playRecord = TestObjectFactory.GetMockPlayRecord(game: mockGame);            

            mockGenericRepository.Setup(m => m.GetById<PlayRecords>(It.IsAny<int>())).Returns(playRecord);

            var subjectUnderTest = new PlayRecordService(mockGenericRepository.Object, graphServiceClient);

            //Act
            var retrievedRecord = await subjectUnderTest.GetPlayRecordById(playRecord.Id);

            //Assert
            using (new AssertionScope())
            {
                mockGenericRepository.Verify(m => m.GetById<PlayRecords>(It.IsAny<int>()), Times.Once);
            }
        }
    }
}