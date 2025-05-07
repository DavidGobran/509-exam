using atm.Models;

namespace atm.Interfaces
{
    /// <summary>
    /// Defines the contract for user repository operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user based on their username and pin code.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="pinCode">The pin code of the user.</param>
        /// <returns>The user if found, or null otherwise.</returns>
        User GetUser(string username, string pinCode);

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        /// <param name="user">The user to add.</param>
        void AddUser(User user);

        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        /// <param name="user">The user to update.</param>
        void UpdateUser(User user);

        /// <summary>
        /// Deletes a user from the repository.
        /// </summary>
        /// <param name="username">The username of the user to delete.</param>
        void DeleteUser(string username);
    }
}
