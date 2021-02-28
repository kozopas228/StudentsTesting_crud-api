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
    public class TestControllerTests
    {
        private BaseTestFixture _fixture;

        public TestControllerTests()
        {
            _fixture = new BaseTestFixture();
        }

        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            // Act
            var response = await _fixture.Client.GetAsync("api/Test");
            response.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<IEnumerable<Tests_CRUD_BLL.Models.Test>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task Post_ShouldCreateNewTest()
        {
            //Arrange
            var fixture = new Fixture();
            var fakeTest = fixture
                .Build<Tests_CRUD_BLL.Models.Test>()
                .With(x => x.Description, "test1")
                .Create();
            var fakeTestJson = JsonConvert.SerializeObject(fakeTest);
            var httpContent = new StringContent(fakeTestJson, System.Text.Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PostAsync("api/Test", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.DbContext.Tests, test => test.Description == "test1");

        }

        [Fact]
        public async Task Put_ShouldUpdateExistingTest()
        {
            //Arrange
            var test = _fixture.DbContext.Tests.Last();
            test.Description = "test2";
            var testJson = JsonConvert.SerializeObject(test);

            var httpContent = new StringContent(testJson, System.Text.Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PutAsync("api/Test", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.DbContext.Tests, x => x.Description == "test2");
        }

        [Fact]
        public async Task Delete_ShouldDeleteExistingTest()
        {
            //Arrange
            var fixture = new Fixture();
            var test = fixture.Build<Tests_CRUD_DAL.Entities.Test>()
                .Without(x=>x.Questions)
                .Without(x=>x.TestTheme)
                .Without(x=>x.TestThemeId)
                .Create();
            var testJson = JsonConvert.SerializeObject(test);
            var httpContent = new StringContent(testJson, System.Text.Encoding.UTF8, "application/json");

            await _fixture.Client.PostAsync("api/Test", httpContent);

            //Act
            var response = await _fixture.Client.DeleteAsync("api/Test/" + test.Id);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.DoesNotContain(_fixture.DbContext.Tests, x => x.Id == test.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnExistingTest()
        {
            //Arrange
            var fixture = new Fixture();
            var test = fixture.Build<Tests_CRUD_DAL.Entities.Test>()
                .Without(x => x.Questions)
                .Without(x => x.TestTheme)
                .Without(x => x.TestThemeId)
                .Create();
            var testJson = JsonConvert.SerializeObject(test);
            var httpContent = new StringContent(testJson, System.Text.Encoding.UTF8, "application/json");

            await _fixture.Client.PostAsync("api/Test", httpContent);

            //Act
            var response = await _fixture.Client.GetAsync("api/Test/" + test.Id);
            response.EnsureSuccessStatusCode();

            var responseObject = JsonConvert.DeserializeObject<Tests_CRUD_DAL.Entities.Test>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(test.Id, responseObject.Id);
        }

        [Fact]
        public async Task GetById_TestDoesntExist_ShouldReturn_NotFound()
        {
            //Act
            var response = await _fixture.Client.GetAsync("api/Test/" + Guid.NewGuid());

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}