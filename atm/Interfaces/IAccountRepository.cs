using atm.Models;

namespace atm.Interfaces
{
    /// <summary>
    /// Defines the contract for account repository operations.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Adds a new account for a customer.
        /// </summary>
        /// <param name="customer">The customer whose account is to be added.</param>
        void AddAccount(Customer customer);

        /// <summary>
        /// Updates an existing account for a customer.
        /// </summary>
        /// <param name="customer">The customer whose account is to be updated.</param>
        void UpdateAccount(Customer customer);

        /// <summary>
        /// Deletes an account based on the account number.
        /// </summary>
        /// <param name="accountNumber">The account number of the account to delete.</param>
        void DeleteAccount(int accountNumber);

        /// <summary>
        /// Retrieves account details based on the account number.
        /// </summary>
        /// <param name="accountNumber">The account number of the account.</param>
        /// <returns>The account details, or null if not found.</returns>
        Customer GetAccount(int accountNumber);
    }
}
