using api.Models;
using Components.Security;

namespace api
{
    public static class EncrypterStarter {

        public static string GetSymmetricKey()
            => AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings","SymmetricKey");
        public static void SetupEncryption(){
            AesEncrypter._instance = new AesEncrypter(GetSymmetricKey());
        }
        
    }
}