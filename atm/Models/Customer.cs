namespace atm.Models
{
    public class Customer : User
    {
        public decimal AccountBalance { get; set; }
        public List<string> TransactionHistory { get; private set; }

        public Customer(string username, string password, decimal accountBalance) 
            : base(username, password, "Customer")
        {
            AccountBalance = accountBalance;
            TransactionHistory = new List<string>();
        }

        public void AddTransaction(string transaction)
        {
            TransactionHistory.Add(transaction);
        }
    }
}
