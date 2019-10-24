using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace mcs.api.Security
{
    public class JwtAuthenticator
    {
        private SymmetricSecurityKey GetSecurityKey()
            => new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

        private Object ConvertToken(JwtSecurityToken token)
        {
            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }

        private SigningCredentials GenerateSigningCredential()
        {
            var key = GetSecurityKey();
            return new Microsoft.IdentityModel.Tokens.
                SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        public object CreateJwtToken(Claim[] claim)
        {
            var token = new JwtSecurityToken(
                issuer: "mcsunity.net",
                audience: "mcsunity.net",
                expires: DateTime.UtcNow.AddHours(1),
                claims: claim,
                signingCredentials: GenerateSigningCredential()
            );
            var data = ConvertToken(token);
            return data;

        }
    }
}