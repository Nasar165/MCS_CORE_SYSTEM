using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace api
{
    public class DatabaseStarter
    {
        public static void SetHttpContext(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            Database.DatabaseHelper.Instance.SetHttpContextAccessor(httpContextAccessor);
        }
    }

}