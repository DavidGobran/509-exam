using System;
using System.Data;
using MySql.Data.MySqlClient;
using atm.Interfaces;
using atm.Models;

namespace atm.Services
{
    public class UserService : IUserService
    {
        private readonly string _connectionString;

        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User Login(string username, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string userType = reader["UserType"].ToString();
                        return new User(username, password, userType);
                    }
                }
            }
            return null;
        }

        public void Register(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, Password, UserType) VALUES (@username, @password, @userType)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@userType", user.UserType);
                cmd.ExecuteNonQuery();
            }
        }

        public User GetUser(string username)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string password = reader["Password"].ToString();
                        string userType = reader["UserType"].ToString();
                        return new User(username, password, userType);
                    }
                }
            }
            return null;
        }

        public void UpdateUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET Password = @password, UserType = @userType WHERE Username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@userType", user.UserType);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(string username)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Users WHERE Username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
