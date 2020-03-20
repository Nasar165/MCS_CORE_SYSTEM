using xheaderSecurity.Interface;

namespace api.Middleware
{
    public class Policy : IPolicy
    {
        public string Header { get; set; }
        public string Value { get; set; }
        public bool Remove { get; set; }
    }
}