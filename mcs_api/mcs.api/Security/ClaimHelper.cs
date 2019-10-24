using System;
using System.Collections.Generic;
using System.Security.Claims;
using mcs.api.Security.Interface;

namespace mcs.api.Security
{
    public class ClaimsHelper : IClaimHelper
    {
        private Claim AddClaim(string claimName, string value)
            => new Claim(claimName, value);

        public List<Claim> AddDataToClaim<T>(T objectType)
        {
            Type temp = typeof(T);
            var properties = temp.GetProperties();
            var claims = new List<Claim>();
            foreach (var property in properties)
            {
                claims.Add(new Claim(property.Name,
                    property.GetValue(objectType, null).ToString()));
            };
            return claims;
        }
    }
}