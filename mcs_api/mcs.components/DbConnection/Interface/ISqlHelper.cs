using System.Data;

namespace mcs.Components.DbConnection.Interface
{
    public interface ISqlHelper
    {
        DataTable SelectQuery<T>(string query, SqlCommandHelper<T> data);

        void InsertQuery<T>(string query, SqlCommandHelper<T> data);

        object InsertQueryScalar<T>(string query, SqlCommandHelper<T> data);

        void DeleteData<T>(string query, SqlCommandHelper<T> data);
    }
}