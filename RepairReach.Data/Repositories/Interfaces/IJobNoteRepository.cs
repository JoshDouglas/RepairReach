using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IJobNoteRepository : IDisposable
    {
        /// <summary>
        /// Get JobNote by Id
        /// </summary>
        /// <param name="jobNoteId"></param>
        /// <returns></returns>
        Task<JobNote> GetAsync(int? jobNoteId);

        Task<IEnumerable<JobNote>> GetForJobAsync(int jobId);

        /// <summary>
        /// Get All JobNotes
        /// </summary>
        /// <returns>List of JobNotes</returns>
        Task<IEnumerable<JobNote>> GetAllAsync();

        /// <summary>
        /// Add new JobNote
        /// </summary>
        /// <param name="jobNote">JobNote information</param>
        /// <returns>JobNoteId</returns>
        Task<int> AddAsync(JobNote jobNote);

        /// <summary>
        /// Update JobNote
        /// </summary>
        /// <param name="jobNote">JobNote information</param>
        Task UpdateAsync(JobNote jobNote);

        /// <summary>
        /// Delete JobNote
        /// </summary>
        /// <param name="jobNoteId">JobNote to delete</param>
        Task DeleteAsync(int? jobNoteId);
    }
}
