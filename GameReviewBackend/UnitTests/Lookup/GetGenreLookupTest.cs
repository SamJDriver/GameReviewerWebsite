using BusinessLogic.Infrastructure;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Repositories;

namespace UnitTests.Lookup
{
    public class GetGenreLookupTest : BaseTest
    {
        [Fact]
        public void Can_GetGenreLookups()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            
            List<GenresLookup> genreLookups = new();
            for (int i = 0; i < _faker.Random.Int(1, 100); i++)
            {
                genreLookups.Add(TestObjectFactory.GetMockGenresLookup());
            }
            mockGenericRepository.Setup(m => m.GetAll<GenresLookup>())
            .Returns(genreLookups.AsQueryable());

            var subjectUnderTest = new LookupService(mockGenericRepository.Object);
            
            // Act
            var genreLookupsResult = subjectUnderTest.GetGenreLookups();

            // Assert
            using (new AssertionScope())
            {
                genreLookupsResult.Count().Should().Be(genreLookups.Count);
            }
        }
    }
}