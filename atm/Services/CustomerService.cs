using atm.Interfaces;
using atm.Models;
using MySql.Data.MySqlClient;

namespace atm.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly string _connectionString;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public int GetBalance(Customer customer)
        {
            if (_customerRepository != null)
                return _customerRepository.GetBalance(customer.Username);

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT AccountBalance FROM Customers WHERE Username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", customer.Username);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32("AccountBalance");
                    }
                }
            }
            return 0;
        }

        public void Deposit(Customer customer, int amount)
        {
            if (_customerRepository != null)
            {
                _customerRepository.Deposit(customer.Username, amount);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Customers SET AccountBalance = AccountBalance + @amount WHERE Username = @username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@username", customer.Username);
                cmd.ExecuteNonQuery();
            }
        }

        public void Withdraw(Customer customer, int amount)
        {
            if (_customerRepository != null)
            {
                _customerRepository.Withdraw(customer.Username, amount);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Customers SET AccountBalance = AccountBalance - @amount WHERE Username = @username AND AccountBalance >= @amount";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@username", customer.Username);
                cmd.ExecuteNonQuery();
            }
        }
    }
}