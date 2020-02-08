using System.IO;
using Components.Interface;
using Components.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Components.Test
{
    [TestCategory("GithubAction")]
    [TestClass]
    public class Sha256FileHashtest
    {
        private IFileWriter FileWriter = new FileWriter();
        IFileIntegrity FileIntegrity = new SHA256FileHash(new FileWriter());

        [TestMethod]
        public void AddFileHashToList()
        {
            FileIntegrity.AddFileHashToIntegrityStore("sqltest", "/integrityTestFile.txt");
            FileIntegrity.AddFileHashToIntegrityStore("sqlquery", "scripts/sqlqueries.json");
            Assert.IsTrue(FileIntegrity.FileIntegrityIsIntact("sqltest"));
        }

        [TestMethod]
        public void RemoveFileFromStore()
        {
            FileIntegrity.DeleteFileIntegrityFromStore("sqltest");
            Assert.IsFalse(FileIntegrity.FileIntegrityIsIntact("sqlTest"));
        }
    }
}