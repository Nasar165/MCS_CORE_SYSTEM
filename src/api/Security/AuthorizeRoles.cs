using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Components.Security
{
    public class AuthorizeRoles: Attribute, IAuthorizationFilter
    {
        private readonly string[] userAssignedRole; 

        public AuthorizeRoles(params string[] roles) 
        { 
            this.userAssignedRole = roles; 
        } 

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claim = context.HttpContext.User.Claims;
            //context.Result = new ForbidResult();
        }
    }
}