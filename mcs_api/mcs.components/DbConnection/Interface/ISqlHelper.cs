using System.Data;

namespace mcs.components.Interface
{
    public interface ISqlHelper
    {
        DataTable SelectQuery(string query, object data = null);

        void InsertQuery<T>(string query, T data);

        object InsertQueryScalar<T>(string query, T data);

        void DeleteData<T>(string query, T data);
    }
}