using mcs.Components.DbConnection;
using mcs.Components.DbConnection.Interface;
using mcs.api.Security.Interface;

namespace mcs.api.Database.Interface
{
    public interface IDatabaseHelper
    {
        ISqlHelper GetMcsConnection();
        SqlCommandHelper<T> CreateSqlCommand<T>(T data, params string[] ignore);
        string GetClientDatabase(IClaimHelper claimHelper);
    }
}