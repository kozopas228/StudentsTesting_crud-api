using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Tests_CRUD_BLL.Util.Mappers.Implementation;
using Tests_CRUD_DAL.Repositories.Interfaces;
using Xunit;

namespace Tests_CRUD_BLL.UnitTests.MapperTests
{
    public class TestMapperTests
    {
        private TestMapper mapper;
        private IAnswerRepository answerRepository;
        private IQuestionRepository questionRepository;
        private ITestRepository testRepository;
        private ITestThemeRepository testThemeRepository;

        private Fixture fixture;
        public TestMapperTests()
        {
            answerRepository = new Mock<IAnswerRepository>().Object;
            questionRepository = new Mock<IQuestionRepository>().Object;
            testRepository = new Mock<ITestRepository>().Object;
            testThemeRepository = new Mock<ITestThemeRepository>().Object;

            mapper = new TestMapper(testRepository, questionRepository, answerRepository, testThemeRepository);

            fixture = new Fixture();
        }

        [Fact]
        public void ToDto_ReturnsMapperObject()
        {
            //Arrange
            var entity = fixture.Build<Tests_CRUD_DAL.Entities.Test>()
                .Without(x=>x.Questions)
                .Without(x=>x.TestThemeId)
                .Without(x=>x.TestTheme)
                .Create();

            var dto = new Tests_CRUD_BLL.Models.Test
            {
                Id = entity.Id,
                Description = entity.Description,
                Name = entity.Name
            };

            //Act
            var result = mapper.ToDto(entity);

            //Assert
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Id, result.Id);
        }

        [Fact]
        public async Task ToEntity_ReturnsMappedObject()
        {
            //Arrange
            var dto = fixture.Create<Tests_CRUD_BLL.Models.Test>();

            var entity = new Tests_CRUD_DAL.Entities.Test
            {
                Id = dto.Id,
                Description = dto.Description,
                Name = dto.Name
            };

            //Act
            var result = await mapper.ToEntityAsync(dto);

            //Assert
            Assert.Equal(entity.Description, result.Description);
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(entity.Id, result.Id);
        }
    }
}