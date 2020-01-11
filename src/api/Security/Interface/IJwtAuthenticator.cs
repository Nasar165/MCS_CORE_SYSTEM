using System.Collections.Generic;
using System.Security.Claims;

namespace api.Security.Interface
{
    public interface IJwtAuthenticator
    {
        object CreateJwtToken(List<Claim> claim, string audiance, string issuer);
    }
}