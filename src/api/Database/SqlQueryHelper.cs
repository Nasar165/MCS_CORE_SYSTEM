using System.Collections.Generic;
using System.IO;
using System.Linq;
using api.Database.Interface;
using Components;
using Newtonsoft.Json;

namespace api.Database
{
    public class SqlQueryHelper : IQueryHelper
    {
        private readonly string QueryFile = 
            $"{Directory.GetCurrentDirectory()}/scripts/sqlqueries.json";
        private IReadOnlyCollection<SqlProcedures> SqlProcedureList { get; set; }
        public SqlQueryHelper()
            => GetProcedureList();

        private void GetProcedureList()
        {
            var fileWriter = new FileWriter();
            var JsonData = fileWriter.ReadTextFromFile(QueryFile);
            SqlProcedureList = JsonConvert.DeserializeObject<List<SqlProcedures>>(JsonData);
        }

        public string GetSqlQuery(string procedureName)
        {
            var procedure = SqlProcedureList.FirstOrDefault(
                x  =>  x.procedure == procedureName);

            if(Validation.ObjectIsNull(procedure))
                throw new System.Exception($"{procedureName} could not be found");
            return procedure.query;
        }
    }
}