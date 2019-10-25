namespace mcs.api.Models.Interface
{
    public interface IAccessKey
    {
        string TokenKey { get; set; }
        string GroupKey { get; set; }
    }
}