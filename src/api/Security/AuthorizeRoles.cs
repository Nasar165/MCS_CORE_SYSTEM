using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Components.Security
{
    public class AuthorizeRoles: Attribute, IAuthorizationFilter
    {
        private readonly string[] userAssignedRole; 
        private AuthorizationFilterContext Context { get; set; }

        public AuthorizeRoles(params string[] roles) 
            =>this.userAssignedRole = roles; 

        private void RejectRequest()
            => Context.Result = new ForbidResult();

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Context = context;
        }
    }
}