namespace mcs.api.Security.AuthTemplate.Interface
{
    public interface IUserAccount
    {
        string Username { get; set; }
        string Password { get; set; }
        void SetRoles(params string[] roles);
        string[] GetRoles();
    }
}