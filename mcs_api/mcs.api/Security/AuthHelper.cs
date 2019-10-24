using mcs.api.Security.Interface;

namespace mcs.api.Security
{
    public class AuthHelper
    {
        IClaimHelper _ClaimHelper { get; set; }
        IJwtAuthenticator _JwtAuthenticator { get; set; }



    }
}