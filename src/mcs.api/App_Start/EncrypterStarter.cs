using mcs.api.Models;
using mcs.Components.Security;

public static class EncrypterStarter {

    public static string GetSymmetricKey()
        => AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings","SymmetricKey");
    public static void SetupEncryption(){
        AesEncrypter._instance = new AesEncrypter(GetSymmetricKey());
    }
    
}