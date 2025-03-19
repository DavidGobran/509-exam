using atm.Models;

namespace atm.Interfaces
{
    public interface IUserService
    {
        User Login(string username, string password);
        void Register(User user);
        User GetUser(string username);
        void UpdateUser(User user);
        void DeleteUser(string username);
    }
}