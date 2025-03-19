using atm.Models;

namespace atm.Interfaces
{
    public interface ICustomerService
    {
        int GetBalance(Customer customer);
        void Deposit(Customer customer, int amount);
        void Withdraw(Customer customer, int amount);
    }
}