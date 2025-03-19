using atm.Models;

namespace atm.Interfaces
{
    public interface IUserService
    {
        User Login(string username, string pinCode);
    }
}