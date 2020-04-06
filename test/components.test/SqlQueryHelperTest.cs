using System.IO;
using Components.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Components.Test
{
    [TestCategory("GithubAction")]
    [TestClass]
    public class SqlQueryHelperTest
    {
        [TestMethod]
        public void GetQuery()
        {
            var writer = new xFilewriter.FileWriter();
            var direcoryPath = $"{Directory.GetCurrentDirectory()}/scripts";
            writer.EnsureThatFilePathExists(direcoryPath, "sqlqueries.json");
            writer.AppendTextToFile("[{\"procedure\": \"apiauth\", \"query\": \"select * from token where tokenkey = @tokenkey\" }]", direcoryPath + "/sqlqueries.json", FileMode.Truncate);
            var SqlQueryHelp = new SqlQueryHelper();
            var query = SqlQueryHelp.GetSqlQuery("apiauth");
            Assert.AreEqual("select * from token where tokenkey = @tokenkey", query);
        }
    }
}