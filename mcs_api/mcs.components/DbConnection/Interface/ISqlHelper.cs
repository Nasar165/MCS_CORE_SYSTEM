using System.Data;
using mcs.components.DbConnection;

namespace mcs.components.Interface
{
    public interface ISqlHelper
    {
        DataTable SelectQuery<T>(string query, SqlCommandHelper<T> data);

        void InsertQuery<T>(string query, SqlCommandHelper<T> data);

        object InsertQueryScalar<T>(string query, SqlCommandHelper<T> data);

        void DeleteData<T>(string query, SqlCommandHelper<T> data);
    }
}