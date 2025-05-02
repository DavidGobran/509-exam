using atm.Models;

namespace atm.Interfaces
{
    public interface IAdministratorRepository
    {
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int accountNumber);
        Customer GetCustomer(int accountNumber);
    }
}