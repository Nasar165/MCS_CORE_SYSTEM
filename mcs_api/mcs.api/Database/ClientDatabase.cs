namespace mcs.api.Database
{
    public class ClientDatabase
    {
        public int Database_Id { get; set; }
        public string Database_Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        // Database Management type
        public string Dbm { get; set; }

    }
}