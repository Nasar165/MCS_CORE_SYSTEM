using api.Security.Interface;
using xAuth;
using xAuth.Interface;
using xSql.Interface;

namespace api.Security
{
    public class AuthHandler : IAuthHandler
    {
        private IJwtGenerator Jwt { get; }
        private ISqlHelper Sql { get; }
        public AuthHandler(IJwtGenerator jwt, ISqlHelper sql)
        {
            if (jwt is null)
                throw new System.Exception("Constructor Jwt Handler is null");

            if (sql is null)
                throw new System.Exception("Constructor Sql handler is null");

            Jwt = jwt;
            Sql = sql;
        }

        public ITokenRespons UserAuthentication(IUser user)
        {
            try
            {
                var userAuth = new UserAuth(Sql, Jwt);
                var token = userAuth.Authentiacte(user, "user", "localhost", null);
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
                var userAuth = new UserAuth(Sql, Jwt);
                var token = userAuth.RefreshToken(refreshToken, "user", "localhost", null);
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
                var tokenAuth = new TokenAuth(Sql, Jwt);
                var token = tokenAuth.Authentiacte(tokenKey, "token", "localhost", null);
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
                var userAuth = new UserAuth(Sql, Jwt);
                var token = userAuth.RefreshToken(refreshToken, "user", "localhost", null);
                return token;
            }
            catch
            {
                return null;
            }
        }
    }
}