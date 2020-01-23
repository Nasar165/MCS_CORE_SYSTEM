using System.Data;
using Components.Test.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Components.Test
{
    [TestCategory("GithubAction")]
    [TestClass]
    public class ObjectConverterTest
    {
        [TestMethod]
        public void ConvertDataTableToList()
        {
            var table = new DataTable();
            table.Columns.Add("Username");
            table.Columns.Add("Password");
            table.Rows.Add("Nasar", "Nasar123");
            var list = ObjectConverter.ConvertDataTableToList<UserAccount>(table);
            var user = list[0];
            Assert.AreEqual(user.Username, "Nasar");
        }

        [TestMethod]
        public void ConvertDataRowToObject()
        {
            var table = new DataTable();
            table.Columns.Add("Username");
            table.Columns.Add("Password");
            table.Rows.Add("Nasar", "Nasar123");
            var user = ObjectConverter.ConvertDataTableRowToObject<UserAccount>(table, 0);
            Assert.AreEqual(user.Username, "Nasar");
        }

        [TestMethod]
        public void ConvertStringToInt()
        {
            var stringValue = "1";
            var number = ObjectConverter.ConvertStringToInt(stringValue);
            Assert.IsTrue(number == 1);
        }
    }
}
