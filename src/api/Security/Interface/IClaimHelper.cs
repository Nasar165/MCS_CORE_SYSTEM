using System.Collections.Generic;
using System.Security.Claims;
using static api.Security.ClaimsHelper;

namespace api.Security.Interface
{
    public interface IClaimHelper
    {
        List<Claim> AddDataToClaim<T>(T objectType, ClaimAction action);
        List<Claim> AddRolesToClaim(List<Claim> claims, params string[] Roles);
        string GetValueFromClaim(string type);
    }
}