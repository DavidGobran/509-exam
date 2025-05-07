using atm.Models;

namespace atm.Interfaces
{
    /// <summary>
    /// Defines the contract for user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user based on their username and pin code.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="pinCode">The pin code of the user.</param>
        /// <returns>The authenticated user, or null if authentication fails.</returns>
        User Login(string username, string pinCode);
    }
}