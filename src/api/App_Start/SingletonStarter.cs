using api.Database;
using api.Database.Interface;
using api.Middleware;
using api.Middleware.Interface;
using api.Models;
using api.Security;
using api.Security.Interface;
using Components;
using Components.Database;
using Components.Database.Interface;
using Components.Interface;
using Components.Logger;
using Components.Logger.Interface;
using Components.Security;
using Microsoft.Extensions.DependencyInjection;

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
            Services.AddSingleton<ILogger, EventLogger>(ServiceProvider =>
                { return new EventLogger(GetLoggingStyle()); });
            Services.AddSingleton<IQueryHelper, SqlQueryHelper>();
            Services.AddSingleton<IFileWriter, FileWriter>();
            Services.AddSingleton<IFileIntegrity, SHA256FileHash>();
            Services.AddSingleton<IHeaderPolicy, OWASPHeaderPolicy>();
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