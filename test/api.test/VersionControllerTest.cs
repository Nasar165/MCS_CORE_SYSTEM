using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace api.test
{
    public class VersionControllerTest : IClassFixture<WebApplicationFactory<api.Startup>>
    {
        private readonly WebApplicationFactory<api.Startup> WebApp;
        public VersionControllerTest(WebApplicationFactory<api.Startup> webapp)
            => WebApp = webapp;

        [Theory]
        [InlineData("/version")]
        public async void TestVersionController(string url)
        {
            var client = WebApp.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
