using System.Collections.Generic;
using System.IO;
using System.Linq;
using Components.Database.Interface;
using Components.Interface;
using Newtonsoft.Json;

namespace Components.Database
{
    public class SqlQueryHelper : IQueryHelper
    {
        private readonly string QueryFile =
            $"{Directory.GetCurrentDirectory()}/scripts/sqlqueries.json";
        private IReadOnlyCollection<SqlProcedures> SqlProcedureList { get; set; }
        public SqlQueryHelper(IFileIntegrity fileIntegrity)
        {
            QueryFileIntergrityIsIntact(fileIntegrity);
            GetProcedureList();
        }

        private void QueryFileIntergrityIsIntact(IFileIntegrity fileIntegrity)
        {
            if (!fileIntegrity.FileIntegrityIsIntact("sqlquery"))
                throw new System.Exception("SQL Script File file integrity has failed");
        }

        private void GetProcedureList()
        {
            var fileWriter = new FileWriter();
            var JsonData = fileWriter.ReadTextFromFile(QueryFile);
            SqlProcedureList = JsonConvert.DeserializeObject<List<SqlProcedures>>(JsonData);
        }

        public string GetSqlQuery(string procedureName)
        {
            var procedure = SqlProcedureList.FirstOrDefault(
                x => x.procedure == procedureName);

            if (Validation.ObjectIsNull(procedure))
                throw new System.Exception($"{procedureName} could not be found");
            return procedure.query;
        }
    }
}