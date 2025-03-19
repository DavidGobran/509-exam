using atm.Models;

namespace atm.Interfaces
{
    public interface IAdministratorService
    {
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int accountNumber);
        Customer GetCustomer(int accountNumber);
    }
}