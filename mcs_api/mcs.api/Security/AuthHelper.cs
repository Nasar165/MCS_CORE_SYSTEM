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

        private object GenerateToken<T>(T data, string audiance)
        {
            try
            {
                var claimList = _ClaimHelper.AddDataToClaim<T>(data);
                var Token = _JwtAuthenticator.CreateJwtToken(claimList, audiance, "mcsunity.net");
                return Token;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public object AuthentiacteAPI(IAccessKey ApiKey)
        {
            try
            {
                var Token = GenerateToken(ApiKey, "API");
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
                var Token = GenerateToken(user, "user");
                return Token;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}