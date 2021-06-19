using AutoFixture;
using System.Collections.Generic;

namespace Tests_CRUD_BLL.UnitTests.ServiceTests.Util
{
    public class FakeDataForUnitTests
    {
        public List<Tests_CRUD_DAL.Entities.Answer> Answers { get; set; }
        public List<Tests_CRUD_DAL.Entities.Question> Questions { get; set; }
        public List<Tests_CRUD_DAL.Entities.Test> Tests { get; set; }
        public List<Tests_CRUD_DAL.Entities.TestTheme> TestThemes { get; set; }
        public Fixture Fixture { get; set; }
        public FakeDataForUnitTests()
        {
            this.Fixture = new Fixture();

            this.Answers = new List<Tests_CRUD_DAL.Entities.Answer>();
            this.Questions = new List<Tests_CRUD_DAL.Entities.Question>();
            this.Tests = new List<Tests_CRUD_DAL.Entities.Test>();
            this.TestThemes = new List<Tests_CRUD_DAL.Entities.TestTheme>();

            for (int i = 0; i < 10; i++)
            {
                this.Answers.Add(Fixture.Build<Tests_CRUD_DAL.Entities.Answer>().Without(x => x.Question).Without(x => x.QuestionId).Create());
                this.Questions.Add(Fixture.Build<Tests_CRUD_DAL.Entities.Question>().Without(x => x.Answers).Without(x => x.Test).Without(x => x.TestId).Create());
                this.Tests.Add(Fixture.Build<Tests_CRUD_DAL.Entities.Test>().Without(x => x.Questions).Without(x => x.TestThemeId).Without(x => x.TestTheme).Create());
                this.TestThemes.Add(Fixture.Build<Tests_CRUD_DAL.Entities.TestTheme>().Without(x => x.Tests).Create());
            }
        }
    }
}