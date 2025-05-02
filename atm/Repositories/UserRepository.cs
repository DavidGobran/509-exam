using System;
using System.Data;
using MySql.Data.MySqlClient;
using atm.Interfaces;
using atm.Models;

namespace atm.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User GetUser(string username, string pinCode)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username = @username AND PinCode = @pinCode";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@pinCode", pinCode);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string userType = reader["UserType"].ToString();
                        return new User(username, userType, pinCode);
                    }
                }
            }
            return null;
        }

        public void AddUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, UserType, PinCode) VALUES (@username, @userType, @pinCode)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@userType", user.UserType);
                cmd.Parameters.AddWithValue("@pinCode", user.PinCode);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUser(User user)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET UserType = @userType, PinCode = @pinCode WHERE Username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@userType", user.UserType);
                cmd.Parameters.AddWithValue("@pinCode", user.PinCode);
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
