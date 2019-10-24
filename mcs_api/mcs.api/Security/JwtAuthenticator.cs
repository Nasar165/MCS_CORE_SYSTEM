using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using mcs.api.Security.Interface;
using Microsoft.IdentityModel.Tokens;

namespace mcs.api.Security
{
    public class JwtAuthenticator : IJwtAuthenticator
    {
        private SymmetricSecurityKey GetSecurityKey()
            => new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecretToken"));

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

        public object CreateJwtToken(List<Claim> claim, string audiance, string issuer)
        {
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audiance,
                expires: DateTime.UtcNow.AddHours(1),
                claims: claim,
                signingCredentials: GenerateSigningCredential()
            );
            var data = ConvertToken(token);
            return data;

        }
    }
}