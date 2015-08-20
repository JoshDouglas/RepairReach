using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface ICustomerRepository : IDisposable
    {
        /// <summary>
        /// Get Customer by Id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<Customer> GetAsync(int? customerId);

        Task<Customer> GetForJobAsync(int jobId);

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns>List of Customers</returns>
        Task<IEnumerable<Customer>> GetAllAsync();

        Task<IEnumerable<Customer>> GetAllByDesignationAndNameLetterAsync(string designation, string nameLetter);

        Task<IEnumerable<Customer>> GetByDesignationAndNameLetterPagedAsync(string designation, string nameLetter,
            int pageIndex, int pageSize);

        Task<int> GetCountForDesignationAndNameLetterAsync(string designation, string nameLetter);

        /// <summary>
        /// Get All Customers by Designation
        /// </summary>
        /// <returns>List of Customers</returns>
        Task<IEnumerable<Customer>> GetAllByDesignationAsync(string designation);

        Task<IEnumerable<Customer>> GetAllBySearchAsync(string searchTerm);

        Task<IEnumerable<Customer>> GetAllIndividualByFirstLast(string firstName, string lastName);

        /// <summary>
        /// Add new Customer
        /// </summary>
        /// <param name="customer">Customer information</param>
        /// <returns>CustomerId</returns>
        Task<int> AddAsync(Customer customer);

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="customer">Customer information</param>
        Task UpdateAsync(Customer customer);

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="customerId">Customer to delete</param>
        Task DeleteAsync(int? customerId);

        Task<IEnumerable<Customer>> GetTermAsync(string term);
    }
}
