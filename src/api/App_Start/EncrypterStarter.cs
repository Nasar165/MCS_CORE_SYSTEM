using api.Models;
using Components.Security;

namespace api
{
    public static class EncrypterStarter
    {
        public static string GetAesKey()
            => AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings", "AESKey");
        public static void SetupEncryption()
        {
            AesEncrypter._instance = new AesEncrypter(GetAesKey());
        }
    }
}