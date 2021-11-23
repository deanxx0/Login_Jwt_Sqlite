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
            try
            {
                _userDbService.CreateUsersTable();
                Console.WriteLine("Create table success");
            }
            catch
            {
                Console.WriteLine("Create table Fail");
                return NotFound("Create table Fail");
            }
            return Ok();
        }

        [HttpPost("User")]
        public ActionResult<User> CreateUser(CreateUser createUser)
        {
            User user = new();
            try
            {
                user = _userDbService.InsertUser(createUser);
                Console.WriteLine("Create user success");
            }
            catch
            {
                Console.WriteLine("Create user Fail");
                return NotFound("Create user Fail");
            }
            return Ok(user);
        }

        [HttpGet("User")]
        public ActionResult<List<User>> FindAllUser()
        {
            List<User> users = new();
            try
            {
                users = _userDbService.FindAllUsers();
                Console.WriteLine("Find all user success");
            }
            catch
            {
                Console.WriteLine("Find all user fail");
                return NotFound("Find all user fail");
            }
            return Ok(users);
        }

        [HttpGet("User/{userName}")]
        public ActionResult<User> FindUser(string userName)
        {
            User user = new();
            try
            {
                user = _userDbService.FindUser(userName);
                Console.WriteLine("Find user success");
            }
            catch
            {
                Console.WriteLine("Find user fail");
                return NotFound("Find user fail");
            }
            return Ok(user);
        }

        [HttpDelete("User/{userName}")]
        public ActionResult<User> DeleteUser(string userName)
        {
            User user = new();
            try
            {
                user = _userDbService.DeleteUser(userName);
                Console.WriteLine("Delete user success");
            }
            catch
            {
                Console.WriteLine("Delete user fail");
                return NotFound("Delete user fail");
            }
            return Ok(user);
        }
    }
}
