using api.Database;
using api.Database.Interface;
using api.Middleware;
using api.Models;
using api.Security;
using api.Security.Interface;
using Components.Database;
using Components.Database.Interface;
using Microsoft.Extensions.DependencyInjection;
using xEventLogger;
using xEventLogger.Interface;
using xFilewriter;
using xFilewriter.Interface;
using xheaderSecurity.Interface;

namespace api
{
    public class SingletonStarter
    {
        private static IServiceCollection Services { get; set; }

        private static void AddScoped()
        {
            Services.AddHttpContextAccessor();
            Services.AddScoped<IJwtAuthenticator, JwtAuthenticator>();
            Services.AddScoped<IClaimHelper, ClaimHelper>();
            Services.AddScoped<IDatabaseHelper, DatabaseHelper>();
            Services.AddScoped<IAuthHelper, AuthHelper>();
        }

        private static bool GetLoggingStyle()
            => bool.Parse(AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings", "LogAsJson"));

        private static void AddSingleton()
        {
            Services.AddSingleton<IEventLogger, EventLogger>(ServiceProvider =>
                { return new EventLogger(new FileWriter()); });
            Services.AddSingleton<IQueryHelper, SqlQueryHelper>();
            Services.AddSingleton<IFileWriter, FileWriter>();
            Services.AddSingleton<IHeaderPolicy, OWASPPolicy>();
        }

        public static void RegisterSingleton(IServiceCollection services)
        {
            Services = services;
            Services.AddHttpContextAccessor();
            AddSingleton();
            AddScoped();
        }
    }
}