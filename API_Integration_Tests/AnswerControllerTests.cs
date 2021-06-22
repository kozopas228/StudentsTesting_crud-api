using API_Integration_Tests.Util;
using AutoFixture;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace API_Integration_Tests
{
    public class AnswerControllerTests
    {
        private BaseTestFixture _fixture;

        public AnswerControllerTests()
        {
            _fixture = new BaseTestFixture();
        }

        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            // Act
            var response = await _fixture.Client.GetAsync("api/Answer");
            response.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<IEnumerable<Tests_CRUD_BLL.Models.Answer>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task Post_ShouldCreateNewAnswer()
        {
            //Arrange
            var fixture = new Fixture();
            var fakeAnswer = fixture
                .Build<Tests_CRUD_BLL.Models.Answer>()
                .With(x => x.Text, "test1")
                .Create();
            var fakeAnswerJson = JsonConvert.SerializeObject(fakeAnswer);
            var httpContent = new StringContent(fakeAnswerJson, System.Text.Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PostAsync("api/Answer", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.DbContext.Answers, answer => answer.Text == "test1");

        }

        [Fact]
        public async Task Put_ShouldUpdateExistingAnswer()
        {
            //Arrange
            var answer = _fixture.DbContext.Answers.Last();
            answer.Text = "test2";
            var answerJson = JsonConvert.SerializeObject(answer);

            var httpContent = new StringContent(answerJson, System.Text.Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PutAsync("api/Answer", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.DbContext.Answers, x => x.Text == "test2");
        }

        [Fact]
        public async Task Delete_ShouldDeleteExistingAnswer()
        {
            //Arrange
            var fixture = new Fixture();
            var answer = fixture.Build<Tests_CRUD_DAL.Entities.Answer>().Without(x => x.Question).Without(x => x.QuestionId).Create();
            var answerJson = JsonConvert.SerializeObject(answer);
            var httpContent = new StringContent(answerJson, System.Text.Encoding.UTF8, "application/json");

            await _fixture.Client.PostAsync("api/Answer", httpContent);

            //Act
            var response = await _fixture.Client.DeleteAsync("api/Answer/" + answer.Id);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.DoesNotContain(_fixture.DbContext.Answers, x => x.Id == answer.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnExistingAnswer()
        {
            //Arrange
            var fixture = new Fixture();
            var answer = fixture.Build<Tests_CRUD_DAL.Entities.Answer>().Without(x => x.Question).Without(x => x.QuestionId).Create();
            var answerJson = JsonConvert.SerializeObject(answer);
            var httpContent = new StringContent(answerJson, System.Text.Encoding.UTF8, "application/json");

            await _fixture.Client.PostAsync("api/Answer", httpContent);

            //Act
            var response = await _fixture.Client.GetAsync("api/Answer/" + answer.Id);
            response.EnsureSuccessStatusCode();

            var responseObject = JsonConvert.DeserializeObject<Tests_CRUD_DAL.Entities.Answer>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(answer.Id, responseObject.Id);
        }

        [Fact]
        public async Task GetById_AnswerDoesntExist_ShouldReturn_NotFound()
        {
            //Act
            var response = await _fixture.Client.GetAsync("api/Answer/" + Guid.NewGuid());

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}