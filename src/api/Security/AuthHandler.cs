using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using api.Models;
using api.Security.Interface;
using Components.Security;
using xAuth;
using xAuth.Interface;
using xEventLogger.Interface;
using xSql.Interface;

namespace api.Security
{
    public class AuthHandler : IAuthHandler
    {
        private IAuth UserAuth { get; }
        private IAuth TokenAuth { get; }
        private IEventLogger Logger { get; }
        private readonly string Domain = AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings", "Domain");

        public AuthHandler(IJwtGenerator jwt, ISqlHelper sql, IEventLogger logger)
        {
            if (jwt is null || sql is null || logger is null)
                throw new Exception("AuthHandler:A constructor parameter is null");

            UserAuth = new UserAuth(sql, jwt);
            TokenAuth = new TokenAuth(sql, jwt);
            Logger = logger;
        }

        public List<Claim> GenerateClaim(int id)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var encryptedId = AesEncrypter._instance.EncryptData(id.ToString());
            claims.Add(new Claim("key", encryptedId));
            return claims;
        }

        public ITokenRespons UserAuthentication(IUser user)
        {
            try
            {
                var token = UserAuth.Authentiacte(user, "user", Domain, GenerateClaim);
                return token;
            }
            catch
            {
                return null;
            }
        }

        public ITokenRespons UserRefreshToken(string refreshToken)
        {
            try
            {
                var token = UserAuth.RefreshToken(refreshToken, "user", Domain, GenerateClaim);
                return token;
            }
            catch
            {
                return null;
            }
        }

        public ITokenRespons TokenAuthentication(IToken tokenKey)
        {
            try
            {
                var token = TokenAuth.Authentiacte(tokenKey, "token", Domain, GenerateClaim);
                return token;
            }
            catch
            {
                return null;
            }
        }

        public ITokenRespons TokenRefreshToken(string refreshToken)
        {
            try
            {
                var token = TokenAuth.RefreshToken(refreshToken, "token", Domain, GenerateClaim);
                return token;
            }
            catch
            {
                return null;
            }
        }
    }
}