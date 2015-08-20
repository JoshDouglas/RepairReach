using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IQuickLineItemRepository : IDisposable
    {
        /// <summary>
        /// Get QuickLineItem by Id
        /// </summary>
        /// <param name="quickLineItemId"></param>
        /// <returns></returns>
        Task<QuickLineItem> GetAsync(int? quickLineItemId);

        /// <summary>
        /// Get All QuickLineItems
        /// </summary>
        /// <returns>List of QuickLineItems</returns>
        Task<IEnumerable<QuickLineItem>> GetAllAsync();

        Task<IEnumerable<QuickLineItem>> GetTermAsync(string term);

        /// <summary>
        /// Add new QuickLineItem
        /// </summary>
        /// <param name="quickLineItem">QuickLineItem information</param>
        /// <returns>QuickLineItemId</returns>
        Task<int> AddAsync(QuickLineItem quickLineItem);

        /// <summary>
        /// Update QuickLineItem
        /// </summary>
        /// <param name="quickLineItem">QuickLineItem information</param>
        Task UpdateAsync(QuickLineItem quickLineItem);

        /// <summary>
        /// Delete QuickLineItem
        /// </summary>
        /// <param name="quickLineItemId">QuickLineItem to delete</param>
        Task DeleteAsync(int? quickLineItemId);
    }
}
