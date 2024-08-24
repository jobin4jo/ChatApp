using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Common.jwt
{
    public static  class Jwt
    {
        public static string GenerateToken(string role, int id, string Key, string Issuer)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claimList = new List<Claim>();
            claimList.Add(new Claim(JwtRegisteredClaimNames.NameId, role));
            claimList.Add(new Claim(JwtRegisteredClaimNames.Sub, role.ToString()));
            claimList.Add(new Claim(JwtRegisteredClaimNames.Jti, id.ToString()));
            claimList.Add(new Claim(ClaimTypes.Role, role.ToString()));
            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Issuer,
                claims: claimList,
                expires: DateTime.Now.AddDays(10),
                signingCredentials: credentials
                );
            var encodedtoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedtoken;
        }
    }
}
