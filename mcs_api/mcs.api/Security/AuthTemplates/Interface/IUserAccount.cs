namespace mcs.api.Security.Interface
{
    public interface IUserAccount
    {
        string UserName { get; set; }
        string Password { get; set; }
    }
}