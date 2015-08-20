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
    public class JobCategoryRepository : IJobCategoryRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of JobCategoryRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public JobCategoryRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IJobCategoryRepository"/>
        /// </summary>
        /// <param name="jobCategoryId"><see cref="IJobCategoryRepository"/></param>
        /// <returns><see cref="IJobCategoryRepository"/></returns>
        public async Task<JobCategory> GetAsync(int? jobCategoryId)
        {
            return await _context.JobCategories.FindAsync(jobCategoryId);

        }

        /// <summary>
        /// <see cref="IJobCategoryRepository"/>
        /// </summary>
        /// <returns><see cref="IJobCategoryRepository"/></returns>
        public async Task<IEnumerable<JobCategory>> GetAllAsync()
        {
            return await _context.JobCategories.ToListAsync();
        }

        /// <summary>
        /// <see cref="IJobCategoryRepository"/>
        /// </summary>
        /// <param name="jobCategory"><see cref="IJobCategoryRepository"/></param>
        /// <returns><see cref="IJobCategoryRepository"/></returns>
        public async Task<int> AddAsync(JobCategory jobCategory)
        {
            _context.JobCategories.Add(jobCategory);
            await _context.SaveChangesAsync();
            return jobCategory.JobCategoryId;
        }

        /// <summary>
        /// <see cref="IJobCategoryRepository"/>
        /// </summary>
        /// <param name="jobCategory"><see cref="IJobCategoryRepository"/></param>
        public async Task UpdateAsync(JobCategory jobCategory)
        {
            _context.Entry<JobCategory>(jobCategory)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IJobCategoryRepository"/>
        /// </summary>
        /// <param name="jobCategoryId"><see cref="IJobCategoryRepository"/></param>
        public async Task DeleteAsync(int? jobCategoryId)
        {
            var jobCategory = await _context.JobCategories.FindAsync(jobCategoryId);
            if (jobCategory != null)
            {
                _context.JobCategories.Remove(jobCategory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetNextSequenceNumberAsync()
        {
            var maxSequenceNumber = await (from js in _context.JobCategories
                                           select (int?)js.SequenceNumber).MaxAsync();

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
