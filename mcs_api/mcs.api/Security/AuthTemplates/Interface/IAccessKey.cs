namespace mcs.api.Security.Interface
{
    public interface IAccessKey
    {
        string TokenKey { get; set; }
        string GroupKey { get; set; }
    }
}