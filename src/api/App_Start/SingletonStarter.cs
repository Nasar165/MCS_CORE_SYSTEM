using api.Database;
using api.Database.Interface;
using api.Middleware;
using api.Models;
using api.Security;
using api.Security.Interface;
using Components.Database;
using Components.Database.Interface;
using Microsoft.Extensions.DependencyInjection;
using xAuth;
using xAuth.Interface;
using xEventLogger;
using xEventLogger.Interface;
using xFilewriter;
using xFilewriter.Interface;
using xheaderSecurity.Interface;
using xSql;
using xSql.Interface;

namespace api
{
    public class SingletonStarter
    {
        private static IServiceCollection Services { get; set; }

        private static void AddScoped()
        {
            Services.AddHttpContextAccessor();
            Services.AddScoped<IClaimHelper, ClaimHelper>();
            Services.AddScoped<IDatabaseHelper, DatabaseHelper>();

            var encryptionKey = AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings", "JWTKey");
            Services.AddScoped<IJwtGenerator, JwtGenerator>(ServiceProvider =>
           { return new JwtGenerator(encryptionKey, "HS256"); });

            var dbString = (AppConfigHelper.Instance.GetValueFromAppConfig("ConnectionStrings", "default"));
            Services.AddScoped<ISqlHelper, NpgSql>(ServiceProvider =>
            {
                return new NpgSql(dbString);
            });

            Services.AddScoped<TokenAuth>();
            Services.AddScoped<UserAuth>();
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