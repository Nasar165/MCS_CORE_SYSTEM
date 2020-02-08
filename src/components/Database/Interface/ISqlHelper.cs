using System.Data;

namespace Components.Database.Interface
{
    public interface ISqlHelper
    {
        DataTable SelectQuery<T>(string query, SqlCommandHelper<T> data);

        void AlterDataQuery<T>(string query, SqlCommandHelper<T> data);

        object AlterDataQueryScalar<T>(string query, SqlCommandHelper<T> data);
    }
}