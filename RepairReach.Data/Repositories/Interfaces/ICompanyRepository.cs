using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface ICompanyRepository : IDisposable
    {
        /// <summary>
        /// Get Company by Id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<Company> GetAsync(int? companyId);

        /// <summary>
        /// Get Company
        /// </summary>
        /// <returns></returns>
        Task<Company> GetFirstAsync();

        /// <summary>
        /// Get All Companys
        /// </summary>
        /// <returns>List of Companys</returns>
        Task<IEnumerable<Company>> GetAllAsync();

        /// <summary>
        /// Add new Company
        /// </summary>
        /// <param name="company">Company information</param>
        /// <returns>CompanyId</returns>
        Task<int> AddAsync(Company company);

        /// <summary>
        /// Update Company
        /// </summary>
        /// <param name="company">Company information</param>
        Task UpdateAsync(Company company);

        /// <summary>
        /// Delete Company
        /// </summary>
        /// <param name="companyId">Company to delete</param>
        Task DeleteAsync(int? companyId);
    }
}
