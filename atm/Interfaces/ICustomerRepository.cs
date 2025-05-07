using atm.Models;

namespace atm.Interfaces
{
    /// <summary>
    /// Defines the contract for customer repository operations.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Retrieves the balance of a customer's account.
        /// </summary>
        /// <param name="username">The username of the customer.</param>
        /// <returns>The account balance of the customer.</returns>
        int GetBalance(string username);

        /// <summary>
        /// Deposits an amount into a customer's account.
        /// </summary>
        /// <param name="username">The username of the customer.</param>
        /// <param name="amount">The amount to deposit.</param>
        void Deposit(string username, int amount);

        /// <summary>
        /// Withdraws an amount from a customer's account.
        /// </summary>
        /// <param name="username">The username of the customer.</param>
        /// <param name="amount">The amount to withdraw.</param>
        void Withdraw(string username, int amount);
    }
}