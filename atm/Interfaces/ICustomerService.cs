using atm.Models;

namespace atm.Interfaces
{
    /// <summary>
    /// Defines the contract for customer-related operations.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Retrieves the balance of a customer's account.
        /// </summary>
        /// <param name="customer">The customer whose balance is to be retrieved.</param>
        /// <returns>The account balance of the customer.</returns>
        int GetBalance(Customer customer);

        /// <summary>
        /// Deposits an amount into a customer's account.
        /// </summary>
        /// <param name="customer">The customer whose account is to be credited.</param>
        /// <param name="amount">The amount to deposit.</param>
        void Deposit(Customer customer, int amount);

        /// <summary>
        /// Withdraws an amount from a customer's account.
        /// </summary>
        /// <param name="customer">The customer whose account is to be debited.</param>
        /// <param name="amount">The amount to withdraw.</param>
        void Withdraw(Customer customer, int amount);
    }
}