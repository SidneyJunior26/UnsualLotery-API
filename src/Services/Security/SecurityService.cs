﻿using System;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace UnsualLotery.Services.Security
{
    public class SecurityService
    {
        private readonly IConfiguration _config;

        public SecurityService(IConfiguration config)
        {
            _config = config;
        }

        public SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity subject)
        {
            var key = Encoding.ASCII.GetBytes(_config["JwtBearerTokenSettings:SecretKey"]);

            return new SecurityTokenDescriptor
            {
                Subject = subject,
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _config["JwtBearerTokenSettings:Audience"],
                Issuer = _config["JwtBearerTokenSettings:Issuer"],
                Expires = DateTime.UtcNow.AddHours(1),
            };
        }
    }
}