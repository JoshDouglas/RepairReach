using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IActivityEventRepository : IDisposable
    {
        /// <summary>
        /// Get ActivityEvent by Id
        /// </summary>
        /// <param name="activityEventId"></param>
        /// <returns></returns>
        Task<ActivityEvent> GetAsync(int? activityEventId);

        /// <summary>
        /// Get All ActivityEvents
        /// </summary>
        /// <returns>List of ActivityEvents</returns>
        Task<IEnumerable<ActivityEvent>> GetAllAsync();

        Task<IEnumerable<ActivityEvent>> GetLast10Async();

        Task<IEnumerable<ActivityEvent>> GetLastXAsync(int x);

        Task<IEnumerable<ActivityEvent>> GetByDateName(DateTime? startTime, DateTime? endTime, string createdBy);

        Task<IEnumerable<ActivityEvent>> GetForJobAsync(int jobId);

        /// <summary>
        /// Add new ActivityEvent
        /// </summary>
        /// <param name="activityEvent">ActivityEvent information</param>
        /// <returns>ActivityEventId</returns>
        Task<int> AddAsync(ActivityEvent activityEvent);

        /// <summary>
        /// Update ActivityEvent
        /// </summary>
        /// <param name="activityEvent">ActivityEvent information</param>
        Task UpdateAsync(ActivityEvent activityEvent);

        /// <summary>
        /// Delete ActivityEvent
        /// </summary>
        /// <param name="activityEventId">ActivityEvent to delete</param>
        Task DeleteAsync(int? activityEventId);
    }
}
