using System.Data;

namespace Components.DbConnection.Interface
{
    public interface ISqlHelper
    {
        DataTable SelectQuery<T>(string query, SqlCommandHelper<T> data);

        void AlterDataQuery<T>(string query, SqlCommandHelper<T> data);

        object AlterDataQueryScalar<T>(string query, SqlCommandHelper<T> data);
    }
}