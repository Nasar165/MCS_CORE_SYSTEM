using System;
using System.Data;
using Components.DbConnection.Interface;
using Npgsql;

namespace Components.DbConnection
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

        private bool DontSkipPropertie(string[] skipPropertie, string propertie)
        {
            foreach (var key in skipPropertie)
            {
                if (key.ToLower() == propertie.ToLower())
                {
                    return false;
                }
            }
            return true;
        }

        private NpgsqlCommand AddParametersToSqlCommand<T>(SqlCommandHelper<T> data)
        {
            var model = data.Data;
            if (!Validation.ObjectIsNull(model))
                foreach (var propertie in ReflectionHelper.GetPropertiesOfObject(model))
                {
                    if (DontSkipPropertie(data.SkipProperties, propertie.Name))
                    {
                        var objectValue = propertie.GetValue(model);
                        if (!Validation.ObjectIsNull(objectValue))
                            data.SqlCommand.Parameters.AddWithValue($"@{propertie.Name.ToLower()}", objectValue);
                    }
                }
            return data.SqlCommand;
        }

        public DataTable ReadDataFromDatabase(NpgsqlCommand sqlCommand)
        {
            var reader = sqlCommand.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(reader);
            return dataTable;
        }

        public DataTable SelectQuery<T>(string query, SqlCommandHelper<T> data)
        {
            try
            {
                connection.Open();
                var sqlCommand = new NpgsqlCommand(query, connection);
                if (!Validation.ObjectIsNull(data))
                {
                    data.SqlCommand = sqlCommand;
                    sqlCommand = AddParametersToSqlCommand(data);
                }
                return ReadDataFromDatabase(sqlCommand);
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

        public void AlterDataQuery<T>(string query, SqlCommandHelper<T> data)
        {
            try
            {
                connection.Open();
                var sqlCommand = new NpgsqlCommand(query, connection);
                if (!Validation.ObjectIsNull(data))
                {
                    data.SqlCommand = sqlCommand;
                    sqlCommand = AddParametersToSqlCommand(data);
                    sqlCommand.ExecuteNonQuery();
                }
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

        public object AlterDataQueryScalar<T>(string query, SqlCommandHelper<T> data)
        {
            try
            {
                connection.Open();
                var sqlCommand = new NpgsqlCommand(query, connection);
                object scalarData = null;
                if (!Validation.ObjectIsNull(data))
                {
                    data.SqlCommand = sqlCommand;
                    sqlCommand = AddParametersToSqlCommand(data);
                    scalarData = sqlCommand.ExecuteScalar();
                }
                return scalarData;
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