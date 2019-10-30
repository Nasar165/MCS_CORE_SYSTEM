using System;
using mcs.api.Models;
using mcs.api.Security.AuthTemplate;
using mcs.api.Security.AuthTemplate.Interface;
using mcs.api.Security.Interface;
using mcs.components;
using mcs.components.DbConnection;
using mcs.components.Errorhandler;

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

        private T GetCredentialsFromSql<T>(string tableName)
        {
            var mcsdbcon = AppConfigHelper.Instance.GetDbConnection();
            var sql = new NpgSqlHelper(mcsdbcon);
            var dataTable = sql.SelectQuery($"Select * from {tableName}");
            return ObjectConverter.ConvertDataTableToList<T>(dataTable)[0];
        }

        public object AuthentiacteAPI(IAccessKey apiKey)
        {
            try
            {
                var dbApiKey = GetCredentialsFromSql<AccessKey>($"token where tokenkey = '{apiKey.TokenKey}'");
                if (apiKey.TokenKey == dbApiKey.TokenKey && apiKey.GroupKey == dbApiKey.GroupKey)
                {
                    var Token = GenerateToken(dbApiKey, "API");
                    return Token;
                }
                return NotAuthorized("API", "(TokenKey and GroupKey)");

            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
                throw error;
            }
        }

        public object AuthenticateUser(IUserAccount user)
        {
            try
            {
                var dbuser = GetCredentialsFromSql<UserAccount>($"useraccount where username = '{user.Username}'");
                if (user.Username == dbuser.Username && user.Password == dbuser.Password)
                {
                    var Token = GenerateToken(dbuser, "User");
                    return Token;
                }
                return NotAuthorized("User", "(Username and Password)");
            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
                throw error;
            }
        }
    }
}