﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration.UserSecrets;
using CareerCrafterClassLib.Model;
namespace JWT.Logic
{


    public class TokenGeneration
    {
        private readonly IConfiguration _configuration;
        public TokenGeneration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJWT(string userId, string role, string department, string accessLevel, int employerId)
        {

            //Form Security Key and Credential
            var key = _configuration.GetValue<string>("ApiSettings:Secret");
            var securedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var securityCredentials = new SigningCredentials(securedKey, SecurityAlgorithms.HmacSha256);


            //Define Claims with a List of Claims 
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub,userId),
                new Claim(ClaimTypes.Role,role),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()), //Unique Id
                new Claim("Department",department),
                new Claim("accessLevel",accessLevel),
                new Claim("EmployerId", employerId.ToString()) // Add EmployerId as a claim
            };

            //Define the Token Object
            var token = new JwtSecurityToken(

                  issuer: "hexaware.com",
                  audience: "Interns",
                  claims: claims,
                  expires: DateTime.Now.AddHours(1),
                  signingCredentials: securityCredentials
                );


            var tokenS = new JwtSecurityTokenHandler();

            return tokenS.WriteToken(token);
        }

    }
}