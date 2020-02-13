using api.Middleware.Interface;
using Microsoft.AspNetCore.Http;

namespace api.Middleware
{
    public class OWASPHeaderPolicy : IHeaderPolicy
    {
        private IHeaderDictionary Header { get; set; }
        private void AddSecurityToHeader()
        {
            Header.Add("X-Content-Type-Options", "nosniff");
            Header.Add("X-Frame-Options", "deny");
            Header.Add("X-XSS-Protection", "1; mode=block");
            Header.Add("Content-Security-Policy", "default-src 'none'");
            Header.Add("content-type", "application/json");
            Header.Add("X-Permitted-Cross-Domain-Policies", "none");
            Header.Add("Referrer-Policy", "no-referrer");
            Header.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
            Header.Add("Expect-CT", "max-age=86400, enforce");
        }

        private void RemoveVaulnerabiltiesFromHeader()
        {
            Header.Remove("Server");
        }

        public void AddPolicyToHeader(IHeaderDictionary header)
        {
            Header = header;
            AddSecurityToHeader();
            RemoveVaulnerabiltiesFromHeader();
        }
    }
}