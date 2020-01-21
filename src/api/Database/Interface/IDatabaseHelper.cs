using Components.DbConnection;
using Components.DbConnection.Interface;
using System.Security.Claims;

namespace api.Database.Interface
{
    public interface IDatabaseHelper
    {
        ISqlHelper GetDefaultConnection();
        SqlCommandHelper<T> CreateSqlCommand<T>(T data, params string[] ignore);
        string GetClientDatabase(ClaimsPrincipal User);
    }
}