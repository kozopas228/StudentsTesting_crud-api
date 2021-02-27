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

            var listOfQuestions = new List<Models.Question>();

            foreach (var question in test.Questions)
            {
                var listOfAnswers = new List<Models.Answer>();

                listOfQuestions.Add(new Models.Question
                {
                    Id = question.Id,
                    TestId = question.TestId,
                    Text = question.Text,
                    Answers = listOfAnswers
                });

                foreach (var answer in question.Answers)
                {
                    listOfAnswers.Add(new Models.Answer
                    {
                        Id = answer.Id,
                        IsCorrect = answer.IsCorrect,
                        QuestionId = answer.QuestionId,
                        Text = answer.Text
                    });
                }

            }

            result.Questions = listOfQuestions;

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

            var testThemes = await TestThemeRepository.GetAllAsync();

            var theme = testThemes.First(x => x.Id == test.TestThemeId);

            result.TestTheme = theme;

            var listOfQuestions = new List<Tests_CRUD_DAL.Entities.Question>();

            var allQuestions = await this.QuestionRepository.GetAllAsync();

            foreach (var question in test.Questions)
            {
                if (allQuestions.Any(x=>x.Id == question.Id))
                {
                    listOfQuestions.Add(allQuestions.First(x=>x.Id==question.Id));
                }
                else
                {
                    var listOfAnswers = new List<Tests_CRUD_DAL.Entities.Answer>();

                    var quest = new Tests_CRUD_DAL.Entities.Question()
                    {
                        Id = question.Id,
                        Test = result,
                        TestId = test.Id,
                        Text = question.Text,
                        Answers = listOfAnswers
                    };

                    listOfQuestions.Add(quest);

                    foreach (var answer in question.Answers)
                    {
                        listOfAnswers.Add(new Tests_CRUD_DAL.Entities.Answer
                        {
                            Id = answer.Id,
                            IsCorrect = answer.IsCorrect,
                            Question = quest,
                            QuestionId = answer.QuestionId,
                            Text = answer.Text
                        });
                    }
                }
            }

            result.Questions = listOfQuestions;

            return result;
        }
    }
}