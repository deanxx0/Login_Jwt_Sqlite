using Login_Jwt_Sqlite.Models;
using Login_Jwt_Sqlite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Jwt_Sqlite.Controllers
{
    [Route("")]
    [ApiController]
    public class UserDbController : ControllerBase
    {
        private readonly UserDbService _userDbService;

        public UserDbController(UserDbService userDbService)
        {
            _userDbService = userDbService;
        }

        [HttpPost("UsersTable")]
        public ActionResult CreateUsersTable()
        {
            _userDbService.CreateUsersTable();
            return Ok();
        }

        [HttpPost("User")]
        public ActionResult CreateUser(CreateUser createUser)
        {
            _userDbService.InsertUser(createUser);
            return Ok();
        }

        [HttpGet("User")]
        public ActionResult FindAllUser() => Ok(_userDbService.FindAllUsers());

        [HttpGet("User/{userName}")]
        public ActionResult FindUser(string userName)
        {
            var user = _userDbService.FindUser(userName);
            return Ok(user);
        }

        [HttpDelete("User/{userName}")]
        public ActionResult DeleteUser(string userName)
        {
            _userDbService.DeleteUser(userName);
            return Ok();
        }
    }
}
