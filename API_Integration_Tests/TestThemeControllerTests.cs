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
    public class TestThemeControllerTests
    {
        private BaseTestFixture _fixture;

        public TestThemeControllerTests()
        {
            _fixture = new BaseTestFixture();
        }

        [Fact]
        public async Task Get_ShouldReturnListResult()
        {
            // Act
            var response = await _fixture.Client.GetAsync("api/TestTheme");
            response.EnsureSuccessStatusCode();
            var models = JsonConvert.DeserializeObject<IEnumerable<Tests_CRUD_BLL.Models.TestTheme>>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.NotEmpty(models);
        }

        [Fact]
        public async Task Post_ShouldCreateNewTestTheme()
        {
            //Arrange
            var fixture = new Fixture();
            var fakeTestTheme = fixture
                .Build<Tests_CRUD_BLL.Models.TestTheme>()
                .With(x => x.Name, "test1")
                .Create();
            var fakeTestThemeJson = JsonConvert.SerializeObject(fakeTestTheme);
            var httpContent = new StringContent(fakeTestThemeJson, System.Text.Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PostAsync("api/TestTheme", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.DbContext.TestThemes, testTheme => testTheme.Name == "test1");

        }

        [Fact]
        public async Task Put_ShouldUpdateExistingTestTheme()
        {
            //Arrange
            var testTheme = _fixture.DbContext.TestThemes.Last();
            testTheme.Name = "test2";
            var testThemeJson = JsonConvert.SerializeObject(testTheme);

            var httpContent = new StringContent(testThemeJson, System.Text.Encoding.UTF8, "application/json");

            //Act
            var response = await _fixture.Client.PutAsync("api/TestTheme", httpContent);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.Contains(_fixture.DbContext.TestThemes, x => x.Name == "test2");
        }

        [Fact]
        public async Task Delete_ShouldDeleteExistingTestTheme()
        {
            //Arrange
            var fixture = new Fixture();

            var testTheme = fixture.Build<Tests_CRUD_DAL.Entities.TestTheme>()
                .Without(x => x.Tests)
                .Create();

            var testThemeJson = JsonConvert.SerializeObject(testTheme);
            var httpContent = new StringContent(testThemeJson, System.Text.Encoding.UTF8, "application/json");

            await _fixture.Client.PostAsync("api/TestTheme", httpContent);

            //Act
            var response = await _fixture.Client.DeleteAsync("api/TestTheme/" + testTheme.Id);
            response.EnsureSuccessStatusCode();

            // Assert
            Assert.DoesNotContain(_fixture.DbContext.TestThemes, x => x.Id == testTheme.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnExistingTestTheme()
        {
            //Arrange
            var fixture = new Fixture();
            var testTheme = fixture.Build<Tests_CRUD_DAL.Entities.TestTheme>()
                .Without(x => x.Tests)
                .Create();
            var testThemeJson = JsonConvert.SerializeObject(testTheme);
            var httpContent = new StringContent(testThemeJson, System.Text.Encoding.UTF8, "application/json");

            await _fixture.Client.PostAsync("api/TestTheme", httpContent);

            //Act
            var response = await _fixture.Client.GetAsync("api/TestTheme/" + testTheme.Id);
            response.EnsureSuccessStatusCode();

            var responseObject = JsonConvert.DeserializeObject<Tests_CRUD_DAL.Entities.TestTheme>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(testTheme.Id, responseObject.Id);
        }

        [Fact]
        public async Task GetById_TestThemeDoesntExist_ShouldReturn_NotFound()
        {
            //Act
            var response = await _fixture.Client.GetAsync("api/TestTheme/" + Guid.NewGuid());

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}