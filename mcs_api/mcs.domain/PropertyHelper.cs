using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        private DataTable GetApartmentsFromSql()
            => Sql.SelectQuery<Unit>(
                "Select * from property, unit where property.ref_id = unit.ref_id and unit_type_id = 3", null);

        private void RemoveUnits(List<Unit> list)
            => list.RemoveAll(x => x.Website == false);

        public Unit GetApartment(int refId, bool website = true)
        {
            var data = GetApartmentsFromSql();
            var list = ObjectConverter.ConvertDataTableToList<Unit>(data);
            if (!website)
                RemoveUnits(list);
            var apartment = list.FirstOrDefault(x => x.Ref_Id == refId);
            return apartment;
        }

        public IReadOnlyCollection<Unit> GetApartments(bool website = true)
        {
            var data = GetApartmentsFromSql();
            var list = ObjectConverter.ConvertDataTableToList<Unit>(data);
            if (!website)
                RemoveUnits(list);
            return list;
        }

    }
}