using System;
using System.Data;
using System.Reflection;
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

        private bool DontSkipPropertie(string[] skipPropertie, PropertyInfo propertie)
        {
            foreach (var key in skipPropertie)
            {
                if (key.ToLower() == propertie.Name.ToLower())
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
                    if (DontSkipPropertie(data.SkipProperties, propertie))
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
            NpgsqlDataReader reader = sqlCommand.ExecuteReader();
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

        public void InsertQuery<T>(string query, SqlCommandHelper<T> data)
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

        public object InsertQueryScalar<T>(string query, SqlCommandHelper<T> data)
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

        public void DeleteData<T>(string query, SqlCommandHelper<T> data)
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