using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IJobRepository : IDisposable
    {
        /// <summary>
        /// Get Job by Id
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        Task<Job> GetAsync(int? jobId);

        /// <summary>
        /// Get All Jobs
        /// </summary>
        /// <returns>List of Jobs</returns>
        Task<IEnumerable<Job>> GetAllAsync();

        Task<IEnumerable<Job>> GetAllOpenAsync();

        Task<IEnumerable<Job>> GetAllForCustomer(int customerId);

        Task<IEnumerable<Job>> GetAllByStatusAsync(string status);

        Task<IEnumerable<Job>> GetByStatusPagedAsync(string status, int pageIndex, int pageSize);

        Task<IEnumerable<Job>> GetAllByCategoryAsync(string category);

        Task<IEnumerable<Job>> GetAllByJobSubTypeAsync(string jobSubType);

        Task<IEnumerable<Job>> GetAllBySearchAsync(string searchTerm);

        Task<IEnumerable<Job>> GetAllClosedAsync();

        Task<IEnumerable<Job>> GetAllByStatusAndClosedAsync(string status, bool? isClosed);

        Task<IEnumerable<Job>> GetNonAuthorized(DateTime? beginDate, DateTime? endDate, bool showAll);

        Task<IEnumerable<Job>> GetAllRescheduleNeedsApprovalAlertsAsync();

        Task<IEnumerable<Job>> GetAllAwaitingPaymentAlertsAsync();

        Task<IEnumerable<Job>> GetAllOnHoldAlertsAsync();

        Task<IEnumerable<Job>> GetClosedOnDayAsync(DateTime dayClosed);

        Task<IEnumerable<Job>> GetClosedOnMonthYearAsync(int month, int year);

        Task<int> GetCountForStatusAsync(string status);

        Task<int> GetCountAsync();
        
        Task<int> GetMaxJobNumber();

        /// <summary>
        /// Add new Job
        /// </summary>
        /// <param name="job">Job information</param>
        /// <returns>JobId</returns>
        Task<int> AddAsync(Job job);

        /// <summary>
        /// Update Job
        /// </summary>
        /// <param name="job">Job information</param>
        Task UpdateAsync(Job job);

        /// <summary>
        /// Delete Job
        /// </summary>
        /// <param name="jobId">Job to delete</param>
        Task DeleteAsync(int? jobId);
    }
}
