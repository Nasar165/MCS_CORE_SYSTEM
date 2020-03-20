using System.Collections.Generic;
using api.Middleware.Constants;
using Components;
using xheaderSecurity.Interface;

namespace api.Middleware
{
    public class OWASPPolicy : IHeaderPolicy
    {
        public IList<IPolicy> Headers { get; }

        public OWASPPolicy()
            => Headers = new List<IPolicy>();

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
            if (!Validation.ValueIsGreateherThan(Headers.Count, 0))
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
}