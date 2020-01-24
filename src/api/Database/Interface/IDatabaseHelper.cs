using Components.DbConnection;
using Components.DbConnection.Interface;
using Microsoft.AspNetCore.Http;

namespace api.Database.Interface
{
    public interface IDatabaseHelper
    {
        void SetHttpContextAccessor(IHttpContextAccessor contextAccessor);
        ISqlHelper GetDefaultConnection();
        SqlCommandHelper<T> CreateSqlCommand<T>(T data, params string[] ignore);
        string GetClientDatabase();
    }
}