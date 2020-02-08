using System;
using System.Security.Authentication;
using api.Database.Interface;
using api.Models;
using api.Security.AuthTemplate;
using api.Security.AuthTemplate.Interface;
using api.Security.Interface;
using Components;
using Components.Database;
using Components.Database.Interface;
using Components.Logger.Interface;
using Components.Security;

namespace api.Security
{
    public delegate void AuthAction();
    public class AuthHelper : IAuthHelper
    {
        private IClaimHelper _ClaimHelper { get; }
        private IJwtAuthenticator _JwtAuthenticator { get; }
        private IDatabaseHelper DatabaseHelper { get; }
        private ILogger Logger { get; }
        private IQueryHelper QueryHeler { get; }
        public AuthHelper(IJwtAuthenticator jwtAuthenticator
                , IDatabaseHelper database, IClaimHelper claimHelper, ILogger logger,
                IQueryHelper queryHelper)
        {
            _ClaimHelper = claimHelper;
            _JwtAuthenticator = jwtAuthenticator;
            DatabaseHelper = database;
            Logger = logger;
            QueryHeler = queryHelper;
        }

        private SqlCommandHelper<T> CreateSqlCommand<T>(T data, params string[] ignore)
            => new SqlCommandHelper<T>(data, ignore);

        private T GetCredentialsFromSql<T>(string procedureName, SqlCommandHelper<T> account, string name)
        {
            var sql = DatabaseHelper.GetDefaultConnection();
            var dataTable = sql.SelectQuery(QueryHeler.GetSqlQuery(procedureName), account);
            if (dataTable.Rows.Count > 0)
                return ObjectConverter.ConvertDataTableToList<T>(dataTable)[0];
            else
                throw new InvalidCredentialException($"{name} Authentication failed! ");
        }

        private void LogAuthentication(string name)
        {
            var sql = DatabaseHelper.GetDefaultConnection();
            var authLogg = new AuthLogg() { Username = name };
            Logger.LogAuthentication(sql, authLogg);
        }

        private TokenKey ProvideMinimalDataToToken(int key)
            => new TokenKey() { Key = key };

        public string GetDoamin()
            => AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings", "Domain");

        private object GenerateToken<T>(T data, string audiance, params string[] roles)
        {
            try
            {
                var claimList = _ClaimHelper.AddDataToClaim<T>(data, AesEncrypter._instance.EncryptData);
                if (!Validation.ObjectIsNull(roles))
                    claimList = _ClaimHelper.AddRolesToClaim(claimList, roles);
                var Token = _JwtAuthenticator.CreateJwtToken(claimList
                    , audiance, GetDoamin());
                return Token;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        private object AuthorizationProcedure(int key, string authName)
        {
            LogAuthentication(authName);
            var tokenClaim = ProvideMinimalDataToToken(key);
            var Token = GenerateToken(tokenClaim, "API", null);
            return Token;
        }

        public object AuthentiacteAPI(IAccessKey apiKey)
        {
            try
            {
                var sqlcommand = CreateSqlCommand((AccessKey)apiKey, "groupkey");
                var dbApiKey = GetCredentialsFromSql<AccessKey>("apiauth", sqlcommand, apiKey.TokenKey);
                if (apiKey.TokenKey == dbApiKey.TokenKey && apiKey.GroupKey == dbApiKey.GroupKey && dbApiKey.Active == true)
                {
                    return AuthorizationProcedure(dbApiKey.TokenKey_Id, dbApiKey.TokenKey);
                }
                return false;
            }
            catch (Exception error)
            {
                Logger.LogEventAsync(error);
                return false;
            }
        }

        public object AuthenticateUser(IUserAccount user, AuthAction method)
        {
            try
            {
                if (method != null)
                    method();
                var sqlcommand = CreateSqlCommand((UserAccount)user, "password");
                var dbuser = GetCredentialsFromSql<UserAccount>("userauth", sqlcommand, user.Username);
                if (user.Username == dbuser.Username && user.Password == dbuser.Password)
                {
                    return AuthorizationProcedure(dbuser.UserAccount_Id, dbuser.Username);
                }
                return false;
            }
            catch (Exception error)
            {
                Logger.LogEventAsync(error);
                return false;
            }
        }
    }
}
