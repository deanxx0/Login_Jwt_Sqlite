﻿using Login_Jwt_Sqlite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Login_Jwt_Sqlite.Controllers
{
    [Route("")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserDbService _userDbService;
        public LoginController(UserDbService userDbService)
        {
            _userDbService = userDbService;
        }

        [HttpPost("Login/{userName}/{password}")]
        public async Task<ActionResult> Login(string userName, string password)
        {
            var user = _userDbService.FindUser(userName);
            if (user == null) return NotFound();
            if (user.Password != password) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaa");

            var tokenDescriptor = new SecurityTokenDescriptor();
            if (user.UserClaim.ToString() == "Member")
            {
                tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("Member", "true"),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
            }
            if (user.UserClaim.ToString() == "Admin")
            {
                tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("Member", "true"),
                        new Claim("Admin", "true"),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
            }

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            HttpContext.Response.Headers.Add("access_token", tokenString);
            return Ok(tokenString);
        }
    }
}