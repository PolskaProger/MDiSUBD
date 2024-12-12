using StrikeBallShop.DAL.Database;
using StrikeBallShop.DML.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace StrikeBallShop.DAL.Repositories
{
    public class UserRepository
    {
        private DBConnect database;

        public UserRepository(DBConnect database)
        {
            this.database = database;
        }

        public User GetUserById(int id)
        {
            string query = "SELECT * FROM \"User\" WHERE \"id\" = @id";
            DataTable dataTable = database.ExecuteQuery(query);
            User user = new User();
            foreach (DataRow row in dataTable.Rows)
            {
                user.id = Convert.ToInt32(row["id"]);
                user.login = row["login"].ToString();
                user.email = row["email"].ToString();
                user.passwordhash = row["passwordhash"].ToString();
                user.roleid = Convert.ToInt32(row["roleid"]);
                user.regdate = Convert.ToDateTime(row["regdate"]);
            }
            return user;
        }

        public void CreateUser(User user)
        {
            string query = "INSERT INTO \"User\" (\"login\", \"email\", \"passwordhash\", \"roleid\", \"regdate\") VALUES (@login, @email, @passwordhash, @roleid, @regdate)";
            database.ExecuteNonQuery(query);
        }

        public void UpdateUser(User user)
        {
            string query = "UPDATE \"User\" SET \"Login\" = @login, \"Email\" = @email, \"PasswordHash\" = @passwordHash, \"RoleId\" = @roleId, \"RegDate\" = @regDate WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public void DeleteUser(int id)
        {
            string query = "DELETE FROM \"User\" WHERE \"Id\" = @id";
            database.ExecuteNonQuery(query);
        }

        public List<User> GetAllUsers()
        {
            string query = "SELECT * FROM \"User\"";
            DataTable dataTable = database.ExecuteQuery(query);
            List<User> users = new List<User>();
            foreach (DataRow row in dataTable.Rows)
            {
                User user = new User
                {
                    id = Convert.ToInt32(row["Id"]),
                    login = row["Login"].ToString(),
                    email = row["Email"].ToString(),
                    passwordhash = row["PasswordHash"].ToString(),
                    roleid = Convert.ToInt32(row["RoleId"]),
                    regdate = Convert.ToDateTime(row["RegDate"])
                };
                users.Add(user);
            }
            return users;
        }

        public User AuthenticateUser(string login)
        {
            string query = "SELECT * FROM \"User\" WHERE \"Login\" = @login";
            DataTable dataTable = database.ExecuteQuery(query);
            User user = null;
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                user = new User
                {
                    id = Convert.ToInt32(row["Id"]),
                    login = row["Login"].ToString(),
                    email = row["Email"].ToString(),
                    passwordhash = row["PasswordHash"].ToString(),
                    roleid = Convert.ToInt32(row["RoleId"]),
                    regdate = Convert.ToDateTime(row["RegDate"])
                };
            }
            return user;
        }

        public void RegisterUser(User user)
        {
            string query = "INSERT INTO \"User\" (\"Login\", \"Email\", \"PasswordHash\", \"RoleId\", \"RegDate\") VALUES (@login, @email, @passwordHash, @roleId, @regDate)";
            database.ExecuteNonQuery(query);
        }
    }
}