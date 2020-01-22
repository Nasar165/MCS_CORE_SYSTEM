using System.Net;

namespace Components.Mail.Interface
{
    public interface IServerConfig
    {
        string Host { get; set; }
        int Port { get; set; }
        bool SSL { get; set; }
        bool DefaultCredentials { get; set; }
        NetworkCredential UserAccount { get; set; }
    }
}