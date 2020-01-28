using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using api.Security.Interface;
using Components;
using Microsoft.AspNetCore.Http;

namespace api.Security
{
    public class ClaimsHelper : IClaimHelper
    {
        public delegate string ClaimAction(string data);
        IEnumerable<Claim> Claims { get; }
        public ClaimsHelper(IHttpContextAccessor claims)
            => Claims = claims.HttpContext.User.Claims;

        public string GetValueFromClaim(string type)
        {
            var data = "";
            if (Validation.ValueIsGreateherThan(Claims.Count(), 0))
                data = Claims.FirstOrDefault(x => x.Type == type).Value;
            return data;
        }

        private Claim AddClaim(string claimName, string value)
            => new Claim(claimName, value);

        private List<Claim> AddJtiToClaim(List<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            return claims;
        }

        private List<Claim> AddRoleToClaim(List<Claim> claims, string role)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
            return claims;
        }

        public List<Claim> AddRolesToClaim(List<Claim> claims, params string[] Roles)
        {
            foreach (var role in Roles)
            {
                claims = AddRoleToClaim(claims, role);
            }
            return claims;
        }

        public List<Claim> AddDataToClaim<T>(T data, ClaimAction action)
        {
            var claims = new List<Claim>();
            claims = AddJtiToClaim(claims);
            var properties = ReflectionHelper.GetPropertiesOfObject(data);
            foreach (var property in properties)
            {
                if (!Validation.ObjectIsNull(property.GetValue(data)))
                {
                    var value = "";
                    if (action != null)
                        value = action(property.GetValue(data, null).ToString());
                    else
                        value = property.GetValue(data, null).ToString();

                    claims.Add(new Claim(property.Name.ToLower(), value));
                }
            }
            return claims;
        }
    }
}