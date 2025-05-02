using System;
using System.Collections.Generic;
using atm.Interfaces;
using atm.Models;
using MySql.Data.MySqlClient;

namespace atm.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

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
