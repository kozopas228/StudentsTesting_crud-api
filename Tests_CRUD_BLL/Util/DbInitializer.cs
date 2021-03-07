using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Tests_CRUD_DAL;

namespace Tests_CRUD_BLL.Util
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            if (context.Answers.Any())
            {
                return;
            }

            var fixture = new Fixture();

            var fakeAnswers = new List<Tests_CRUD_DAL.Entities.Answer>();

            for (int i = 0; i < 100; i++)
            {
                var answer = fixture.Build<Tests_CRUD_DAL.Entities.Answer>()
                    .Without(x => x.QuestionId)
                    .Without(x => x.Question)
                    .Create();

                fakeAnswers.Add(answer);
            }

            context.Answers.AddRange(fakeAnswers);

            context.SaveChanges();






            var fakeQuestions = new List<Tests_CRUD_DAL.Entities.Question>();

            for (int i = 0; i < 25; i++)
            {
                var question = fixture.Build<Tests_CRUD_DAL.Entities.Question>()
                    .Without(x => x.Answers)
                    .Without(x => x.Test)
                    .Without(x => x.TestId)
                    .Create();

                question.Answers = fakeAnswers.Skip(i * 4).Take(4).ToList();

                fakeQuestions.Add(question);
            }

            context.Questions.AddRange(fakeQuestions);

            context.SaveChanges();






            var fakeTests = new List<Tests_CRUD_DAL.Entities.Test>();

            for (int i = 0; i < 5; i++)
            {
                var test = fixture.Build<Tests_CRUD_DAL.Entities.Test>()
                    .Without(x => x.Questions)
                    .Without(x => x.TestTheme)
                    .Without(x => x.TestThemeId)
                    .Create();

                test.Questions = fakeQuestions.Skip(i * 5).Take(5).ToList();

                fakeTests.Add(test);

            }

            context.Tests.AddRange(fakeTests);

            context.SaveChanges();






            var fakeThemes = new List<Tests_CRUD_DAL.Entities.TestTheme>();

            var testThemes = fixture.Build<Tests_CRUD_DAL.Entities.TestTheme>()
                .Without(x => x.Tests)
                .CreateMany(2);

            fakeThemes = testThemes.ToList();

            fakeThemes[0].Tests = fakeTests.Take(2).ToList();
            fakeThemes[1].Tests = fakeTests.Skip(2).Take(3).ToList();

            context.TestThemes.AddRange(fakeThemes);

            context.SaveChanges();



        }
    }
}