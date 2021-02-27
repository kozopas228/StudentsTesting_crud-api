using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Util.Mappers.Implementation
{
    public class QuestionMapper : IQuestionMapper
    {
        public IQuestionRepository QuestionRepository { get; set; }
        public ITestThemeRepository TestThemeRepository { get; set; }
        public ITestRepository TestRepository { get; set; }
        public IAnswerRepository AnswerRepository { get; set; }

        public QuestionMapper(ITestRepository testRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, ITestThemeRepository testThemeRepository)
        {
            this.TestRepository = testRepository;
            this.QuestionRepository = questionRepository;
            this.AnswerRepository = answerRepository;
            this.TestThemeRepository = testThemeRepository;
        }

        public Models.Question ToDto(Tests_CRUD_DAL.Entities.Question question)
        {
            var result = new Models.Question
            {
                Id = question.Id,
                TestId = question.TestId,
                Text = question.Text,
            };

            if (question.Answers == null)
            {
                return result;
            }

            result.AnswersIds = question.Answers.Select(x=>(Guid?)x.Id).ToList();

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

            if (result.TestId == Guid.Empty)
            {
                result.TestId = null;
            }

            var allAnswers = await AnswerRepository.GetAllAsync();

            var answerList = new List<Tests_CRUD_DAL.Entities.Answer>();

            foreach (var answer in allAnswers)
            {
                foreach (var answ in question.AnswersIds)
                {
                    if (answ == answer.Id)
                    {
                        answerList.Add(answer);
                        break;
                    }
                }
            }

            result.Answers = answerList;

            return result;
        }
    }
}