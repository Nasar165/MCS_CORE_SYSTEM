using System;
using System.Collections.Generic;
using api;
using api.Database;
using api.Security;
using api.Security.AuthTemplate;
using Components.Errorhandler;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Components.Security
{
    public class AuthorizeRoles : Attribute, IAuthorizationFilter
    {
        private readonly string[] UserAssignedRole;

        public AuthorizeRoles(params string[] roles)
            => this.UserAssignedRole = roles;

        private void RejectRequest(AuthorizationFilterContext context)
            => context.Result = new ErrorRespons("Permission Dennied", 403);

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

        public void OnAuthorization(AuthorizationFilterContext context)
        {   
            try
            {
                var claims = context.HttpContext.User.Claims;
                var claimHelper = new ClaimsHelper(claims);
                IEnumerable<Roles> roles = null;
                var key = AesEncrypter._instance.DecryptyData(
                    claimHelper.GetValueFromClaim("key"));
                if (IsUser(claimHelper.GetValueFromClaim("aud")))
                    roles = DatabaseHelper.Instance.GetRolesFromUser(key);
                else
                    roles = DatabaseHelper.Instance.GetRolesFromToken(key);

                if (!UserHasPermission(roles))
                    RejectRequest(context);
            }
            catch(Exception error)
            {
                // make error logging Asyncronous.
                ErrorLogger.Instance.LogError(error);
                context.Result = new ErrorRespons("An unhandled Exception has occured", 500);
            }

        }
    }
}