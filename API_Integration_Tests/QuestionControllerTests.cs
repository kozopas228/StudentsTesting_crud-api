using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using API_Integration_Tests.Util;
using AutoFixture;
using Newtonsoft.Json;
using Xunit;

namespace API_Integration_Tests
{
    public class QuestionControllerTests
    {
        private BaseTestFixture _fixture;

        public QuestionControllerTests()
        {
            _fixture = new BaseTestFixture();
        }

        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            // Act
            var response = await _fixture.Client.GetAsync("api/Question");
            response.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<IEnumerable<Tests_CRUD_BLL.Models.Question>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task Post_ShouldCreateNewQuestion()
        {
            //Arrange
            var fixture = new Fixture();
            var fakeQuestion = fixture
                .Build<Tests_CRUD_BLL.Models.Question>()
                .With(x => x.Text, "test1")
                .Create();
            var fakeQuestionJson = JsonConvert.SerializeObject(fakeQuestion);
            var httpContent = new StringContent(fakeQuestionJson, System.Text.Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PostAsync("api/Question", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.DbContext.Questions, question => question.Text == "test1");

        }

        [Fact]
        public async Task Put_ShouldUpdateExistingQuestion()
        {
            //Arrange
            var question = _fixture.DbContext.Questions.Last();
            question.Text = "test2";
            var questionJson = JsonConvert.SerializeObject(question);

            var httpContent = new StringContent(questionJson, System.Text.Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PutAsync("api/Question", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.DbContext.Questions, x => x.Text == "test2");
        }

        [Fact]
        public async Task Delete_ShouldDeleteExistingQuestion()
        {
            //Arrange
            var fixture = new Fixture();
            var question = fixture.Build<Tests_CRUD_DAL.Entities.Question>().Without(x=>x.Answers).Without(x=>x.Test).Without(x=>x.TestId).Create();
            var questionJson = JsonConvert.SerializeObject(question);
            var httpContent = new StringContent(questionJson, System.Text.Encoding.UTF8, "application/json");

            await _fixture.Client.PostAsync("api/Question", httpContent);

            //Act
            var response = await _fixture.Client.DeleteAsync("api/Question/" + question.Id);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.DoesNotContain(_fixture.DbContext.Questions, x => x.Id == question.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnExistingQuestion()
        {
            //Arrange
            var fixture = new Fixture();
            var question = fixture.Build<Tests_CRUD_DAL.Entities.Question>().Without(x => x.Answers)
                .Without(x => x.Test).Without(x => x.TestId).Create();
            var questionJson = JsonConvert.SerializeObject(question);
            var httpContent = new StringContent(questionJson, System.Text.Encoding.UTF8, "application/json");

            await _fixture.Client.PostAsync("api/Question", httpContent);

            //Act
            var response = await _fixture.Client.GetAsync("api/Question/" + question.Id);
            response.EnsureSuccessStatusCode();

            var responseObject = JsonConvert.DeserializeObject<Tests_CRUD_DAL.Entities.Question>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(question.Id, responseObject.Id);
        }

        [Fact]
        public async Task GetById_QuestionDoesntExist_ShouldReturn_NotFound()
        {
            //Act
            var response = await _fixture.Client.GetAsync("api/Question/" + Guid.NewGuid());

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}