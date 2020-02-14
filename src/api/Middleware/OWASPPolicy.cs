using System.Collections.Generic;
using api.Middleware.Constants;
using api.Middleware.Interface;

namespace api.Middleware
{
    public class OWASPPolicy : IHeaderPolicy
    {
        public IList<Policy> Headers { get; }
        public OWASPPolicy()
            => Headers = new List<Policy>();

        private void AddPolicy(string header, string value, bool remove = false)
            => Headers.Add(new Policy() { Header = header, Value = value, Remove = remove });
        private void SetXContentType()
            => AddPolicy(XContentTypeOptions.Header, XContentTypeOptions.NoSniff);
        private void SetFrameOption()
            => AddPolicy(FrameOptions.Header, FrameOptions.Deny);
        private void SetXssProtection()
            => AddPolicy(XSSProtectionOptions.Header, XSSProtectionOptions.Mode1Block);
        private void SetXPermitedCrossDomain()
            => AddPolicy(XPermitedCrossDomainOptions.Header, XPermitedCrossDomainOptions.None);
        private void SetReferrerPolicy()
            => AddPolicy(ReferrerPolicyOptions.Header, ReferrerPolicyOptions.NoReferrer);
        private void RemoveServer()
            => AddPolicy("Server", "", true);
        public void BuildPolicies()
        {
            SetXContentType();
            SetFrameOption();
            SetXssProtection();
            SetXPermitedCrossDomain();
            SetReferrerPolicy();
            RemoveServer();
        }
    }
}