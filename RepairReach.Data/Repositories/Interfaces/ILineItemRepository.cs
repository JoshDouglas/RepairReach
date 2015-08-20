using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface ILineItemRepository : IDisposable
    {
        /// <summary>
        /// Get LineItem by Id
        /// </summary>
        /// <param name="lineItemId"></param>
        /// <returns></returns>
        Task<LineItem> GetAsync(int? lineItemId);

        /// <summary>
        /// Get All LineItems
        /// </summary>
        /// <returns>List of LineItems</returns>
        Task<IEnumerable<LineItem>> GetAllAsync();

        Task<IEnumerable<LineItem>> GetAllByJobAsync(int jobId);

        Task<int> GetMaxLineItemByJob(int jobId);

        /// <summary>
        /// Add new LineItem
        /// </summary>
        /// <param name="lineItem">LineItem information</param>
        /// <returns>LineItemId</returns>
        Task<int> AddAsync(LineItem lineItem);

        /// <summary>
        /// Update LineItem
        /// </summary>
        /// <param name="lineItem">LineItem information</param>
        Task UpdateAsync(LineItem lineItem);

        /// <summary>
        /// Delete LineItem
        /// </summary>
        /// <param name="lineItemId">LineItem to delete</param>
        Task DeleteAsync(int? lineItemId);
    }
}
