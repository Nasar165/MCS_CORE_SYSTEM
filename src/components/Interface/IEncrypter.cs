namespace Components.Interface{
    public interface IEncrypter{
        string EncryptData(string valueToEncrypt);

        string DecryptyData(string valueToDecrypt);
    }
}
