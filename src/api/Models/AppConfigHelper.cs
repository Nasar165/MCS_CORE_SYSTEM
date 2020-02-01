using Components;
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

        public string GetSecreatKey()
            => GetValueFromAppConfig("AppSettings", "SecretKey");

        public string GetDefaultSQlConnection()
        {
            if (Validation.IsDocker())
                return GetValueFromAppConfig("ConnectionStrings", "docker");
            else
                return GetValueFromAppConfig("ConnectionStrings", "default");
        }

        public void SetIConfiguration(IConfiguration config)
            => AppConfig = config;
    }
}
