using System.Linq;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Util.Mappers.Implementation
{
    public class TestThemeMapper : ITestThemeMapper
    {
        public ITestRepository TestRepository { get; set; }
        public IAnswerRepository AnswerRepository { get; set; }
        public IQuestionRepository QuestionRepository { get; set; }
        public ITestThemeRepository TestThemeRepository { get; set; }
        public TestThemeMapper(ITestRepository testRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, ITestThemeRepository testThemeRepository)
        {
            this.TestRepository = testRepository;
            this.QuestionRepository = questionRepository;
            this.AnswerRepository = answerRepository;
            this.TestThemeRepository = testThemeRepository;
        }

        public Models.TestTheme ToDto(Tests_CRUD_DAL.Entities.TestTheme theme)
        {
            return new Models.TestTheme
            {
                Id = theme.Id,
                Name = theme.Name
            };
        }

        public async Task<Tests_CRUD_DAL.Entities.TestTheme> ToEntityAsync(Models.TestTheme theme)
        {
            var result = new Tests_CRUD_DAL.Entities.TestTheme
            {
                Id = theme.Id,
                Name = theme.Name,
            };

            var allTests = await this.TestRepository.GetAllAsync();

            var matchTests = allTests.Where(x => x.TestThemeId == theme.Id).ToList();

            result.Tests = matchTests;

            return result;
        }
    }
}