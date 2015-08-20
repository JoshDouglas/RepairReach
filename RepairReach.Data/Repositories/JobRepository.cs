using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using MoreLinq;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.Data.Extensions;

namespace RepairReach.Data.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of JobRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public JobRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IJobRepository"/>
        /// </summary>
        /// <param name="jobId"><see cref="IJobRepository"/></param>
        /// <returns><see cref="IJobRepository"/></returns>
        public async Task<Job> GetAsync(int? jobId)
        {
            return await _context.Jobs.FindAsync(jobId);

        }

        /// <summary>
        /// <see cref="IJobRepository"/>
        /// </summary>
        /// <returns><see cref="IJobRepository"/></returns>
        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            return await _context.Jobs.ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllOpenAsync()
        {
            return await _context.Jobs.Where(j=> j.JobClosed.HasValue == false).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllForCustomer(int customerId)
        {
            return await _context.Jobs.Where(j => j.CustomerId == customerId).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetNonAuthorized(DateTime? beginDate, DateTime? endDate, bool showAll)
        {
            if (showAll || beginDate.HasValue == false || endDate.HasValue == false) return await _context.Jobs.Where(j => j.JobAuthorized.HasValue == false).ToListAsync();

            beginDate = beginDate.Value.DayMin();
            endDate = endDate.Value.DayMax();

            return await _context.Jobs.Where(j => j.JobAuthorized.HasValue == false && j.JobCreated >= beginDate.Value && j.JobCreated <= endDate.Value).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllByStatusAsync(string status)
        {
            if (string.IsNullOrEmpty(status)) return await _context.Jobs.Where(j => j.JobStatus.Description.ToLower().Equals("closed") == false).ToListAsync();
            return await _context.Jobs.Where(j => j.JobStatus.Description == status).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetByStatusPagedAsync(string status, int pageIndex, int pageSize)
        {
            if (string.IsNullOrEmpty(status)) return await _context.Jobs
                .Where(j => j.JobStatus.Description.ToLower().Equals("closed") == false)
                .OrderByDescending(j => j.JobId)
                .Skip((pageIndex-1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return await _context.Jobs
                .Where(j => j.JobStatus.Description == status)
                .OrderByDescending(j => j.JobId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllByCategoryAsync(string category)
        {
            return await _context.Jobs.Where(j => j.JobCategory.Description == category).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllByJobSubTypeAsync(string jobSubType)
        {
            switch (jobSubType)
            {
                case "Not Authorized":
                    return await _context.Jobs.Where(j => j.JobAuthorized.HasValue == false).ToListAsync();
                case "Not Scheduled":
                    return await _context.Jobs.Where(j => j.JobAuthorized.HasValue && j.JobScheduled.HasValue == false).ToListAsync();
                case "All Jobs In Progress":
                    return await _context.Jobs.Where(j => j.JobAuthorized.HasValue && j.JobClosed.HasValue == false).ToListAsync();
                case "Work Started":
                    return await _context.Jobs.Where(j => j.JobAuthorized.HasValue && j.JobClosed.HasValue == false && j.JobStarted.HasValue && j.JobFinished.HasValue == false).ToListAsync();
                case "Work Finished":
                    return await _context.Jobs.Where(j => j.JobAuthorized.HasValue && j.JobClosed.HasValue == false && j.JobFinished.HasValue).ToListAsync();
                case "All Completed Jobs":
                    return await _context.Jobs.Where(j => j.JobAuthorized.HasValue && j.JobClosed.HasValue).ToListAsync();
                case "Billed":
                    return await _context.Jobs.Where(j => j.JobAuthorized.HasValue && j.JobClosed.HasValue && j.JobBilled.HasValue).ToListAsync();
                case "Not Billed":
                    return await _context.Jobs.Where(j => j.JobAuthorized.HasValue && j.JobClosed.HasValue && j.JobBilled.HasValue == false).ToListAsync();
                default:
                    return await _context.Jobs.Where(j => j.JobAuthorized.HasValue == false).ToListAsync();
            }
        }

        public async Task<IEnumerable<Job>> GetAllBySearchAsync(string searchTerm)
        {
            string searchTermLower = searchTerm.ToLower();
            int searchTermNumber = 0;
            Int32.TryParse(searchTerm, out searchTermNumber);

            return await _context.Jobs.Where(j =>
                j.ContactFirstName.ToLower().Contains(searchTermLower) ||
                j.ContactLastName.ToLower().Contains(searchTermLower) ||
                j.Address1.ToLower().Contains(searchTermLower) ||
                j.City.ToLower().Contains(searchTermLower) ||
                j.State.ToLower().Contains(searchTermLower) ||
                j.Zipcode.ToLower().Contains(searchTermLower) ||
                j.JobNumber == searchTermNumber).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllClosedAsync()
        {
            return await _context.Jobs.Where(j => j.JobStatus.Description.ToLower().Equals("closed")).ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllByStatusAndClosedAsync(string status, bool? isClosed)
        {
            ////both?
            //if (string.IsNullOrEmpty(status) == false && isClosed.HasValue)
            //{
            //    return await _context.Jobs.Where(j => j.JobStatus.Description == status && j.IsClosed == isClosed.Value).ToListAsync();
            //}

            ////closed?
            //if (isClosed.HasValue)
            //{
            //    return await _context.Jobs.Where(j => j.IsClosed == isClosed.Value).ToListAsync();
            //}

            ////status?
            //if (string.IsNullOrEmpty(status) == false)
            //{
            //    return await _context.Jobs.Where(j => j.JobStatus.Description == status).ToListAsync();
            //}

            //default - return all non-closed jobs
            //return await _context.Jobs.Where(j => j.IsClosed == false).ToListAsync();

            return await _context.Jobs.ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllRescheduleNeedsApprovalAlertsAsync()
        {
            //temp stuff for azure utc
            var oneDayAgo = DateTime.UtcNow.AddHours(-24);
            return await _context.Jobs.Where(j => (j.JobStatus.Description.ToLower().Equals("reschedule") || j.JobStatus.Description.ToLower().Equals("needs approval")) && j.LastViewedTime <= oneDayAgo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllAwaitingPaymentAlertsAsync()
        {
            //temp stuff for azure utc
            var twoWeeksAgo = DateTime.UtcNow.AddDays(-14);
            return await _context.Jobs.Where(j => j.JobStatus.Description.ToLower().Equals("awaiting payment") && j.LastViewedTime <= twoWeeksAgo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetAllOnHoldAlertsAsync()
        {
            //temp stuff for azure utc
            var twoDaysAgo = DateTime.UtcNow.AddHours(-48);
            return await _context.Jobs.Where(j => j.JobStatus.Description.ToLower().Equals("on hold") && j.LastViewedTime <= twoDaysAgo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetClosedOnDayAsync(DateTime dayClosedLocal)
        {
            var company = await _context.Companies.FirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            //TODO: may need to re-evaluate for performance
            var jobs = await _context.Jobs.Where(j => j.JobClosed.HasValue).ToListAsync();

            return jobs
                .Where(j => TimeZoneInfo.ConvertTimeFromUtc(j.JobClosed.Value, timeZoneInfo).Year == dayClosedLocal.Year
                && TimeZoneInfo.ConvertTimeFromUtc(j.JobClosed.Value, timeZoneInfo).Month == dayClosedLocal.Month
                && TimeZoneInfo.ConvertTimeFromUtc(j.JobClosed.Value, timeZoneInfo).Day == dayClosedLocal.Day).ToList();

            //return await _context.Jobs.
            //    Where(j => j.JobClosed.Value.Year == dayClosedLocal.Year
            //    && j.JobClosed.Value.Month == dayClosedLocal.Month
            //    && j.JobClosed.Value.Day == dayClosedLocal.Day)
            //    .ToListAsync();
        }

        public async Task<IEnumerable<Job>> GetClosedOnMonthYearAsync(int month, int year)
        {
            var company = await _context.Companies.FirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            //TODO: may need to re-evaluate for performance
            var jobs = await _context.Jobs.Where(j => j.JobClosed.HasValue).ToListAsync();

            return jobs
                .Where(j => TimeZoneInfo.ConvertTimeFromUtc(j.JobClosed.Value, timeZoneInfo).Year == year
                && TimeZoneInfo.ConvertTimeFromUtc(j.JobClosed.Value, timeZoneInfo).Month == month).ToList();

            //return await _context.Jobs.Where(j => j.JobClosed.Value.Month == month && j.JobClosed.Value.Year == year).ToListAsync();
        }

        public async Task<int> GetCountForStatusAsync(string status)
        {
            if (string.IsNullOrEmpty(status)) return await _context.Jobs.CountAsync(j => j.JobStatus.Description.ToLower().Equals("closed") == false);
            return await _context.Jobs.Where(j => j.JobStatus.Description == status).CountAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Jobs.Where(j => j.JobStatus.Description.ToLower().Equals("closed") == false).CountAsync();
        }

        public async Task<int> GetMaxJobNumber()
        {
            if (_context.Jobs.Count() > 0)
            {
                var maxJobNumberTask = Task.Factory.StartNew(() => _context.Jobs.MaxBy(j => j.JobNumber));
                await maxJobNumberTask;
                return maxJobNumberTask.Result.JobNumber;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// <see cref="IJobRepository"/>
        /// </summary>
        /// <param name="job"><see cref="IJobRepository"/></param>
        /// <returns><see cref="IJobRepository"/></returns>
        public async Task<int> AddAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job.JobId;
        }

        /// <summary>
        /// <see cref="IJobRepository"/>
        /// </summary>
        /// <param name="job"><see cref="IJobRepository"/></param>
        public async Task UpdateAsync(Job job)
        {
            //06.30.2014 JDD - When changing state here and grabbing the same object again in the controller it will throw an error because it thinks someone else might change the data.
            //_context.Entry<Job>(job)
            //    .State = EntityState.Modified;
            var currentJob = await _context.Jobs.FindAsync(job.JobId);
            _context.Entry(currentJob).CurrentValues.SetValues(job);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IJobRepository"/>
        /// </summary>
        /// <param name="jobId"><see cref="IJobRepository"/></param>
        public async Task DeleteAsync(int? jobId)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Dispose all resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose all resource
        /// </summary>
        /// <param name="disposing">Dispose managed resources check</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
