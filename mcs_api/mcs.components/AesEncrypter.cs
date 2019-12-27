using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AesEncrypter : IEncrypter
{
    private byte[] IV = new byte[16];  
    protected string SymmetricKey {get;set;}
    public AesEncrypter(string symmetricKey)
        => SymmetricKey = symmetricKey;

    private Aes CreateAesInstance(){
        var aes =  Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(SymmetricKey);
        aes.IV = IV;
        aes.Padding = PaddingMode.PKCS7;
        aes.Mode = CipherMode.CBC;
        return aes;
    }    
    private ICryptoTransform createAesEncryptor(){
        var aes = CreateAesInstance();
        return aes.CreateEncryptor(aes.Key,aes.IV);    
    }

    private ICryptoTransform createAesDecryptor(){
        var aes =  CreateAesInstance();
        return aes.CreateDecryptor(aes.Key,aes.IV);    
    }

    private string EncryptData(string valueToEncrypt,MemoryStream memoryStream, CryptoStream cryptoStream){
        using (var streamWriter = new StreamWriter(cryptoStream))  
        {  
            streamWriter.Write(valueToEncrypt);  
        }  
        var array = memoryStream.ToArray();
        return Convert.ToBase64String(array);    
    }

    private string DecryptData(CryptoStream cryptoStream){
        using (var streamReader = new StreamReader(cryptoStream))  
        {  
            return streamReader.ReadToEnd();  
        } 
    }

    private string GenerateCryptoStream(string stringValue, MemoryStream memoryStream, ICryptoTransform encryptor, CryptoStreamMode streamMode){
        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, streamMode))  
        {  
            if(streamMode == CryptoStreamMode.Write)
                return EncryptData(stringValue, memoryStream, cryptoStream);
            else
                return DecryptData(cryptoStream);
        }  
    }

    public string EncryptData(string valueToEncrypt)
    {   
        try {
            var encryptor = createAesEncryptor();  
            var memoryStream = new MemoryStream();
            var result = GenerateCryptoStream(valueToEncrypt, memoryStream, 
                                                encryptor, CryptoStreamMode.Write);
            return result;  
        }
        catch (Exception)
        {
            throw;
        }
    }

    public string DecryptyData(string valueToDecrypt)
    {
        try {
            byte[] buffer = Convert.FromBase64String(valueToDecrypt);  
            var decryptor = createAesDecryptor();
            var memoryStream = new MemoryStream(buffer);
            var result = GenerateCryptoStream(null, memoryStream, decryptor, CryptoStreamMode.Read);
            return result; 
        } 
        catch(Exception){
            throw;    
        } 
    }
}