public class AesEncrypter : IEncrypter
{
    //Implement AES Encryption
    protected string SymmetricKey {get;set;}
    public AesEncrypter()
    {
        
    }

    public string EncryptData(string valueToEncrypt)
    {
        throw new System.NotImplementedException();
    }

    public string DecryptyData<T>(string valueToDecrypt)
    {
        throw new System.NotImplementedException();
    }


}