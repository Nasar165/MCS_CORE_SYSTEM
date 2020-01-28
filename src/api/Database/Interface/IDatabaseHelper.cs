using System.Collections.Generic;
using api.Security.AuthTemplate;
using Components.DbConnection;
using Components.DbConnection.Interface;

namespace api.Database.Interface
{
    public interface IDatabaseHelper
    {
        ISqlHelper GetDefaultConnection();
        SqlCommandHelper<T> CreateSqlCommand<T>(T data, params string[] ignore);
        string GetClientDatabase();
        IEnumerable<Roles> GetRolesFromUser(string key);
        IEnumerable<Roles> GetRolesFromToken(string key);
    }
}