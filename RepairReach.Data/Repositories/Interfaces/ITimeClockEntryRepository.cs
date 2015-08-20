using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface ITimeClockEntryRepository : IDisposable
    {
        /// <summary>
        /// Get TimeClockEntry by Id
        /// </summary>
        /// <param name="timeClockEntryId"></param>
        /// <returns></returns>
        Task<TimeClockEntry> GetAsync(int? timeClockEntryId);

        Task<TimeClockEntry> GetLastForEmployeeAsync(int staffId);

        /// <summary>
        /// Get All TimeClockEntrys
        /// </summary>
        /// <returns>List of TimeClockEntrys</returns>
        Task<IEnumerable<TimeClockEntry>> GetAllAsync();

        Task<IEnumerable<TimeClockEntry>> GetAllForEmployeeAsync(int staffId);

        /// <summary>
        /// Add new TimeClockEntry
        /// </summary>
        /// <param name="timeClockEntry">TimeClockEntry information</param>
        /// <returns>TimeClockEntryId</returns>
        Task<int> AddAsync(TimeClockEntry timeClockEntry);

        /// <summary>
        /// Update TimeClockEntry
        /// </summary>
        /// <param name="timeClockEntry">TimeClockEntry information</param>
        Task UpdateAsync(TimeClockEntry timeClockEntry);

        /// <summary>
        /// Delete TimeClockEntry
        /// </summary>
        /// <param name="timeClockEntryId">TimeClockEntry to delete</param>
        Task DeleteAsync(int? timeClockEntryId);

        RepairReachContext GetContext();
    }
}
