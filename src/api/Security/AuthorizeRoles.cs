using System;
using System.Collections.Generic;
using api.Database;
using api.Models.Error;
using api.Security;
using api.Security.AuthTemplate;
using Components.Errorhandler;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Components.Security
{
    public class AuthorizeRoles : Attribute, IAuthorizationFilter
    {
        private readonly string[] UserAssignedRole;
        private AuthorizationFilterContext Context { get; set; }

        public AuthorizeRoles(params string[] roles)
            => this.UserAssignedRole = roles;

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
                    var claims = Context.HttpContext.User.Claims;
                    var claimHelper = new ClaimsHelper(claims);
                    IEnumerable<Roles> roles = null;
                    var key = AesEncrypter._instance.DecryptyData(
                        claimHelper.GetValueFromClaim("key"));
                    if (IsUser(claimHelper.GetValueFromClaim("aud")))
                        roles = DatabaseHelper.Instance.GetRolesFromUser(key);
                    else
                        roles = DatabaseHelper.Instance.GetRolesFromToken(key);

                    if (!UserHasPermission(roles))
                        RejectRequest("Permission denied", 403);
                }
                catch (Exception error)
                {
                    ErrorLogger.Instance.LogErrorAsync(error);
                    RejectRequest("An unhandled Exception has occured", 500);
                }
            else
                RejectRequest("Not Authorized", 401);
        }
    }
}