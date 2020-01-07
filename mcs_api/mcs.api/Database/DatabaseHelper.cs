using System;
using mcs.api.Models;
using mcs.api.Security.Interface;
using mcs.Components;
using mcs.Components.DbConnection;
using mcs.Components.Errorhandler;
using mcs.Components.Security;
using mcs.Components.DbConnection.Interface;
using mcs.api.Database.Interface;

namespace mcs.api.Database
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

        public string GetClientDatabase(IClaimHelper claimHelper)
        {
            try
            {
                var mcsSql = GetMcsConnection();
                var dbId = AesEncrypter._instance.DecryptyData(claimHelper.GetValueFromClaim("Database_Id"));
                var sqlCommand = CreateSqlCommand(CreateClientId(dbId), "");
                var dataTable = mcsSql.SelectQuery($"Select * from database_list where database_id = @id", sqlCommand);
                var clientDatabase = ObjectConverter.ConvertDataTableRowToObject<ClientDatabase>(dataTable.Rows[0]);
                return clientDatabase.GetConnectionString();
            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
                throw;
            }

        }
    }
}