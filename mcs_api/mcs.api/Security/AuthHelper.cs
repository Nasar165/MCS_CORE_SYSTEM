using System;
using System.Security.Authentication;
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

        private T GetCredentialsFromSql<T>(string tableQuery, T account)
        {
            var mcsdbcon = AppConfigHelper.Instance.GetDbConnection();
            var sql = new NpgSqlHelper(mcsdbcon);
            var dataTable = sql.SelectQuery($"Select * from {tableQuery}", account);
            if (dataTable.Rows.Count > 0)
                return ObjectConverter.ConvertDataTableToList<T>(dataTable)[0];
            else
                throw new InvalidCredentialException($"{account} Authentication failed! ");
        }

        private void LogAuthentication(string name)
        {
            var mcsdbcon = AppConfigHelper.Instance.GetDbConnection();
            var sql = new NpgSqlHelper(mcsdbcon);
        }

        private object GenerateToken<T>(T data, string audiance)
        {
            try
            {
                var claimList = _ClaimHelper.AddDataToClaim<T>(data);
                var Token = _JwtAuthenticator.CreateJwtToken(claimList
                    , audiance, "mcsunity.net");
                return Token;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public object AuthentiacteAPI(IAccessKey apiKey)
        {
            try
            {
                var dbApiKey = GetCredentialsFromSql<AccessKey>(
                    $"token where tokenkey = @tokenkey", (AccessKey)apiKey);
                if (apiKey.TokenKey == dbApiKey.TokenKey && apiKey.GroupKey == dbApiKey.GroupKey)
                {
                    LogAuthentication(apiKey.TokenKey);
                    var Token = GenerateToken(dbApiKey, "API");
                    return Token;
                }
                return false;

            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
                return false;
            }
        }

        public object AuthenticateUser(IUserAccount user)
        {
            try
            {
                var dbuser = GetCredentialsFromSql<UserAccount>(
                    $"useraccount where username = '@username'", (UserAccount)user);
                if (user.Username == dbuser.Username && user.Password == dbuser.Password)
                {
                    LogAuthentication(user.Username);
                    var Token = GenerateToken(dbuser, "User");
                    return Token;
                }
                return false;
            }
            catch (Exception error)
            {
                ErrorLogger.Instance.LogError(error);
                return false;
            }
        }
    }
}