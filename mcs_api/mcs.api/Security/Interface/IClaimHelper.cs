using System.Collections.Generic;
using System.Security.Claims;

namespace mcs.api.Security.Interface
{
    public interface IClaimHelper
    {
        List<Claim> AddDataToClaim<T>(T objectType);
        string GetValueFromClaim(string type);
    }
}