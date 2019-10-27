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

        private object NotAuthorized(string name, string param)
        {
            return new
            {
                status = 401,
                message = $"{name} Authentication failed! Please check {param}."
            };
        }

        public object AuthentiacteAPI(IAccessKey ApiKey)
        {
            try
            {
                var DbAccessKey = new
                {
                    TokenKey = "",
                    GroupKey = ""
                };
                if (DbAccessKey.TokenKey == ApiKey.TokenKey)
                {
                    var Token = GenerateToken(ApiKey, "API");
                    return Token;
                }
                return NotAuthorized("API", "(TokenKey and GroupKey)");

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
                var userAccount = new
                {
                    Username = "",
                    Password = ""
                };
                if (userAccount.Username == user.Username)
                {
                    var Token = GenerateToken(user, "User");
                    return Token;
                }
                return NotAuthorized("User", "(Username and Password)");
            }
            catch (Exception error)
            {
                throw error;
            }
        }
    }
}