using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace api.test
{
    public class AuthenticationTest : IClassFixture<WebApplicationFactory<api.Startup>>
    {
        private readonly WebApplicationFactory<api.Startup> WebApp;
        public AuthenticationTest(WebApplicationFactory<api.Startup> webapp)
            => WebApp = webapp;

        private StringContent CreateJsonContent(string data)
            => new StringContent(data, Encoding.UTF8, "application/json");

        [Theory]
        [InlineData("/apiauth")]
        public async void APIAuthControllerTest(string url)
        {
            var client = WebApp.CreateClient();
            var response = await client.PostAsync(url,
            CreateJsonContent("{\"TokenKey\":\"#we321$$awe\", \"GroupKey\":\"12\"}"));
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/userauth")]
        public async void UserAuthControllerTest(string url)
        {
            var client = WebApp.CreateClient();
            var response = await client.PostAsync(url,
                CreateJsonContent("{\"Username\":\"nasar2\", \"Password\":\"nasar165\"}"));
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}