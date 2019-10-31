namespace mcs.api.Security.AuthTemplate.Interface
{
    public interface IAccessKey
    {
        string TokenKey { get; set; }
        string GroupKey { get; set; }
    }
}