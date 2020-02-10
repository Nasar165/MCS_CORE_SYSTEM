using Microsoft.AspNetCore.Http;

namespace api.Middleware.Interface
{
    public interface IHeaderPolicy
    {
        void AddPolicyToHeader(IHeaderDictionary header);
    }
}