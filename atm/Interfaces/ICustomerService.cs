using atm.Models;

namespace atm.Interfaces
{
    public interface ICustomerService
    {
        decimal GetBalance(User user);
        void Deposit(User user, decimal amount);
        void Withdraw(User user, decimal amount);
        List<string> GetTransactionHistory(User user);
    }
}