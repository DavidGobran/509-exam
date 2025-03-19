namespace atm.Models
{
    public class Customer : User
    {
        public int AccountBalance { get; set; }
        public int AccountNumber { get; set; }
        public string Status { get; set; }
        public string AccountHolder { get; set; }

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
