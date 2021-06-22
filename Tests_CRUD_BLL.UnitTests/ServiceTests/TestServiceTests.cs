﻿using AutoFixture;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Services.Implementation;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_BLL.UnitTests.ServiceTests.Util;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;
using Xunit;

namespace Tests_CRUD_BLL.UnitTests.ServiceTests
{
    public class TestServiceTests
    {
        private Mock<ITestRepository> repositoryMock;
        private Mock<ITestMapper> mapperMock;
        private ITestService service;
        private Fixture fixture;
        private FakeDataForUnitTests fakeDataForUnitTests;
        public TestServiceTests()
        {
            fixture = new Fixture();

            fakeDataForUnitTests = new FakeDataForUnitTests();

            repositoryMock = new Mock<ITestRepository>();
            repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(this.fakeDataForUnitTests.Tests);

            mapperMock = new Mock<ITestMapper>();
            service = new TestService(repositoryMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task GetAsync_RepositoryInvokes()
        {
            //Act
            await service.GetAllAsync();

            //Arrange
            repositoryMock.Verify(x => x.GetAllAsync());
        }

        [Fact]
        public async Task GetAsync_MapperInvokes()
        {
            //Act
            await service.GetAllAsync();

            //Assert
            mapperMock.Verify(x => x.ToDto(It.IsAny<Tests_CRUD_DAL.Entities.Test>()));
        }

        [Fact]
        public async Task DeleteAsync_RepositoryInvokes()
        {
            //Arrange
            var lastObject = (await this.repositoryMock.Object.GetAllAsync()).Last();
            var idToDelete = lastObject.Id;

            //Act
            await service.DeleteAsync(idToDelete);

            //Assert
            this.repositoryMock.Verify(x => x.DeleteAsync(idToDelete));
        }

        [Fact]
        public async Task UpdateAsync_RepositoryInvokes()
        {
            //Arrange
            var lastObject = (await this.repositoryMock.Object.GetAllAsync()).Last();
            lastObject.Name = "ahaha";

            var mappedObject = new Tests_CRUD_BLL.Models.Test
            {
                Id = lastObject.Id,
                Name = lastObject.Name
            };

            //Act
            await service.UpdateAsync(mappedObject);

            //Assert
            this.repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Tests_CRUD_DAL.Entities.Test>()));
        }

        [Fact]
        public async Task UpdateAsync_MapperInvokes()
        {
            //Arrange
            var lastObject = (await this.repositoryMock.Object.GetAllAsync()).Last();
            lastObject.Name = "ahaha";

            var mappedObject = new Tests_CRUD_BLL.Models.Test
            {
                Id = lastObject.Id,
                Name = lastObject.Name
            };

            //Act
            await service.UpdateAsync(mappedObject);

            //Assert
            this.mapperMock.Verify(x => x.ToEntityAsync(mappedObject));

        }

        [Fact]
        public async Task CreateAsync_RepositoryInvokes()
        {
            //Arrange
            var lastObject = (await this.repositoryMock.Object.GetAllAsync()).Last();
            lastObject.Name = "ahaha";

            var mappedObject = new Tests_CRUD_BLL.Models.Test
            {
                Id = lastObject.Id,
                Name = lastObject.Name
            };

            //Act
            await service.CreateAsync(mappedObject);

            //Assert
            this.repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Tests_CRUD_DAL.Entities.Test>()));
        }

        [Fact]
        public async Task CreateAsync_MapperInvokes()
        {
            //Arrange
            var lastObject = (await this.repositoryMock.Object.GetAllAsync()).Last();
            lastObject.Name = "ahaha";

            var mappedObject = new Tests_CRUD_BLL.Models.Test
            {
                Id = lastObject.Id,
                Name = lastObject.Name
            };

            //Act
            await service.CreateAsync(mappedObject);

            //Assert
            this.mapperMock.Verify(x => x.ToEntityAsync(mappedObject));

        }
    }
}