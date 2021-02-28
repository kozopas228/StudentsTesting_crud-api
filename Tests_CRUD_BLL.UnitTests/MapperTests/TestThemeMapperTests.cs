using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Tests_CRUD_BLL.Util.Mappers.Implementation;
using Tests_CRUD_DAL.Repositories.Interfaces;
using Xunit;

namespace Tests_CRUD_BLL.UnitTests.MapperTests
{
    public class TestThemeMapperTests
    {
        private TestThemeMapper mapper;
        private IAnswerRepository answerRepository;
        private IQuestionRepository questionRepository;
        private ITestRepository testRepository;
        private ITestThemeRepository testThemeRepository;

        private Fixture fixture;
        public TestThemeMapperTests()
        {
            answerRepository = new Mock<IAnswerRepository>().Object;
            questionRepository = new Mock<IQuestionRepository>().Object;
            testRepository = new Mock<ITestRepository>().Object;
            testThemeRepository = new Mock<ITestThemeRepository>().Object;

            mapper = new TestThemeMapper(testRepository, questionRepository, answerRepository, testThemeRepository);

            fixture = new Fixture();
        }

        [Fact]
        public void ToDto_ReturnsMapperObject()
        {
            //Arrange
            var entity = fixture.Build<Tests_CRUD_DAL.Entities.TestTheme>()
                .Without(x=>x.Tests)
                .Create();

            var dto = new Tests_CRUD_BLL.Models.TestTheme
            {
                Id = entity.Id,
                Name = entity.Name
            };

            //Act
            var result = mapper.ToDto(entity);

            //Assert
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Id, result.Id);
        }

        [Fact]
        public async Task ToEntity_ReturnsMappedObject()
        {
            //Arrange
            var dto = fixture.Create<Tests_CRUD_BLL.Models.TestTheme>();

            var entity = new Tests_CRUD_DAL.Entities.TestTheme
            {
                Id = dto.Id,
                Name = dto.Name
            };

            //Act
            var result = await mapper.ToEntityAsync(dto);

            //Assert
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.Id, result.Id);
        }
    }
}