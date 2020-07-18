using Microsoft.Extensions.Configuration;
using System;

namespace api.Models
{
    public class AppConfigHelper
    {
        private IConfiguration AppConfig { get; set; }
        private static Lazy<AppConfigHelper> _Instance = new Lazy<AppConfigHelper>();
        public static AppConfigHelper Instance => _Instance != null ? _Instance.Value : new Lazy<AppConfigHelper>().Value;

        public string GetValueFromAppConfig(string section, string name)
            => AppConfig.GetSection(section).GetSection(name).Value;

        public string GetSecretKey()
            => GetValueFromAppConfig("AppSettings", "JWTkey");

        private bool IsDocker()
            => bool.Parse(GetValueFromAppConfig("AppSettings", "Docker"));

        public string GetDefaultSQlConnection()
        {
            if (IsDocker())
                return GetValueFromAppConfig("ConnectionStrings", "docker");
            else
                return GetValueFromAppConfig("ConnectionStrings", "default");
        }

        public void SetIConfiguration(IConfiguration config)
            => AppConfig = config;
    }
}
