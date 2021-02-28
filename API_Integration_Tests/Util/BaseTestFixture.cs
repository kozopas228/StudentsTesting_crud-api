using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Tests_CRUD_API;
using Tests_CRUD_DAL;

namespace API_Integration_Tests.Util
{
    public class BaseTestFixture
    {
        public TestServer TestServer { get; }
        public ApplicationContext DbContext { get; }
        public HttpClient Client { get; }

        public BaseTestFixture()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);
            Client = TestServer.CreateClient();
            DbContext = TestServer.Host.Services.GetService<ApplicationContext>();

            FakeDbInitializer.Initialize(DbContext);

        }

        public void Dispose()
        {
            Client.Dispose();
            TestServer.Dispose();
        }
    }
}