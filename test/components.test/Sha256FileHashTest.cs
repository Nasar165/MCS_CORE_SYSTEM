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
            var dir = Directory.GetCurrentDirectory() + "/scripts";
            FileWriter.CreateDirectoryPath(dir);
            var file = dir + "/sqlqueries.json";
            FileWriter.CreateFile(file);
            FileWriter.AppendTextToFile("[{\"procedure\": \"apiauth\", \"query\": \"select * from token where tokenkey = @tokenkey\" }]"
                            , file, FileMode.Truncate);
            FileIntegrity.AddFileHashToIntegrityStore("sqltest", "scripts/sqlqueries.json");
            FileIntegrity.AddFileHashToIntegrityStore("sqlquery", "scripts/sqlqueries.json");
            Assert.IsTrue(FileIntegrity.FileIntegrityIsIntact("sqlquery"));
        }

        [TestMethod]
        public void RemoveFileFromStore()
        {
            FileIntegrity.DeleteFileIntegrityFromStore("sqltest");
            Assert.IsFalse(FileIntegrity.FileIntegrityIsIntact("sqlTest"));
        }
    }
}