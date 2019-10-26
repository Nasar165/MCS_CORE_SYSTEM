using System;
using mcs.api.Security.AuthTemplate.Interface;
using mcs.api.Security.Interface;

namespace mcs.api.Security
{
    public class AuthHelper : IAuthHelper
    {
        IClaimHelper _ClaimHelper { get; set; }
        IJwtAuthenticator _JwtAuthenticator { get; set; }
        public AuthHelper()
        {
            _ClaimHelper = new ClaimsHelper();
            _JwtAuthenticator = new JwtAuthenticator();
        }

        public object AuthentiacteAPI(IAccessKey ApiKey)
        {
            try
            {
                var claimList = _ClaimHelper.AddDataToClaim<IAccessKey>(ApiKey);
                var Token = _JwtAuthenticator.CreateJwtToken(claimList, "API", "mcsunity.net");
                return Token;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public object AuthenticateUser(IUserAccount user)
        {
            try
            {
                var claimList = _ClaimHelper.AddDataToClaim<IUserAccount>(user);
                var Token = _JwtAuthenticator.CreateJwtToken(claimList, "API", "mcsunity.net");
                return Token;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}