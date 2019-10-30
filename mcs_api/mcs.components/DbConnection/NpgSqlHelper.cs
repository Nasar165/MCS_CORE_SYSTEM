using System;
using System.Data;
using mcs.components.Interface;
using Npgsql;

namespace mcs.components.DbConnection
{
    public class NpgSqlHelper : ISqlHelper
    {
        private NpgsqlConnection connection { get; set; }

        public NpgSqlHelper(string sqlConnectionString)
        {
            if (!Validation.StringIsNullOrEmpty(sqlConnectionString))
                CreateConnectionString(sqlConnectionString);
            else
                throw new NullReferenceException("Sql ConnectionString is Null");
        }

        private void CreateConnectionString(string sqlConnectionString)
           => connection = new NpgsqlConnection(sqlConnectionString);

        public DataTable SelectQuery(string query)
        {
            try
            {
                connection.Open();
                var sqlDataReader = new NpgsqlDataAdapter(query, connection);
                var table = new DataTable();
                sqlDataReader.Fill(table);
                return table;
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                connection.Close();
            }
        }

        public void InsertQuery<T>(string query, T data)
        {
            try
            {
                connection.Open();
                throw new System.NotImplementedException();
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                connection.Close();
            }
        }

        public object InsertQueryScalar<T>(string query, T data)
        {
            try
            {
                connection.Open();
                throw new System.NotImplementedException();
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                connection.Close();
                throw new System.NotImplementedException();
            }
        }

        public void DeleteData<T>(string query, T data)
        {
            try
            {
                connection.Open();
                throw new System.NotImplementedException();
            }
            catch (Exception error)
            {
                throw error;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}