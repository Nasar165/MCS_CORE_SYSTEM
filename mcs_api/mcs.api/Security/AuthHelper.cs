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

        private T GetCredentialsFromSql<T>(string tableQuery, string account)
        {
            var mcsdbcon = AppConfigHelper.Instance.GetDbConnection();
            var sql = new NpgSqlHelper(mcsdbcon);
            // SQL INJECTION VULNERABILITY DETECTED
            var dataTable = sql.SelectQuery($"Select * from {tableQuery}");
            if (dataTable.Rows.Count > 0)
                return ObjectConverter.ConvertDataTableToList<T>(dataTable)[0];
            else
                throw new InvalidCredentialException($"{account} Authentication failed! ");
        }

        public object AuthentiacteAPI(IAccessKey apiKey)
        {
            try
            {
                var dbApiKey = GetCredentialsFromSql<AccessKey>(
                    $"token where tokenkey = '{apiKey.TokenKey}'", apiKey.TokenKey);
                if (apiKey.TokenKey == dbApiKey.TokenKey && apiKey.GroupKey == dbApiKey.GroupKey)
                {
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
                    $"useraccount where username = '{user.Username}'", user.Username);
                if (user.Username == dbuser.Username && user.Password == dbuser.Password)
                {
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