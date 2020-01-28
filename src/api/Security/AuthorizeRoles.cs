using System;
using System.Collections.Generic;
using api.Database.Interface;
using api.Models.Error;
using api.Security.AuthTemplate;
using api.Security.Interface;
using Components.Errorhandler;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Components.Security
{
    public class AuthorizeRoles : Attribute, IAuthorizationFilter
    {
        private readonly string[] UserAssignedRole;
        private AuthorizationFilterContext Context { get; set; }
        private IDatabaseHelper DatabaseHelper { get; }
        private IClaimHelper ClaimHelper { get; }
        /// Clean up the constructor to better fit the what its supposed to do 
        // The first parameter should be roles only and the rest should be injected after
        public AuthorizeRoles(IClaimHelper claimHelper, IDatabaseHelper databaseHelper, params string[] roles)
        {
            this.DatabaseHelper = databaseHelper;
            this.ClaimHelper = claimHelper;
            this.UserAssignedRole = roles;
        }

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
                    IEnumerable<Roles> roles = null;
                    var key = AesEncrypter._instance.DecryptyData(
                        ClaimHelper.GetValueFromClaim("key"));
                    if (IsUser(ClaimHelper.GetValueFromClaim("aud")))
                        roles = DatabaseHelper.GetRolesFromUser(key);
                    else
                        roles = DatabaseHelper.GetRolesFromToken(key);

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