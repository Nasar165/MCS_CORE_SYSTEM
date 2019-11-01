using mcs.components.DbConnection;

namespace mcs.api.Models
{
    public class DatabaseHelper
    {
        private object createDb(int databaseId)
            => new { Database_id = databaseId };


        public void GetDatabase(int databaseId)
        {
            var databaseKey = createDb(databaseId);
        }
    }
}