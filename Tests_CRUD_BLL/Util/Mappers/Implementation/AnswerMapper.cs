using System.Threading.Tasks;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Util.Mappers
{
    public class AnswerMapper
    {
        public IAnswerRepository AnswerRepository { get; set; }

        public AnswerMapper(IAnswerRepository answerRepository)
        {
            this.AnswerRepository = answerRepository;
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

        public Task<Tests_CRUD_DAL.Entities.Answer> ToEntity(Models.Answer answer)
        {
            var result = new Tests_CRUD_DAL.Entities.Answer
            {
                Id = answer.Id,
                IsCorrect = answer.IsCorrect,
                QuestionId = answer.QuestionId,
                Text = answer.Text
            };

            return Task.FromResult(result);
        }
    }
}