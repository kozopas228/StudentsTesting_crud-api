using System;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Tests_CRUD_BLL.Util.Mappers.Implementation;
using Tests_CRUD_DAL.Repositories.Interfaces;
using Xunit;

namespace Tests_CRUD_BLL.UnitTests.MapperTests
{
    public class QuestionMapperTests
    {
        private QuestionMapper mapper;
        private IAnswerRepository answerRepository;
        private IQuestionRepository questionRepository;
        private ITestRepository testRepository;
        private ITestThemeRepository testThemeRepository;

        private Fixture fixture;
        public QuestionMapperTests()
        {
            answerRepository = new Mock<IAnswerRepository>().Object;
            questionRepository = new Mock<IQuestionRepository>().Object;
            testRepository = new Mock<ITestRepository>().Object;
            testThemeRepository = new Mock<ITestThemeRepository>().Object;

            mapper = new QuestionMapper(testRepository, questionRepository, answerRepository, testThemeRepository);

            fixture = new Fixture();
        }

        [Fact]
        public void ToDto_ReturnsMapperObject()
        {
            //Arrange
            var entity = fixture.Build<Tests_CRUD_DAL.Entities.Question>()
                .Without(x=>x.Answers)
                .Without(x=>x.Test)
                .Without(x=>x.TestId)
                .Create();

            var dto = new Tests_CRUD_BLL.Models.Question
            {
                Id = entity.Id,
                Text = entity.Text
            };

            //Act
            var result = mapper.ToDto(entity);

            //Assert
            Assert.Equal(dto.Text, result.Text);
            Assert.Equal(dto.Id, result.Id);
        }

        [Fact]
        public async Task ToEntity_ReturnsMappedObject()
        {
            //Arrange
            var dto = fixture.Create<Tests_CRUD_BLL.Models.Question>();

            var entity = new Tests_CRUD_DAL.Entities.Question
            {
                Id = dto.Id,
                Text = dto.Text
            };

            //Act
            var result = await mapper.ToEntityAsync(dto);

            //Assert
            Assert.Equal(entity.Text, result.Text);
            Assert.Equal(entity.Id, result.Id);
        }
    }
}