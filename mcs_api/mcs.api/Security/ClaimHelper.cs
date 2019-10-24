using System;
using System.Security.Claims;

namespace mcs.api.Security
{
    public class ClaimsHelper
    {
        private Claim AddClaim(string claimName, string value)
            => new Claim(claimName, value);

        public ClaimsIdentity AddDataToClaim<T>(ClaimsIdentity identity, T objectType)
        {
            Type temp = typeof(T);
            var properties = temp.GetProperties();
            foreach (var property in properties)
            {
                identity.AddClaim(AddClaim(property.Name,
                    property.GetValue(objectType, null).ToString()));
            };
            return identity;
        }
    }
}