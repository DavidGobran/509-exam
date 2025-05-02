using atm.Models;

namespace atm.Interfaces
{
    public interface ICustomerRepository
    {
        int GetBalance(string username);
        void Deposit(string username, int amount);
        void Withdraw(string username, int amount);
    }
}