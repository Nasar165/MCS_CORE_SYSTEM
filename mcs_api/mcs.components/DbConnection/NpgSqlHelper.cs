using System.Data;
using mcs.components.Interface;
using Npgsql;

namespace mcs.components.DbConnection
{
    public class NpgSqlHelper : ISqlHelper
    {

        public NpgSqlHelper()
        {
            NpgsqlCommand command = new NpgsqlCommand();
        }

        public DataTable SelectQuery(string query)
        {
            throw new System.NotImplementedException();
        }

        public void InsertQuery<T>(string query, T data)
        {
            throw new System.NotImplementedException();
        }

        public object InsertQueryScalar<T>(string query, T data)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteData<T>(string query, T data)
        {
            throw new System.NotImplementedException();
        }




    }
}