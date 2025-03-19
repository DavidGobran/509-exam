using System;
using atm.Interfaces;
using atm.Models;
using MySql.Data.MySqlClient;

namespace atm.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly string _connectionString;

        public AdministratorService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddCustomer(Customer customer)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, UserType, PinCode) VALUES (@username, @userType, @pinCode)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", customer.Username);
                cmd.Parameters.AddWithValue("@userType", customer.UserType);
                cmd.Parameters.AddWithValue("@pinCode", customer.PinCode);
                cmd.ExecuteNonQuery();

                query = "INSERT INTO Customers (Username, AccountBalance, Status, AccountHolder) VALUES (@username, @accountBalance, @status, @accountHolder)";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", customer.Username);
                cmd.Parameters.AddWithValue("@accountBalance", customer.AccountBalance);
                cmd.Parameters.AddWithValue("@status", customer.Status);
                cmd.Parameters.AddWithValue("@accountHolder", customer.AccountHolder);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET Username = @username, PinCode = @pinCode WHERE Username = (SELECT Username FROM Customers WHERE AccountNumber = @accountNumber)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@accountNumber", customer.AccountNumber);
                cmd.Parameters.AddWithValue("@username", customer.Username);
                cmd.Parameters.AddWithValue("@pinCode", customer.PinCode);
                cmd.ExecuteNonQuery();

                query = "UPDATE Customers SET Username = @username, Status = @status, AccountHolder = @accountHolder WHERE AccountNumber = @accountNumber";
                cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@accountNumber", customer.AccountNumber);
                cmd.Parameters.AddWithValue("@username", customer.Username);
                cmd.Parameters.AddWithValue("@status", customer.Status);
                cmd.Parameters.AddWithValue("@accountHolder", customer.AccountHolder);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int accountNumber)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                // Retrieve the username associated with the account number
                string query = "SELECT Username FROM Customers WHERE AccountNumber = @accountNumber";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
                string username = cmd.ExecuteScalar()?.ToString();

                if (username != null)
                {
                    // Delete the customer from the Customers table
                    query = "DELETE FROM Customers WHERE AccountNumber = @accountNumber";
                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
                    cmd.ExecuteNonQuery();

                    // Delete the user from the Users table using the retrieved username
                    query = "DELETE FROM Users WHERE Username = @username";
                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Customer GetCustomer(int accountNumber)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT u.Username, u.PinCode, c.AccountBalance, c.AccountNumber, c.Status, c.AccountHolder FROM Users u JOIN Customers c ON u.Username = c.Username WHERE c.AccountNumber = @accountNumber";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@accountNumber", accountNumber);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string username = reader["Username"].ToString();
                        string pinCode = reader["PinCode"].ToString();
                        int accountBalance = reader.GetInt32("AccountBalance");
                        string status = reader["Status"].ToString();
                        string accountHolder = reader["AccountHolder"].ToString();
                        return new Customer(username, pinCode, accountBalance, accountNumber, status, accountHolder);
                    }
                }
            }
            return null;
        }
    }
}