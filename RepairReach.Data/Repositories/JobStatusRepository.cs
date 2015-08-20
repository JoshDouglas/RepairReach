using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories.Interfaces;

namespace RepairReach.Data.Repositories
{
    public class JobStatusRepository : IJobStatusRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of JobStatusRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public JobStatusRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IJobStatusRepository"/>
        /// </summary>
        /// <param name="jobStatusId"><see cref="IJobStatusRepository"/></param>
        /// <returns><see cref="IJobStatusRepository"/></returns>
        public async Task<JobStatus> GetAsync(int? jobStatusId)
        {
            return await _context.JobStatuses.FindAsync(jobStatusId);

        }

        /// <summary>
        /// <see cref="IJobStatusRepository"/>
        /// </summary>
        /// <returns><see cref="IJobStatusRepository"/></returns>
        public async Task<IEnumerable<JobStatus>> GetAllAsync()
        {
            return await _context.JobStatuses.OrderBy(j => j.SequenceNumber).ToListAsync();

        }

        /// <summary>
        /// <see cref="IJobStatusRepository"/>
        /// </summary>
        /// <param name="jobStatus"><see cref="IJobStatusRepository"/></param>
        /// <returns><see cref="IJobStatusRepository"/></returns>
        public async Task<int> AddAsync(JobStatus jobStatus)
        {
            _context.JobStatuses.Add(jobStatus);
            await _context.SaveChangesAsync();
            return jobStatus.JobStatusId;
        }

        /// <summary>
        /// <see cref="IJobStatusRepository"/>
        /// </summary>
        /// <param name="jobStatus"><see cref="IJobStatusRepository"/></param>
        public async Task UpdateAsync(JobStatus jobStatus)
        {
            _context.Entry<JobStatus>(jobStatus)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IJobStatusRepository"/>
        /// </summary>
        /// <param name="jobStatusId"><see cref="IJobStatusRepository"/></param>
        public async Task DeleteAsync(int? jobStatusId)
        {
            var jobStatus = await _context.JobStatuses.FindAsync(jobStatusId);
            if (jobStatus != null)
            {
                _context.JobStatuses.Remove(jobStatus);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetNextSequenceNumberAsync()
        {
            var maxSequenceNumber = await (from js in _context.JobStatuses
                select (int?) js.SequenceNumber).MaxAsync();

            if (maxSequenceNumber.HasValue)
            {
                return maxSequenceNumber.Value + 1;
            }
            else
            {
                return 1;
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
