using api.Database.Interface;

namespace api.Database
{
    public class ClientDatabase : IClientDatabase
    {
        public int Database_Id { get; set; }
        public string Database_Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        // Database Management type
        public string Dbm { get; set; }

        public string GetConnectionString()
            => $"Server=127.0.0.1;Database={Database_Name};Uid={Username};Pwd={Password};";
    }
}