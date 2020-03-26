using System;
using System.Collections.Generic;
using api.Database;
using api.Models;
using api.Models.Error;
using api.Security;
using api.Security.AuthTemplate;
using Components.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using xEventLogger;
using xEventLogger.Interface;
using xFilewriter;

namespace Components.Security
{
    public class AuthorizeRoles : Attribute, IAuthorizationFilter
    {
        private readonly string[] UserAssignedRole;
        private AuthorizationFilterContext Context { get; set; }
        /// Clean up the constructor to better fit the what its supposed to do 
        // The first parameter should be roles only and the rest should be injected after
        private IEventLogger Logger { get; }
        public AuthorizeRoles(params string[] roles)
        {
            UserAssignedRole = roles;
            Logger = new EventLogger(new FileWriter());
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
                    var databaseHelper = new DatabaseHelper(ClaimHelper, Logger, new SqlQueryHelper());
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
                    Logger.LogEventAsync(error, "error.json");
                    RejectRequest("An unhandled Exception has occured", 500);
                }
            else
                RejectRequest("Not Authorized", 401);
        }
    }
}