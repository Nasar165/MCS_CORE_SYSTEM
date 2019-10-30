namespace mcs.api.Security.AuthTemplate.Interface
{
    public interface IAccessKey
    {
        string TokenKey { get; set; }
        string GroupKey { get; set; }
        void SetRoles(params string[] roles);
        string[] GetRoles();
    }
}