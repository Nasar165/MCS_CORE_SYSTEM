public interface IEncrypter{
    string EncryptData(string valueToEncrypt);

    string DecryptyData<T>(string valueToDecrypt);
}