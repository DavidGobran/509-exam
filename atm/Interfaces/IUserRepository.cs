using atm.Models;

namespace atm.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(string username, string pinCode);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string username);
    }
}
