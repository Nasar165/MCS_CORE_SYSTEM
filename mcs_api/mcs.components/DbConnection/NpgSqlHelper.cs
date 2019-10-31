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

        private NpgsqlCommand AddParametersToSqlCommand(NpgsqlCommand sqlCommand, object model)
        {
            foreach (var propertie in model.GetType().GetProperties())
            {
                var objectValue = propertie.GetValue(model);
                if (!Validation.ObjectIsNull(objectValue))
                    sqlCommand.Parameters.AddWithValue($"@{propertie.Name.ToLower()}", objectValue);
            }
            return sqlCommand;
        }

        public DataTable ReadDataFromDatabase(NpgsqlCommand sqlCommand)
        {
            NpgsqlDataReader reader = sqlCommand.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(reader);
            return dataTable;
        }

        public DataTable SelectQuery(string query, object data = null)
        {
            try
            {
                connection.Open();
                var sqlCommand = new NpgsqlCommand(query, connection);
                if (!Validation.ObjectIsNull(data))
                    AddParametersToSqlCommand(sqlCommand, data);
                return ReadDataFromDatabase(sqlCommand); ;
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