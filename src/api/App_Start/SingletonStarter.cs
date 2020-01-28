
using api.Database;
using api.Database.Interface;
using api.Security;
using api.Security.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace api
{
    public class SingletonStarter
    {
        public static void RegisterSingleton(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IJwtAuthenticator, JwtAuthenticator>();
            services.AddScoped<IClaimHelper, ClaimsHelper>();
            services.AddScoped<IDatabaseHelper, DatabaseHelper>();
            services.AddScoped<IAuthHelper, AuthHelper>();
        }
    }
}