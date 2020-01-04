using System;
using mcs.Components;
using mcs.Components.DbConnection;
using mcs.Components.DbConnection.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mcs.components.test
{
    public delegate void DbAction();

    [TestClass]
    public class DbConnectiontest
    {
        private ISqlHelper Connecter { get; } = new NpgSqlHelper("Server=127.0.0.1;Database=mcsunity;Uid=mcsuser;Pwd=Nasar165;");
        private int Id { get; set; }

        private authactivity CreateAauthUser()
        {
            var authActivity = new authactivity(){
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
            Id = (int)Connecter.InsertQueryScalar(query, SqlCommand);
            Assert.AreNotEqual(Id, 0);
        }

        [TestMethod]
        public void SelectDataFromDatabase()
        {
            var authActivity = CreateAauthUser();
            authActivity.Authactivity_Id = Id;
            string [] skipProperties = { "Username", "Date" };
            var SqlCommand = new SqlCommandHelper<authactivity>(authActivity, skipProperties);
            var query = "select * from authactivity where authactivity_id = @authactivity_id";
            var data = Connecter.SelectQuery(query, SqlCommand);
            var authUser = ObjectConverter.ConvertDataTableRowToObject<authactivity>(data.Rows[0]);
            Assert.AreEqual("NasarTest",authUser.Username); 
        }

        [TestMethod]
        public void DeleteDataFromDatabase()
        {
            var authActivity = CreateAauthUser();
            authActivity.Authactivity_Id = Id;
            string [] skipProperties = { "Username", "Date" };
            var SqlCommand = new SqlCommandHelper<authactivity>(authActivity, skipProperties);
            var query = "Deletet authactivity where authactivity_id = @authactivity_id";
            Connecter.DeleteData(query, SqlCommand);
        }
    }

    public class authactivity 
    {
        public int Authactivity_Id { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
    }
}