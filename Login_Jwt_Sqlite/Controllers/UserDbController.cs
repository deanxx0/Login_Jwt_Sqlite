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
        public ActionResult<User> CreateUser(CreateUser createUser)
        {
            var user = _userDbService.InsertUser(createUser);
            return Ok(user);
        }

        [HttpGet("User")]
        public ActionResult<List<User>> FindAllUser()
        {
            var users = _userDbService.FindAllUsers();
            return Ok(users);
        }

        [HttpGet("User/{userName}")]
        public ActionResult<User> FindUser(string userName)
        {
            var user = _userDbService.FindUser(userName);
            return Ok(user);
        }

        [HttpDelete("User/{userName}")]
        public ActionResult<User> DeleteUser(string userName)
        {
            var user = _userDbService.DeleteUser(userName);
            return Ok(user);
        }
    }
}
