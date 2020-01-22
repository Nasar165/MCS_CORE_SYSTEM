using System.Net;
using Components.Mail.Interface;

namespace Components.Mail
{
    public class ServerConfig : IServerConfig
    {
        public string Host { get; set; }
        public int Port { get; set; } = 465;
        public bool SSL { get; set; } = true;
        public bool DefaultCredentials { get; set; } = false;
        public NetworkCredential UserAccount { get; set; }

        public ServerConfig(string username, string password)
        {
            UserAccount = new NetworkCredential()
            {
                UserName = username,
                Password = password
            };
        }
    }
}