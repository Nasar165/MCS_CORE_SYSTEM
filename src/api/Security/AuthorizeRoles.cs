using System;
using api;
using api.Security;
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

        private string[] GetRolesFromUser()
        {
            return new string[] { "", "AddProperty", "DeleteProperty" };
        }

        private string[] GetRolesFromToken()
        {
            return new string[] { "Admin", "AddProperty", "DeleteProperty" };
        }

        public bool HasPermission(string role)
        {
            foreach (var userAssignedRole in UserAssignedRole)
            {
                if (Validation.StringsAreEqual(role, userAssignedRole))
                    return true;
            }
            return false;
        }

        public bool UserHasPermission(string[] roles)
        {
            foreach (var role in roles)
            {
                if (HasPermission(role))
                    return true;
            }
            return false;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var claimHelper = new ClaimsHelper(claims);
            string[] roles;

            if (IsUser(claimHelper.GetValueFromClaim("aud")))
                roles = GetRolesFromUser();
            else
                roles = GetRolesFromToken();

            if (!UserHasPermission(roles))
                RejectRequest(context);
        }
    }
}