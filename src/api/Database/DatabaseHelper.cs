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
using System.Collections.Generic;
using System.Data;

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

        private IList<T> FetchDataFromDB<T>(string key, string querry)
        {
            var sql = GetMcsConnection();
            var id = CreateClientId(key);
            var sqlCommand = CreateSqlCommand<object>(id,"");
            var dataTable = sql.SelectQuery(
                            querry, sqlCommand);
            return ObjectConverter.ConvertDataTableToList<T>(dataTable);
        }

        public DataExtension GetUserFromDatabase(string key)
            => FetchDataFromDB<DataExtension>(key
                                , "Select * from useraccount where useraccount_id = @id")[0];

        public DataExtension GetTokenFromDatabase(string key)
            => FetchDataFromDB<DataExtension>(key
                                , "Select * from token where database_id = @id")[0];

        private ClientDatabase FetchClientDataBase(string dbKey)
            => FetchDataFromDB<ClientDatabase>(dbKey.ToString()
                                , "Select * from database_list where database_id = @id")[0];
        
        private ClientDatabase FetchUserDb(string key)
        {
            var user = GetUserFromDatabase(key);
            var database = FetchClientDataBase(user.Database_Id.ToString());
            return database;
        }

        private ClientDatabase FetchTokenDb(string key)
        {
            var token = GetTokenFromDatabase(key);
            var database = FetchClientDataBase(token.Database_Id.ToString());
            return database;
        }

         public IEnumerable<Roles> GetRolesFromUser(string key)
        {
            var roles = FetchDataFromDB<Roles>(key, $"select name from roles inner join roles_useraccount on roles_useraccount.role_id = roles.role_id Where roles_useraccount.useraccount_id = @id");
            return roles;
        }

        public IEnumerable<Roles> GetRolesFromToken(string key)
        {
            var roles = FetchDataFromDB<Roles>(key, $"select name from roles inner join roles_token on roles_token.role_id = roles.role_id Where roles_token.tokenkey_id = @id");
            return roles;
        }

        // Check if I can set the claimsprinciple during every request.
        public string GetClientDatabase(ClaimsPrincipal User)
        {           
            try
            {
                var claimHelper = new ClaimsHelper(User.Claims);
                var audiance = claimHelper.GetValueFromClaim("aud");
                var connectionString  = "";
                var key = AesEncrypter._instance.DecryptyData(
                    claimHelper.GetValueFromClaim("key"));
                if(Validation.StringsAreEqual(audiance,"User"))
                    connectionString = FetchUserDb(key).GetConnectionString();
                else
                    connectionString = FetchTokenDb(key).GetConnectionString();

                return connectionString;
            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
                throw;
            }
        }
    }
}