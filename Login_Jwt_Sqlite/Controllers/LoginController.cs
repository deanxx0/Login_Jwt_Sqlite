using Login_Jwt_Sqlite.Models;
using Login_Jwt_Sqlite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly string _jwtSeretKey;

        public LoginController(UserDbService userDbService, IConfiguration configuration)
        {
            _jwtSeretKey = configuration.GetValue<string>("JwtSecretKey");
            _userDbService = userDbService;
        }

        [HttpPost("Login/{userName}/{password}")]
        public async Task<ActionResult> Login(string userName, string password)
        {
            var user = _userDbService.FindUser(userName);
            if (user == null) return Unauthorized();
            if (user.Password != password) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSeretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(user.UserClaim.ToString(), "true"),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            HttpContext.Response.Headers.Add("access_token", tokenString);
            return Ok(tokenString);
        }
    }
}
