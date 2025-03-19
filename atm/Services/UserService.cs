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

        public User Login(string username, string pinCode)
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


    }
}