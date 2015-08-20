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
    public class JobNoteRepository : IJobNoteRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of JobNoteRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public JobNoteRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IJobNoteRepository"/>
        /// </summary>
        /// <param name="jobNoteId"><see cref="IJobNoteRepository"/></param>
        /// <returns><see cref="IJobNoteRepository"/></returns>
        public async Task<JobNote> GetAsync(int? jobNoteId)
        {
            return await _context.JobNotes.FindAsync(jobNoteId);

        }

        public async Task<IEnumerable<JobNote>> GetForJobAsync(int jobId)
        {
            return await _context.JobNotes.Where(j => j.JobId == jobId).ToListAsync();
        }

        /// <summary>
        /// <see cref="IJobNoteRepository"/>
        /// </summary>
        /// <returns><see cref="IJobNoteRepository"/></returns>
        public async Task<IEnumerable<JobNote>> GetAllAsync()
        {
            return await _context.JobNotes.ToListAsync();

        }

        /// <summary>
        /// <see cref="IJobNoteRepository"/>
        /// </summary>
        /// <param name="jobNote"><see cref="IJobNoteRepository"/></param>
        /// <returns><see cref="IJobNoteRepository"/></returns>
        public async Task<int> AddAsync(JobNote jobNote)
        {
            _context.JobNotes.Add(jobNote);
            await _context.SaveChangesAsync();
            return jobNote.JobNoteId;
        }

        /// <summary>
        /// <see cref="IJobNoteRepository"/>
        /// </summary>
        /// <param name="jobNote"><see cref="IJobNoteRepository"/></param>
        public async Task UpdateAsync(JobNote jobNote)
        {
            _context.Entry<JobNote>(jobNote)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IJobNoteRepository"/>
        /// </summary>
        /// <param name="jobNoteId"><see cref="IJobNoteRepository"/></param>
        public async Task DeleteAsync(int? jobNoteId)
        {
            var jobNote = await _context.JobNotes.FindAsync(jobNoteId);
            if (jobNote != null)
            {
                _context.JobNotes.Remove(jobNote);
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
