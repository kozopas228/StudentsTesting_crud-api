using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_CRUD_BLL.Util.Mappers.Interfaces;
using Tests_CRUD_DAL.Repositories.Interfaces;

namespace Tests_CRUD_BLL.Util.Mappers.Implementation
{
    public class TestMapper : ITestMapper
    {
        public ITestRepository TestRepository { get; set; }
        public IAnswerRepository AnswerRepository { get; set; }
        public IQuestionRepository QuestionRepository { get; set; }
        public ITestThemeRepository TestThemeRepository { get; set; }

        public TestMapper(ITestRepository testRepository, IQuestionRepository questionRepository, IAnswerRepository answerRepository, ITestThemeRepository testThemeRepository)
        {
            this.TestRepository = testRepository;
            this.QuestionRepository = questionRepository;
            this.AnswerRepository = answerRepository;
            this.TestThemeRepository = testThemeRepository;
        }

        public Models.Test ToDto(Tests_CRUD_DAL.Entities.Test test)
        {
            var result = new Models.Test
            {
                Description = test.Description,
                Id = test.Id,
                Name = test.Name,
                TestThemeId = test.TestThemeId,
            };

            if (test.Questions == null)
            {
                return result;
            }


            result.QuestionsIds = test.Questions.Select(x=>x.TestId).ToList();

            return result;
        }

        public async Task<Tests_CRUD_DAL.Entities.Test> ToEntityAsync(Models.Test test)
        {
            var result = new Tests_CRUD_DAL.Entities.Test
            {
                Description = test.Description,
                Id = test.Id,
                Name = test.Name,
                TestThemeId = test.TestThemeId,

            };

            if (result.TestThemeId == Guid.Empty)
            {
                result.TestThemeId = null;
            }

            var questions = await this.QuestionRepository.GetAllAsync();

            var resultQuestions = new List<Tests_CRUD_DAL.Entities.Question>();

            if (test.QuestionsIds != null)
            {
                foreach (var questionId in test.QuestionsIds)
                {
                    foreach (var question in questions)
                    {
                        if (questionId == question.Id)
                        {
                            resultQuestions.Add(question);
                            break;
                        }
                    }
                }
            }

            result.Questions = resultQuestions.ToList();

            return result;
        }
    }
}