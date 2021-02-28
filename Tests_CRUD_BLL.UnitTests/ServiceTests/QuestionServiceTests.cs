using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Tests_CRUD_BLL.Services.Implementation;
using Tests_CRUD_BLL.Services.Interfaces;
using Tests_CRUD_BLL.UnitTests.ServiceTests.Util;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;
using Xunit;

namespace Tests_CRUD_BLL.UnitTests.ServiceTests
{
    public class QuestionServiceTests
    {
        private Mock<IQuestionRepository> repositoryMock;
        private Mock<IQuestionMapper> mapperMock;
        private IQuestionService service;
        private Fixture fixture;
        private FakeDataForUnitTests fakeDataForUnitTests;
        public QuestionServiceTests()
        {
            fixture = new Fixture();

            fakeDataForUnitTests = new FakeDataForUnitTests();

            repositoryMock = new Mock<IQuestionRepository>();
            repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(this.fakeDataForUnitTests.Questions);

            mapperMock = new Mock<IQuestionMapper>();
            service = new QuestionService(repositoryMock.Object, mapperMock.Object);
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
            mapperMock.Verify(x => x.ToDto(It.IsAny<Tests_CRUD_DAL.Entities.Question>()));
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
            lastObject.Text = "ahaha";

            var mappedObject = new Tests_CRUD_BLL.Models.Question
            {
                Id = lastObject.Id,
                Text = lastObject.Text
            };

            //Act
            await service.UpdateAsync(mappedObject);

            //Assert
            this.repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Tests_CRUD_DAL.Entities.Question>()));
        }

        [Fact]
        public async Task UpdateAsync_MapperInvokes()
        {
            //Arrange
            var lastObject = (await this.repositoryMock.Object.GetAllAsync()).Last();
            lastObject.Text = "ahaha";

            var mappedObject = new Tests_CRUD_BLL.Models.Question
            {
                Id = lastObject.Id,
                Text = lastObject.Text
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
            lastObject.Text = "ahaha";

            var mappedObject = new Tests_CRUD_BLL.Models.Question
            {
                Id = lastObject.Id,
                Text = lastObject.Text
            };

            //Act
            await service.CreateAsync(mappedObject);

            //Assert
            this.repositoryMock.Verify(x => x.CreateAsync(It.IsAny<Tests_CRUD_DAL.Entities.Question>()));
        }

        [Fact]
        public async Task CreateAsync_MapperInvokes()
        {
            //Arrange
            var lastObject = (await this.repositoryMock.Object.GetAllAsync()).Last();
            lastObject.Text = "ahaha";

            var mappedObject = new Tests_CRUD_BLL.Models.Question
            {
                Id = lastObject.Id,
                Text = lastObject.Text
            };

            //Act
            await service.CreateAsync(mappedObject);

            //Assert
            this.mapperMock.Verify(x => x.ToEntityAsync(mappedObject));

        }
    }
}