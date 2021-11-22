using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Jwt_Sqlite.Models
{
    public enum UserClaims
    {
        Member,
        Admin
    }

    public class User
    {
        public int Key { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
        public UserClaims UserClaim { get; set; }
        public DateTime CreatedAt { get; set; }

        public User()
        {

        }

        public User(int key, string userName, string password, bool remember, UserClaims userClaim, DateTime createdAt)
        {
            Key = key;
            UserName = userName;
            Password = password;
            Remember = remember;
            UserClaim = userClaim;
            CreatedAt = createdAt;
        }
    }
}
