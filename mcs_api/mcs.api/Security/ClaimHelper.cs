using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using mcs.api.Security.Interface;
using mcs.components;

namespace mcs.api.Security
{
    public class ClaimsHelper : IClaimHelper
    {
        private Claim AddClaim(string claimName, string value)
            => new Claim(claimName, value);

        private List<Claim> AddJtiToClaim(List<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            return claims;
        }

        public List<Claim> AddDataToClaim<T>(T data)
        {
            var claims = new List<Claim>();
            claims = AddJtiToClaim(claims);
            var properties = ReflectionHelper.GetPropertiesOfObject(data);
            foreach (var property in properties)
            {
                claims.Add(new Claim(property.Name,
                    property.GetValue(data, null).ToString()));
            };
            return claims;
        }
    }
}