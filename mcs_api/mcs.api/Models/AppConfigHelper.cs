using Microsoft.Extensions.Configuration;

namespace mcs.api.Models
{
    public class AppConfigHelper
    {
        private IConfiguration AppConfig { get; }

        public string GetSecreatKey()
            => AppConfig.GetSection("AppSettings").GetSection("SecretKey").Value;

        public void SetIConfiguration(IConfiguration config)
            => AppConfig = config;

        public static AppConfigHelper _AppConfig = new AppConfigHelper();
    }
}