using atm.Models;

namespace atm.Interfaces
{
    public interface IAdministratorService
    {
        void AddCustomer(User user);
        void UpdateCustomer(User user);
        void DeleteCustomer(string username);
        Customer GetCustomer(string username);
    }
}