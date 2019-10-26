using mcs.api.Security.Interface;

namespace mcs.api.Security
{
    public class AuthHelper : IAuthHelper
    {
        IClaimHelper _ClaimHelper { get; set; }
        IJwtAuthenticator _JwtAuthenticator { get; set; }

        public object AuthentiacteAPI(IAccessKey ApiKey)
        {
            throw new System.NotImplementedException();
        }

        public object AuthenticateUser(IUserAccount user)
        {
            throw new System.NotImplementedException();
        }
    }
}