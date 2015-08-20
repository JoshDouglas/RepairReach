using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IHowDidYouFindUsRepository : IDisposable
    {
        /// <summary>
        /// Get HowDidYouFindUs by Id
        /// </summary>
        /// <param name="howDidYouFindUsId"></param>
        /// <returns></returns>
        Task<HowDidYouFindUs> GetAsync(int? howDidYouFindUsId);

        /// <summary>
        /// Get All HowDidYouFindUss
        /// </summary>
        /// <returns>List of HowDidYouFindUss</returns>
        Task<IEnumerable<HowDidYouFindUs>> GetAllAsync();

        /// <summary>
        /// Add new HowDidYouFindUs
        /// </summary>
        /// <param name="howDidYouFindUs">HowDidYouFindUs information</param>
        /// <returns>HowDidYouFindUsId</returns>
        Task<int> AddAsync(HowDidYouFindUs howDidYouFindUs);

        /// <summary>
        /// Update HowDidYouFindUs
        /// </summary>
        /// <param name="howDidYouFindUs">HowDidYouFindUs information</param>
        Task UpdateAsync(HowDidYouFindUs howDidYouFindUs);

        /// <summary>
        /// Delete HowDidYouFindUs
        /// </summary>
        /// <param name="howDidYouFindUsId">HowDidYouFindUs to delete</param>
        Task DeleteAsync(int? howDidYouFindUsId);

        Task<int> GetNextSequenceNumberAsync();
    }
}
