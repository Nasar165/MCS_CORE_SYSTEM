
using System.Collections.Generic;

namespace api.Middleware.Interface
{
    public interface IHeaderPolicy
    {
        IList<Policy> Headers { get; }
        void BuildPolicies();
    }
}