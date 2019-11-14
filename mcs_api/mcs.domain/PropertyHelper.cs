using System;
using mcs.Components.DbConnection;
using mcs.Components.DbConnection.Interface;
using mcs.Components.Errorhandler;
using mcs.domain.Templates;

namespace mcs.domain
{
    public class PropertyHelper
    {
        private ISqlHelper Sql { get; }
        public PropertyHelper(string sqlConnection)
            => Sql = new NpgSqlHelper(sqlConnection);

        private int CreateProperty(Property newProperty)
        {
            var SqlCommand = new SqlCommandHelper<Property>(newProperty);
            var refId = (int)Sql.InsertQueryScalar<Property>("", SqlCommand);
            return refId;
        }

        public void CreatUnit(Unit newUnit)
        {
            try
            {
                newUnit.Ref_Id = CreateProperty(newUnit);
                var SqlCommand = new SqlCommandHelper<Unit>(newUnit);
                Sql.InsertQuery("", SqlCommand);
            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
            }
        }

        public void CreateProject(Project newProject)
        {
            try
            {
                newProject.Ref_Id = CreateProperty(newProject);
                var SqlCommand = new SqlCommandHelper<Project>(newProject);
                Sql.InsertQuery("", SqlCommand);
            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
            }
        }
    }
}