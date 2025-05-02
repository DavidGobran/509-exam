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
        private readonly IUserRepository _userRepository;

        public UserService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Login(string username, string pinCode)
        {
            // if a repo was injected, use it (for unit tests)
            if (_userRepository != null)
                return _userRepository.GetUser(username, pinCode);

            // otherwise fall back to real‑DB logic
            using (var conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username = @username AND PinCode = @pinCode";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@pinCode", pinCode);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var userType = reader["UserType"].ToString();
                        return new User(username, userType, pinCode);
                    }
                }
            }

            return null;
        }
    }
}