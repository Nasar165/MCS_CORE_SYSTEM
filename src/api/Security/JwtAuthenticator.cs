using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Security.Interface;
using Microsoft.IdentityModel.Tokens;
using api.Models;

namespace api.Security
{
    public class JwtAuthenticator : IJwtAuthenticator
    {
        private SymmetricSecurityKey GetSecurityKey()
        {
            var secretKey = AppConfigHelper.Instance.GetSecreatKey();
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }

        private SigningCredentials GenerateSigningCredential()
        {
            var key = GetSecurityKey();
            return new Microsoft.IdentityModel.Tokens.
                SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        private Object ConvertToken(JwtSecurityToken token)
        {
            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                authType = "Bearer",
                expiration = $"UTC:{token.ValidTo}"
            };
        }

        public object CreateJwtToken(List<Claim> claim, string audiance, string issuer)
        {
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audiance,
                expires: DateTime.Now.AddHours(1),
                claims: claim,
                signingCredentials: GenerateSigningCredential()
            );
            var data = ConvertToken(token);
            return data;
        }
    }
}