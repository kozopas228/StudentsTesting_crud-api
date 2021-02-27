using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_CRUD_DAL.Entities;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Util.Mappers
{
    public class QuestionMapper
    {
        public IQuestionRepository QuestionRepository { get; set; }
        public IAnswerRepository AnswerRepository { get; set; }

        public QuestionMapper(IQuestionRepository questionRepository, IAnswerRepository answerRepository)
        {
            this.QuestionRepository = questionRepository;
            this.AnswerRepository = answerRepository;
        }

        public Models.Question ToDto(Tests_CRUD_DAL.Entities.Question question)
        {
            var result = new Models.Question
            {
                Id = question.Id,
                TestId = question.TestId,
                Text = question.Text,
            };

            var listOfAnswers = new List<Models.Answer>();

            foreach (var answer in question.Answers)
            {
                listOfAnswers.Add(new Models.Answer
                {
                    QuestionId = answer.QuestionId,
                    Id = answer.Id,
                    IsCorrect = answer.IsCorrect,
                    Text = answer.Text
                });
            }

            result.Answers = listOfAnswers;

            return result;

        }

        public async Task<Tests_CRUD_DAL.Entities.Question> ToEntityAsync(Models.Question question)
        {
            var result = new Tests_CRUD_DAL.Entities.Question
            {
                Id = question.Id,
                TestId = question.TestId,
                Text = question.Text
            };

            var listOfAnswers = new List<Tests_CRUD_DAL.Entities.Answer>();

            var allAnswers = await this.AnswerRepository.GetAllAsync();

            foreach (var answer in question.Answers)
            {
                if (allAnswers.Any(x => x.Id == answer.Id))
                {
                    listOfAnswers.Add(allAnswers.First(x=>x.Id == answer.Id));
                }
                else
                {
                    listOfAnswers.Add(new Answer
                    {
                        Id = answer.Id,
                        IsCorrect = answer.IsCorrect,
                        QuestionId = answer.QuestionId,
                        Text = answer.Text,
                        Question = result
                    });
                }
            }

            result.Answers = listOfAnswers;

            return result;
        }
    }
}