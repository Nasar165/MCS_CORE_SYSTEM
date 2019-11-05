using mcs.api.Models;
using mcs.api.Security.Interface;
using mcs.components;
using mcs.components.DbConnection;

namespace mcs.api.Database
{
    public class ClientDatabaseHelper
    {
        IClaimHelper ClaimHelper { get; }
        public ClientDatabaseHelper(IClaimHelper claimHelper)
            => ClaimHelper = claimHelper;

        private object createDb(int databaseId)
            => new { Database_id = databaseId };

        private ClientDatabase GetDatabaseFromMcsCon(int id)
        {
            var dbid = new
            {
                Id = id
            };
            var sqlCommand = new SqlCommandHelper<object>(dbid, "");
            var mcsdbcon = AppConfigHelper.Instance.GetDbConnection();
            var sql = new NpgSqlHelper(mcsdbcon);
            var dataTable = sql.SelectQuery($"Select * from database_list where database_id = @id", sqlCommand);
            var clientDatabase = ObjectConverter.ConvertDataTableRowToObject<ClientDatabase>(dataTable.Rows[0]);
            return clientDatabase;
        }

        public void GetDatabase()
        {
            var id = ClaimHelper.GetValueFromClaim("Database_Id");
            var dbId = ObjectConverter.ConvertStringToInt(id);
            var dbData = GetDatabaseFromMcsCon(dbId);
            var databaseKey = createDb(dbId);
        }
    }
}