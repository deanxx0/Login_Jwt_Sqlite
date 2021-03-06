using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Jwt_Sqlite.Models
{
    public class CreateUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserClaims UserClaim { get; set; }

        public CreateUser(string userName, string password, UserClaims userClaim)
        {
            UserName = userName;
            Password = password;
            UserClaim = userClaim;
        }
    }
}
