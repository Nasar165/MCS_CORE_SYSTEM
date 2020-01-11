namespace api.Database.Interface
{
    public interface IClientDatabase
    {
        int Database_Id { get; set; }
        string Database_Name { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Dbm { get; set; }
        string GetConnectionString();
    }
}