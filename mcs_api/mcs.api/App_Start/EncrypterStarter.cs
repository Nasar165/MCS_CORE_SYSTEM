using mcs.api.Models;

public static class EncrypterStarter {

    public static string GetSymmetricKey()
        => AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings","SymmetricKey");
    public static void SetupEncryption(){
        AesEncrypter._instance = new AesEncrypter(GetSymmetricKey());
    }
    
}