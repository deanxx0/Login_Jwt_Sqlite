using Login_Jwt_Sqlite.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Jwt_Sqlite.Services
{
    public class UserDbService
    {
        private readonly string _userTableName = "Users";

        public void CreateUsersTable()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./AppDB.db";
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText =
                    $"CREATE TABLE {_userTableName}(Id INTEGER PRIMARY KEY AUTOINCREMENT, UserName TEXT, Password TEXT, Remember BOOL, UserClaim TEXT, CreatedAt TEXT)";
                createTableCmd.ExecuteNonQuery();
            }
            Console.WriteLine("Users Table Created!");
        }

        public void InsertUser(CreateUser createUser)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./AppDB.db";
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertUserCmd = connection.CreateCommand();
                    insertUserCmd.CommandText =
                        $"INSERT INTO {_userTableName} (UserName, Password, Remember, UserClaim, CreatedAt) VALUES('{createUser.UserName}', '{createUser.Password}', '{createUser.Remember}', '{createUser.UserClaim}', datetime('now', 'localtime'))";
                    insertUserCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
            Console.WriteLine($"User Inserted!: {createUser.UserName}, {createUser.Password}, {createUser.Remember}, {createUser.UserClaim}");
        }

        public User FindUser(string userName)
        {
            User user = new();
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./AppDB.db";
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = $"SELECT * FROM Users WHERE UserName = '{userName}'";
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var remember = false;
                        if (reader.GetString(3).ToLower() == "false")
                        {
                            remember = false;
                        }
                        if (reader.GetString(3).ToLower() == "true")
                        {
                            remember = true;
                        }

                        var userClaim = UserClaims.Member;
                        if (reader.GetString(4) == UserClaims.Member.ToString())
                        {
                            userClaim = UserClaims.Member;
                        }
                        if (reader.GetString(4) == UserClaims.Admin.ToString())
                        {
                            userClaim = UserClaims.Admin;
                        }
                        
                        user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), remember, userClaim, reader.GetDateTime(5));
                    }
                }
            }
            return user;
        }

        public void DeleteUser(string userName)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./AppDB.db";
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var deleteCmd = connection.CreateCommand();
                deleteCmd.CommandText = $"DELETE FROM {_userTableName} WHERE UserName = '{userName}'";
                deleteCmd.ExecuteNonQuery();
            }
            Console.WriteLine("User Deleted!");
        }
    }
}
