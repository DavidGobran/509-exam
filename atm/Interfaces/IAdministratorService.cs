using atm.Models;

namespace atm.Interfaces
{
    /// <summary>
    /// Defines the contract for administrator-related operations.
    /// </summary>
    public interface IAdministratorService
    {
        /// <summary>
        /// Adds a new customer to the system.
        /// </summary>
        /// <param name="customer">The customer to add.</param>
        void AddCustomer(Customer customer);

        /// <summary>
        /// Updates an existing customer's details.
        /// </summary>
        /// <param name="customer">The customer to update.</param>
        void UpdateCustomer(Customer customer);

        /// <summary>
        /// Deletes a customer from the system.
        /// </summary>
        /// <param name="accountNumber">The account number of the customer to delete.</param>
        void DeleteCustomer(int accountNumber);

        /// <summary>
        /// Retrieves a customer's details based on their account number.
        /// </summary>
        /// <param name="accountNumber">The account number of the customer.</param>
        /// <returns>The customer details, or null if not found.</returns>
        Customer GetCustomer(int accountNumber);
    }
}