using System;
using System.Security.Authentication;
using mcs.api.Database;
using mcs.api.Models;
using mcs.api.Security.AuthTemplate;
using mcs.api.Security.AuthTemplate.Interface;
using mcs.api.Security.Interface;
using mcs.Components;
using mcs.Components.DbConnection;
using mcs.Components.Errorhandler;
using mcs.Components.Security;

namespace mcs.api.Security
{
    public delegate void AuthAction();
    public class AuthHelper : IAuthHelper
    {
        IClaimHelper _ClaimHelper { get; set; }
        IJwtAuthenticator _JwtAuthenticator { get; set; }
        public AuthHelper()
        {
            _ClaimHelper = new ClaimsHelper();
            _JwtAuthenticator = new JwtAuthenticator();
        }

        private T GetCredentialsFromSql<T>(string tableQuery, SqlCommandHelper<T> account, string name)
        {
            var sql = DatabaseHelper.Instance.GetMcsConnection();
            var dataTable = sql.SelectQuery($"Select * from {tableQuery};", account);
            if (dataTable.Rows.Count > 0)
                return ObjectConverter.ConvertDataTableToList<T>(dataTable)[0];
            else
                throw new InvalidCredentialException($"{name} Authentication failed! ");
        }

        private void LogAuthentication(string name)
        {
            var sql = DatabaseHelper.Instance.GetMcsConnection();
            var authLogg = new AuthLogg() { Username = name };
            ErrorLogger.Instance.LogAuthentication(sql, authLogg);
        }

        private object GenerateToken<T>(T data, string audiance, params string[] roles)
        {
            try
            {
                var claimList = _ClaimHelper.AddDataToClaim<T>(data, AesEncrypter._instance.EncryptData);
                if(!Validation.ObjectIsNull(roles))
                    claimList = _ClaimHelper.AddRolesToClaim(claimList, roles);
                var Token = _JwtAuthenticator.CreateJwtToken(claimList
                    , audiance, "mcsunity.net");
                return Token;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        private TokenKey ProvideMinimalDataToToken(int key)
            => new TokenKey() { Key = key };

        public object AuthentiacteAPI(IAccessKey apiKey)
        {
            try
            {
                var sqlcommand = new SqlCommandHelper<AccessKey>(
                    (AccessKey)apiKey, "groupkey");
                var dbApiKey = GetCredentialsFromSql<AccessKey>(
                    $"token where tokenkey = @tokenkey", sqlcommand, apiKey.TokenKey);
                if (apiKey.TokenKey == dbApiKey.TokenKey && apiKey.GroupKey == dbApiKey.GroupKey && dbApiKey.Active == true)
                {
                    LogAuthentication(dbApiKey.TokenKey);
                    var tokenClaim = ProvideMinimalDataToToken(dbApiKey.TokenKey_Id);
                    var Token = GenerateToken(tokenClaim, "API", null);
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

        public object AuthenticateUser(IUserAccount user, AuthAction method)
        {
            try
            {
                var sqlcommand = new SqlCommandHelper<UserAccount>(
                    (UserAccount)user, "password");
                var dbuser = GetCredentialsFromSql<UserAccount>(
                    $"useraccount where username = @username", sqlcommand, user.Username);
                if(method != null)
                    method();
                if (user.Username == dbuser.Username && user.Password == dbuser.Password)
                {
                    LogAuthentication(user.Username);
                    var tokenClaim = ProvideMinimalDataToToken(dbuser.UserAccount_Id);
                    var Token = GenerateToken(tokenClaim, "User", null);
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