using mcs.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mcs.components.test
{
    [TestClass]
    public class AesEncryptionTest
    {
        public AesEncryptionTest()
        {
            AesEncrypter._instance = new AesEncrypter("b14ca5898a4e4133bbce2ea2315a1916");       
        }
        
        [TestMethod]
        public void EncryptData()
        {
           var unEncryptedText = "Nasar is the greatest";
           var encryptedText = AesEncrypter._instance.EncryptData(unEncryptedText);
           Assert.AreNotEqual(unEncryptedText,encryptedText);
        }

        [TestMethod]
        public void DecryptData()
        {
            var unEncryptedText = "Nasar is the greatest";
            var encryptedText = AesEncrypter._instance.EncryptData(unEncryptedText);
            var decryptedText = AesEncrypter._instance.DecryptyData(encryptedText);
            Assert.AreEqual(unEncryptedText,decryptedText);
        }
    }
}
