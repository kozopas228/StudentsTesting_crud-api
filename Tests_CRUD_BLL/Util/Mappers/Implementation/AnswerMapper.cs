using System;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Util.Mappers.Implementation
{
    public class AnswerMapper : IAnswerMapper
    {
        public ITestRepository TestRepository { get; set; }
        public IAnswerRepository AnswerRepository { get; set; }
        public IQuestionRepository QuestionRepository { get; set; }
        public ITestThemeRepository TestThemeRepository { get; set; }

        public AnswerMapper(ITestRepository testRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, ITestThemeRepository testThemeRepository)
        {
            this.TestRepository = testRepository;
            this.QuestionRepository = questionRepository;
            this.AnswerRepository = answerRepository;
            this.TestThemeRepository = testThemeRepository;
        }

        public Models.Answer ToDto(Tests_CRUD_DAL.Entities.Answer answer)
        {
            var result = new Models.Answer
            {
                Id = answer.Id,
                IsCorrect = answer.IsCorrect,
                QuestionId = answer.QuestionId,
                Text = answer.Text
            };

            return result;
        }

        public Task<Tests_CRUD_DAL.Entities.Answer> ToEntityAsync(Models.Answer answer)
        {
            var result = new Tests_CRUD_DAL.Entities.Answer
            {
                Id = answer.Id,
                IsCorrect = answer.IsCorrect,
                QuestionId = answer.QuestionId,
                Text = answer.Text
            };

            if (result.QuestionId == Guid.Empty)
            {
                result.QuestionId = null;
            }

            return Task.FromResult(result);
        }
    }
}