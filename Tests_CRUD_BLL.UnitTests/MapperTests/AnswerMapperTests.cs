using System;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Tests_CRUD_BLL.Util.Mappers.Implementation;
using Tests_CRUD_DAL.Repositories.Interfaces;
using Xunit;

namespace Tests_CRUD_BLL.UnitTests.MapperTests
{
    public class AnswerMapperTests
    {
        private AnswerMapper mapper;
        private IAnswerRepository answerRepository;
        private IQuestionRepository questionRepository;
        private ITestRepository testRepository;
        private ITestThemeRepository testThemeRepository;

        private Fixture fixture;
        public AnswerMapperTests()
        {
            answerRepository = new Mock<IAnswerRepository>().Object;
            questionRepository = new Mock<IQuestionRepository>().Object;
            testRepository = new Mock<ITestRepository>().Object;
            testThemeRepository = new Mock<ITestThemeRepository>().Object;

            mapper = new AnswerMapper(testRepository, questionRepository, answerRepository, testThemeRepository);

            fixture = new Fixture();
        }

        [Fact]
        public void ToDto_ReturnsMapperObject()
        {
            //Arrange
            var entity = fixture.Build<Tests_CRUD_DAL.Entities.Answer>()
                .Without(x => x.QuestionId)
                .Without(x => x.Question)
                .Create();

            var dto = new Tests_CRUD_BLL.Models.Answer
            {
                Id = entity.Id,
                IsCorrect = entity.IsCorrect,
                QuestionId = entity.QuestionId,
                Text = entity.Text
            };

            //Act
            var result = mapper.ToDto(entity);

            //Assert
            Assert.Equal(dto.Text, result.Text);
            Assert.Equal(dto.QuestionId, result.QuestionId);
            Assert.Equal(dto.Id, result.Id);
            Assert.Equal(dto.IsCorrect, result.IsCorrect);
        }

        [Fact]
        public async Task ToEntity_ReturnsMappedObject()
        {
            //Arrange
            var dto = fixture.Create<Tests_CRUD_BLL.Models.Answer>();

            var entity = new Tests_CRUD_DAL.Entities.Answer
            {
                Id = dto.Id,
                IsCorrect = dto.IsCorrect,
                QuestionId = dto.QuestionId,
                Text = dto.Text
            };

            //Act
            var result = await mapper.ToEntityAsync(dto);

            //Assert
            Assert.Equal(entity.Text, result.Text);
            Assert.Equal(entity.QuestionId, result.QuestionId);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.IsCorrect, result.IsCorrect);
        }

        [Fact]
        public async Task ToEntity_QuestionIdEmpty_QuestionIdBecomesNull()
        {
            //Arrange
            var dto = fixture.Create<Tests_CRUD_BLL.Models.Answer>();
            dto.QuestionId = Guid.Empty;

            var entity = new Tests_CRUD_DAL.Entities.Answer
            {
                Id = dto.Id,
                IsCorrect = dto.IsCorrect,
                QuestionId = dto.QuestionId,
                Text = dto.Text
            };

            //Act
            var result = await mapper.ToEntityAsync(dto);

            //Assert
            Assert.Null(result.QuestionId);
        }
    }
}