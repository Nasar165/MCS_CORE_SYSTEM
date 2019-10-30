using Microsoft.Extensions.Configuration;
using System;

namespace mcs.api.Models
{
    public class AppConfigHelper
    {
        private IConfiguration AppConfig { get; set; }
        private static Lazy<AppConfigHelper> _Instance = new Lazy<AppConfigHelper>();
        public static AppConfigHelper Instance => _Instance != null ? _Instance.Value : new Lazy<AppConfigHelper>().Value;
        public string GetSecreatKey()
            => AppConfig.GetSection("AppSettings").GetSection("SecretKey").Value;

        public string GetDbConnection()
            => AppConfig.GetSection("ConnectionStrings").GetSection("mcscon").Value;

        public void SetIConfiguration(IConfiguration config)
            => AppConfig = config;


    }
}