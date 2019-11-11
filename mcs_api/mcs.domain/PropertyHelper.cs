using System.Collections.Generic;
using mcs.Components;
using mcs.Components.DbConnection;
using mcs.Components.DbConnection.Interface;
using mcs.domain.Templates;

namespace mcs.domain
{
    public class PropertyHelper
    {
        private ISqlHelper Sql { get; }
        public PropertyHelper(string sqlConnection)
            => Sql = new NpgSqlHelper(sqlConnection);

        public IReadOnlyCollection<Unit> GetCondos(bool website = true)
        {
            var data = Sql.SelectQuery<Unit>("Select * from unit", null);
            var list = ObjectConverter.ConvertDataTableToList<Unit>(data);
            if (!website)
                list.RemoveAll(x => x.Website = false);
            return list;
        }

    }
}