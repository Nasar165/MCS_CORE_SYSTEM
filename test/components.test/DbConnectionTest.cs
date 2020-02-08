using System;
using components.test.Models;
using Components.Database;
using Components.Database.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Components.Test
{
    [TestCategory("Local")]
    [TestClass]
    public class DbConnectiontest
    {
        private readonly ISqlHelper Connecter =
             new NpgSqlHelper("Server=127.0.0.1;port=5432;Database=defaultdatabase;Uid=defaultuser;Pwd=SG,npuLc2?;");
        private int Id { get; set; }

        private authactivity CreateAauthUser()
        {
            var authActivity = new authactivity()
            {
                Username = "NasarTest",
                Date = DateTime.Now
            };
            return authActivity;
        }

        [TestMethod]
        public void InsertDataToDatabase()
        {
            var authActivity = CreateAauthUser();
            var SqlCommand = new SqlCommandHelper<authactivity>(authActivity, "Authactivity_Id");
            var query = "Insert into authactivity (username, date) Values(@username, @date) RETURNING authactivity_id";
            Id = (int)Connecter.AlterDataQueryScalar(query, SqlCommand);
            Assert.AreNotEqual(Id, 0);
        }

        [TestMethod]
        public void SelectDataFromDatabase()
        {
            var authActivity = CreateAauthUser();
            authActivity.Authactivity_Id = Id;
            string[] skipProperties = { "Username", "Date", "" };
            var SqlCommand = new SqlCommandHelper<authactivity>(authActivity, skipProperties);
            var query = "select * from authactivity order by authactivity_id desc";
            var data = Connecter.SelectQuery(query, SqlCommand);
            var user = ObjectConverter.ConvertDataTableRowToObject<authactivity>(data, 0);
            Assert.AreEqual("NasarTest", user.Username);
        }

        [TestMethod]
        public void DeleteDataFromDatabase()
        {
            var authActivity = CreateAauthUser();
            authActivity.Authactivity_Id = Id;
            var SqlCommand = new SqlCommandHelper<authactivity>(authActivity, "");
            var deleteQuery = "Delete from authactivity where username = 'NasarTest'";
            Connecter.AlterDataQuery(deleteQuery, SqlCommand);
            var selectQuery = "select * from authactivity where username = 'NasarTest'";
            var data = Connecter.SelectQuery(selectQuery, SqlCommand);
            Assert.AreEqual(0, data.Rows.Count);
        }
    }
}