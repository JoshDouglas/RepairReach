using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IJobCategoryRepository : IDisposable
    {
        /// <summary>
        /// Get JobCategory by Id
        /// </summary>
        /// <param name="jobCategoryId"></param>
        /// <returns></returns>
        Task<JobCategory> GetAsync(int? jobCategoryId);

        /// <summary>
        /// Get All JobCategorys
        /// </summary>
        /// <returns>List of JobCategorys</returns>
        Task<IEnumerable<JobCategory>> GetAllAsync();

        /// <summary>
        /// Add new JobCategory
        /// </summary>
        /// <param name="jobCategory">JobCategory information</param>
        /// <returns>JobCategoryId</returns>
        Task<int> AddAsync(JobCategory jobCategory);

        /// <summary>
        /// Update JobCategory
        /// </summary>
        /// <param name="jobCategory">JobCategory information</param>
        Task UpdateAsync(JobCategory jobCategory);

        /// <summary>
        /// Delete JobCategory
        /// </summary>
        /// <param name="jobCategoryId">JobCategory to delete</param>
        Task DeleteAsync(int? jobCategoryId);

        Task<int> GetNextSequenceNumberAsync();
    }
}
