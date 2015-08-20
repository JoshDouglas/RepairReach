using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface ITaxRateRepository : IDisposable
    {
        /// <summary>
        /// Get TaxRate by Id
        /// </summary>
        /// <param name="taxRateId"></param>
        /// <returns></returns>
        Task<TaxRate> GetAsync(int? taxRateId);

        Task<TaxRate> GetDefaultRateAsync();

        /// <summary>
        /// Get All TaxRates
        /// </summary>
        /// <returns>List of TaxRates</returns>
        Task<IEnumerable<TaxRate>> GetAllAsync();

        /// <summary>
        /// Add new TaxRate
        /// </summary>
        /// <param name="taxRate">TaxRate information</param>
        /// <returns>TaxRateId</returns>
        Task<int> AddAsync(TaxRate taxRate);

        /// <summary>
        /// Update TaxRate
        /// </summary>
        /// <param name="taxRate">TaxRate information</param>
        Task UpdateAsync(TaxRate taxRate);

        /// <summary>
        /// Delete TaxRate
        /// </summary>
        /// <param name="taxRateId">TaxRate to delete</param>
        Task DeleteAsync(int? taxRateId);
    }
}
