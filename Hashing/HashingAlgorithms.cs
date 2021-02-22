using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Server_Try02.Models;

namespace WebApi.Hashing
{
    public class HashingAlgorithms
    {

        public void CreateHash(String Password, out byte[] Hash, out byte[] Salt)
        //Creates a hash and a salt from password using sha1 algorithm
        {
            var hmac = new System.Security.Cryptography.HMACSHA1();
            Salt = hmac.Key;
            Hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

            hmac.Dispose();
        }

        public bool VerifyHash(String Password, byte[] Hash, byte[] Salt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA1(Salt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != Hash[i])
                {
                    return false;
                }

            }
            return true;
        }

        public String CreateToken(UserModel User)         
           //Creates Token
        {
            var claims = new List<Claim> //list of claims in which we user username and email
            {
                new Claim(ClaimTypes.Name,User.Username),
                new Claim(ClaimTypes.Email,User.Email),
                new Claim(ClaimTypes.Role,User.Role)

            };
            //creates a key based on a string 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AFhu9aVrbdUN3G4Vv87jWA7gCSuJG8c8D7cp7XrUMayEtcryWpGJzDQMnV7"));

            //encoding  using sha512 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor //creating a tokendescriptor based on claims
                                                                                  //and an expiry date
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        
    }
}