using Components.Database;
using Components.Database.Interface;
using Components.Security;
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
            IQueryHelper SqlQueryHelp = new SqlQueryHelper(new SHA256FileHash(new FileWriter()));
            var query = SqlQueryHelp.GetSqlQuery("apiauth");
            Assert.AreEqual("select * from token where tokenkey = @tokenkey", query);
        }
    }
}