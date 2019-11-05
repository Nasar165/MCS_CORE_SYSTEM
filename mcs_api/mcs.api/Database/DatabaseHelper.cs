using System;
using mcs.api.Models;
using mcs.api.Security.Interface;
using mcs.components;
using mcs.components.DbConnection;
using mcs.components.Errorhandler;
using mcs.components.Interface;

namespace mcs.api.Database
{
    public class DatabaseHelper
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
                var sql = GetMcsConnection();
                var dbId = claimHelper.GetValueFromClaim("Database_Id");
                var sqlCommand = CreateSqlCommand(CreateClientId(dbId), "");
                var dataTable = sql.SelectQuery($"Select * from database_list where database_id = @id", sqlCommand);
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