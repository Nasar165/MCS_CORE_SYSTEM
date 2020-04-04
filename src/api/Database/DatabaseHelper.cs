using api.Models;
using Components;
using Components.Database.Interface;
using api.Security.AuthTemplate;
using System.Collections.Generic;
using api.Security.Interface;
using xEventLogger.Interface;
using xSql.Interface;
using xSql;
using api.Database.Interface;

namespace api.Database
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private string DefaultConnection { get; }
        private IClaimHelper ClaimHelper { get; }
        private IEventLogger Logger { get; }
        private IQueryHelper QueryHelper { get; }

        public DatabaseHelper(IClaimHelper claimHelper, IEventLogger logger, IQueryHelper queryHelper)
        {
            ClaimHelper = claimHelper;
            Logger = logger;
            DefaultConnection = AppConfigHelper.Instance.GetDefaultSQlConnection();
            QueryHelper = queryHelper;
        }

        public ISqlHelper GetDefaultConnection()
            => new NpgSql(DefaultConnection);

        private object CreateClientId(string id)
            => new { Id = ObjectConverter.ConvertStringToInt(id) };

        private IList<T> FetchDataFromDB<T>(string key, string querry)
        {
            var sql = GetDefaultConnection();
            var id = CreateClientId(key);
            var dataTable = sql.SelectQuery(
                            querry, id);
            return ObjectConverter.ConvertDataTableToList<T>(dataTable);
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
    }
}
