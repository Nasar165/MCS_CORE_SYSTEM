using api.Database.Interface;

namespace api.Database
{
    public class ClientDatabase : IClientDatabase
    {
        public int Database_Id { get; set; }
        public string Database_Name { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        // Database Management type
        public string Dbm { get; set; }

        public string GetConnectionString()
            => $"Server={Ip}; Port={Port};Database={Database_Name};Uid={Username};Pwd={Password};";
    }
}