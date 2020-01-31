using System;
using System.Collections.Generic;
using api.Database;
using api.Models;
using api.Models.Error;
using api.Security;
using api.Security.AuthTemplate;
using Components.Logger;
using Components.Logger.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Components.Security
{
    public class AuthorizeRoles : Attribute, IAuthorizationFilter
    {
        private readonly string[] UserAssignedRole;
        private AuthorizationFilterContext Context { get; set; }
        /// Clean up the constructor to better fit the what its supposed to do 
        // The first parameter should be roles only and the rest should be injected after
        private ILogger Logger { get; }
        public AuthorizeRoles(params string[] roles)
        {
            UserAssignedRole = roles;
            var logAsJson = bool.Parse(AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings","LogAsJson"));
            Logger = new EventLogger(logAsJson);
        }

        private HttpContextAccessor CreateHttpContextAccessor()
            => new HttpContextAccessor() { HttpContext = Context.HttpContext };

        private void RejectRequest(string message, int statusCode)
            => Context.Result = new ErrorRespons(message, statusCode);

        private bool IsUser(string value)
            => Validation.StringsAreEqual(value, "User");

        public bool HasPermission(string role)
        {
            foreach (var userAssignedRole in UserAssignedRole)
            {
                if (Validation.StringsAreEqual(role, userAssignedRole))
                    return true;
            }
            return false;
        }

        public bool UserHasPermission(IEnumerable<Roles> roles)
        {
            foreach (var role in roles)
            {
                if (HasPermission(role.Name))
                    return true;
            }
            return false;
        }

        public bool UserIsAuthentiacted()
            => Context.HttpContext.User.Identity.IsAuthenticated;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Context = context;
            if (UserIsAuthentiacted())
                try
                {
                    var ClaimHelper = new ClaimHelper(CreateHttpContextAccessor());
                    var databaseHelper = new DatabaseHelper(ClaimHelper, Logger);
                    IEnumerable<Roles> roles = null;
                    var key = AesEncrypter._instance.DecryptyData(
                        ClaimHelper.GetValueFromClaim("key"));
                    if (IsUser(ClaimHelper.GetValueFromClaim("aud")))
                        roles = databaseHelper.GetRolesFromUser(key);
                    else
                        roles = databaseHelper.GetRolesFromToken(key);

                    if (!UserHasPermission(roles))
                        RejectRequest("Permission denied", 403);
                }
                catch (Exception error)
                {
                    Logger.LogEventAsync(error);
                    RejectRequest("An unhandled Exception has occured", 500);
                }
            else
                RejectRequest("Not Authorized", 401);
        }
    }
}