using System;
using api.Models;
using Components;
using Components.Database;
using Components.Security;
using Components.Database.Interface;
using api.Database.Interface;
using api.Security.AuthTemplate;
using System.Collections.Generic;
using api.Security.Interface;
using Components.Logger.Interface;

namespace api.Database
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private string DefaultConnection { get; }
        private IClaimHelper ClaimHelper { get; }
        private ILogger Logger { get; }
        private IQueryHelper QueryHelper { get; }

        public DatabaseHelper(IClaimHelper claimHelper, ILogger logger, IQueryHelper queryHelper)
        {
            ClaimHelper = claimHelper;
            Logger = logger;
            DefaultConnection = AppConfigHelper.Instance.GetDefaultSQlConnection();
            QueryHelper = queryHelper;
        }

        public ISqlHelper GetDefaultConnection()
            => new NpgSqlHelper(DefaultConnection);

        private object CreateClientId(string id)
            => new { Id = ObjectConverter.ConvertStringToInt(id) };

        public SqlCommandHelper<T> CreateSqlCommand<T>(T data, params string[] ignore)
            => new SqlCommandHelper<T>(data, ignore);

        private IList<T> FetchDataFromDB<T>(string key, string querry)
        {
            var sql = GetDefaultConnection();
            var id = CreateClientId(key);
            var sqlCommand = CreateSqlCommand<object>(id, "");
            var dataTable = sql.SelectQuery(
                            querry, sqlCommand);
            return ObjectConverter.ConvertDataTableToList<T>(dataTable);
        }

        public DataExtension GetUserFromDatabase(string key)
            => FetchDataFromDB<DataExtension>(key
                                , QueryHelper.GetSqlQuery("getuseraccount"))[0];

        public DataExtension GetTokenFromDatabase(string key)
            => FetchDataFromDB<DataExtension>(key
                                , QueryHelper.GetSqlQuery("gettoken"))[0];

        private ClientDatabase FetchClientDataBase(string dbKey)
            => FetchDataFromDB<ClientDatabase>(dbKey.ToString()
                                , QueryHelper.GetSqlQuery("getdatabase"))[0];

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
            var roles = FetchDataFromDB<Roles>(key,
                QueryHelper.GetSqlQuery("getuserroles"));
            return roles;
        }

        public IEnumerable<Roles> GetRolesFromToken(string key)
        {
            var roles = FetchDataFromDB<Roles>(key,
                QueryHelper.GetSqlQuery("gettokenroles"));
            return roles;
        }

        private string GetConnectionString(string audiance, string key)
        {
            var connectionString = "";
            if (Validation.StringsAreEqual(audiance, "User"))
                connectionString = FetchUserDb(key).GetConnectionString();
            else
                connectionString = FetchTokenDb(key).GetConnectionString();
            return connectionString;
        }

        public string GetClientDatabase()
        {
            try
            {
                var key = AesEncrypter._instance.DecryptyData(
                    ClaimHelper.GetValueFromClaim("key"));
                var audiance = ClaimHelper.GetValueFromClaim("aud");
                var connectionString = GetConnectionString(audiance, key);
                return connectionString;
            }
            catch (Exception error)
            {
                Logger.LogEventAsync(error);
                throw;
            }
        }
    }
}
