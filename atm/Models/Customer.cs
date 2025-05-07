namespace atm.Models
{
    /// <summary>
    /// Represents a customer in the system.
    /// </summary>
    public class Customer : User
    {
        /// <summary>
        /// Gets or sets the account balance of the customer.
        /// </summary>
        public int AccountBalance { get; set; }

        /// <summary>
        /// Gets or sets the account number of the customer.
        /// </summary>
        public int AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the status of the customer's account.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the account holder's name.
        /// </summary>
        public string AccountHolder { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        /// <param name="username">The username of the customer.</param>
        /// <param name="pinCode">The pin code of the customer.</param>
        /// <param name="accountBalance">The account balance of the customer.</param>
        /// <param name="accountNumber">The account number of the customer.</param>
        /// <param name="status">The status of the customer's account.</param>
        /// <param name="accountHolder">The account holder's name.</param>
        public Customer(string username, string pinCode, int accountBalance, int accountNumber, string status, string accountHolder) 
            : base(username, "Customer", pinCode)
        {
            AccountBalance = accountBalance;
            AccountNumber = accountNumber;
            Status = status;
            AccountHolder = accountHolder;
        }
    }
}
