using atm.Models;

namespace atm.Interfaces
{
    public interface IAccountRepository
    {
        void AddAccount(Customer customer);
        void UpdateAccount(Customer customer);
        void DeleteAccount(int accountNumber);
        Customer GetAccount(int accountNumber);
    }
}
