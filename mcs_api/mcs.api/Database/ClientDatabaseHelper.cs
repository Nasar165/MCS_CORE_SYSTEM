using mcs.api.Security.Interface;
using mcs.components;
using mcs.components.DbConnection;

namespace mcs.api.Database
{
    public class ClientDatabaseHelper
    {
        IClaimHelper ClaimHelper { get; }
        public ClientDatabaseHelper(IClaimHelper claimHelper)
            => ClaimHelper = claimHelper;

        private object createDb(int databaseId)
            => new { Database_id = databaseId };

        public void GetDatabase()
        {
            var id = ClaimHelper.GetValueFromClaim("Database_Id");
            var dbId = ObjectConverter.ConvertStringToInt(id);
            var databaseKey = createDb(dbId);
        }
    }
}