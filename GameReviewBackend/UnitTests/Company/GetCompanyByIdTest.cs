using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Infrastructure;
using Components.Exceptions;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Repositories;
using Xunit;

namespace UnitTests.Company
{
    public class GetCompanyByIdTest
    {
        [Fact]
        public void Can_GetCompanyById()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();
            var mockCompany = TestObjectFactory.GetMockCompany();

            mockGenericRepository.Setup(m => m.GetById<Companies>(It.IsAny<int>())).Returns(mockCompany);
            var subjectUnderTest = new CompanyService(mockGenericRepository.Object);

            // Act
            var result = subjectUnderTest.GetCompanyById(mockCompany.Id);

            // Assert
            using (new AssertionScope())
            {
                mockGenericRepository.Verify(m => m.GetById<Companies>(It.IsAny<int>()), Times.Once);
                result.Id.Should().Be(mockCompany.Id);
                result.Name.Should().Be(mockCompany.Name);
                result.FoundedDate.Should().Be(mockCompany.FoundedDate);
                result.ImageFilePath.Should().Be(mockCompany.ImageFilePath);
            }
        }

        [Fact]
        public void Error_CompanyNotFound_GetCompanyById()
        {
            // Arrange
            Mock<IGenericRepository<DockerDbContext>> mockGenericRepository = new();

            mockGenericRepository.Setup(m => m.GetById<Companies>(It.IsAny<int>())).Returns((Companies)null);

            // Act & Assert
            using (new AssertionScope())
            {
                DgcException exception = Assert.Throws<DgcException>(() => new CompanyService(mockGenericRepository.Object).GetCompanyById(1));
                exception.Message.Should().Be("Company not found.");
            }
        }
    }
}