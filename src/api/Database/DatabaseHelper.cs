using System;
using api.Models;
using Components;
using Components.DbConnection;
using Components.Errorhandler;
using Components.Security;
using Components.DbConnection.Interface;
using api.Database.Interface;
using System.Security.Claims;
using api.Security;
using api.Security.AuthTemplate;

namespace api.Database
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private string McsCon { get; }
        public static DatabaseHelper Instance = new DatabaseHelper();

        public DatabaseHelper()
            => McsCon = AppConfigHelper.Instance.GetDbConnection();

        public ISqlHelper GetMcsConnection()
            => new NpgSqlHelper(McsCon);

        private object CreateClientId(string id)
        {
            return new
            {
                Id = ObjectConverter.ConvertStringToInt(id)
            };
        }
        public SqlCommandHelper<T> CreateSqlCommand<T>(T data, params string[] ignore)
        {
            return new SqlCommandHelper<T>(data, ignore);
        }

        private int GetUserAccount(string userId)
        {
            var sql = GetMcsConnection();
            var id = CreateClientId(userId);
            var sqlCommand = new SqlCommandHelper<object>(id,"");
            var dataTable = sql.SelectQuery(
                            "Select * from useraccount where useraccount_id = @id"
                                , sqlCommand);
            var user = ObjectConverter.ConvertDataTableRowToObject<DataExtension>(dataTable.Rows[0]);
            return user.Database_Id;
        }

        private ClientDatabase GetClientDatabase(string databaseKey)
        {
            var sql = GetMcsConnection();
            var id = CreateClientId(databaseKey);
            var sqlCommand = new SqlCommandHelper<object>(id,"");
            var dataTable = sql.SelectQuery(
                            "Select * from database_list where database_id = @id"
                                , sqlCommand);
            var clientDataBase = ObjectConverter.ConvertDataTableRowToObject<ClientDatabase>(dataTable.Rows[0]);
            return clientDataBase;
        }

        // change the behaviour of the method.
        // Check if I can set the claimsprinciple during every request.
        public string GetClientDatabase(ClaimsPrincipal User)
        {           
            try
            {
                var claimHelper = new ClaimsHelper(User.Claims);
                var audiance = claimHelper.GetValueFromClaim("aud");
                var dbId = 0;
                var key = AesEncrypter._instance.DecryptyData(
                    claimHelper.GetValueFromClaim("Key"));
                if(Validation.StringsAreEqual(audiance,"User"))
                    dbId = GetUserAccount(key);
                else
                    dbId = 0;

                var database = GetClientDatabase(dbId.ToString());
                return database.GetConnectionString();
            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
                throw;
            }
        }
    }
}