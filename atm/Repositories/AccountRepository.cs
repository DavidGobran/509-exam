using System;
using System.Collections.Generic;
using atm.Interfaces;
using atm.Models;
using MySql.Data.MySqlClient;

namespace atm.Repositories
{
    /// <summary>
    /// Provides data access methods for customer accounts.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// The connection string for the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Adds a new account for a customer.
        /// </summary>
        /// <param name="customer">The customer whose account is to be added.</param>
        public void AddAccount(Customer customer)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Customers (Username, AccountBalance, Status, AccountHolder) VALUES (@username, @accountBalance, @status, @accountHolder)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", customer.Username);
                cmd.Parameters.AddWithValue("@accountBalance", customer.AccountBalance);
                cmd.Parameters.AddWithValue("@status", customer.Status);
                cmd.Parameters.AddWithValue("@accountHolder", customer.AccountHolder);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates an existing account for a customer.
        /// </summary>
        /// <param name="customer">The customer whose account is to be updated.</param>
        public void UpdateAccount(Customer customer)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Customers SET AccountBalance = @accountBalance, Status = @status, AccountHolder = @accountHolder WHERE Username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", customer.Username);
                cmd.Parameters.AddWithValue("@accountBalance", customer.AccountBalance);
                cmd.Parameters.AddWithValue("@status", customer.Status);
                cmd.Parameters.AddWithValue("@accountHolder", customer.AccountHolder);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes an account by account number.
        /// </summary>
        /// <param name="accountNumber">The account number of the account to be deleted.</param>
        public void DeleteAccount(int accountNumber)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Customers WHERE AccountNumber = @accountNumber";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@accountNumber", accountNumber);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Retrieves an account by account number.
        /// </summary>
        /// <param name="accountNumber">The account number of the account to be retrieved.</param>
        /// <returns>The customer account details, or null if not found.</returns>
        public Customer GetAccount(int accountNumber)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Customers WHERE AccountNumber = @accountNumber";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@accountNumber", accountNumber);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string username = reader["Username"].ToString();
                        int accountBalance = reader.GetInt32("AccountBalance");
                        string status = reader["Status"].ToString();
                        string accountHolder = reader["AccountHolder"].ToString();
                        return new Customer(username, "", accountBalance, accountNumber, status, accountHolder);
                    }
                }
            }
            return null;
        }
    }
}
