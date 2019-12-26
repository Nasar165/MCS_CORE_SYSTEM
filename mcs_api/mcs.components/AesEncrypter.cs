using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AesEncrypter : IEncrypter
{
    protected string SymmetricKey {get;set;}
    public AesEncrypter(string symmetricKey)
        => SymmetricKey = symmetricKey;

    public string EncryptData(string valueToEncrypt)
    {
        byte[] iv = new byte[16];  
        byte[] array;  

        using (Aes aes = Aes.Create())  
        {  
            aes.Key = Encoding.UTF8.GetBytes(SymmetricKey);  
            aes.IV = iv;  

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);  

            using (MemoryStream memoryStream = new MemoryStream())  
            {  
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))  
                {  
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))  
                    {  
                        streamWriter.Write(valueToEncrypt);  
                    }  

                    array = memoryStream.ToArray();  
                }  
            }  
        }  
        return Convert.ToBase64String(array);  
    }

    public string DecryptyData(string valueToDecrypt)
    {
        byte[] iv = new byte[16];  
        byte[] buffer = Convert.FromBase64String(valueToDecrypt);  

        using (Aes aes = Aes.Create())  
        {  
            aes.Key = Encoding.UTF8.GetBytes(SymmetricKey);  
            aes.IV = iv;  
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);  

            using (MemoryStream memoryStream = new MemoryStream(buffer))  
            {  
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))  
                {  
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))  
                    {  
                        return streamReader.ReadToEnd();  
                    }  
                }  
            }  
        }
    }


}