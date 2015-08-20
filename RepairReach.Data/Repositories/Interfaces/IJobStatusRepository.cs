using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IJobStatusRepository : IDisposable
    {
        /// <summary>
        /// Get JobStatus by Id
        /// </summary>
        /// <param name="jobStatusId"></param>
        /// <returns></returns>
        Task<JobStatus> GetAsync(int? jobStatusId);

        /// <summary>
        /// Get All JobStatuss
        /// </summary>
        /// <returns>List of JobStatuss</returns>
        Task<IEnumerable<JobStatus>> GetAllAsync();

        /// <summary>
        /// Add new JobStatus
        /// </summary>
        /// <param name="jobStatus">JobStatus information</param>
        /// <returns>JobStatusId</returns>
        Task<int> AddAsync(JobStatus jobStatus);

        /// <summary>
        /// Update JobStatus
        /// </summary>
        /// <param name="jobStatus">JobStatus information</param>
        Task UpdateAsync(JobStatus jobStatus);

        /// <summary>
        /// Delete JobStatus
        /// </summary>
        /// <param name="jobStatusId">JobStatus to delete</param>
        Task DeleteAsync(int? jobStatusId);

        Task<int> GetNextSequenceNumberAsync();
    }
}
