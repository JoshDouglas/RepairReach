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
    public class TimeClockEntryRepository : ITimeClockEntryRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of TimeClockEntryRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public TimeClockEntryRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="ITimeClockEntryRepository"/>
        /// </summary>
        /// <param name="timeClockEntryId"><see cref="ITimeClockEntryRepository"/></param>
        /// <returns><see cref="ITimeClockEntryRepository"/></returns>
        public async Task<TimeClockEntry> GetAsync(int? timeClockEntryId)
        {
            return await _context.TimeClockEntries.FindAsync(timeClockEntryId);

        }

        public async Task<TimeClockEntry> GetLastForEmployeeAsync(int staffId)
        {
            var lastEntry =
                (from t in _context.TimeClockEntries orderby t.TimeClockEntryId descending where t.StaffId == staffId select t).First();
            return await _context.TimeClockEntries.FindAsync(lastEntry.TimeClockEntryId);
        }

        public async Task<IEnumerable<TimeClockEntry>> GetAllForEmployeeAsync(int staffId)
        {
            return await _context.TimeClockEntries.Where(e => e.StaffId == staffId).ToListAsync();
        }

        /// <summary>
        /// <see cref="ITimeClockEntryRepository"/>
        /// </summary>
        /// <returns><see cref="ITimeClockEntryRepository"/></returns>
        public async Task<IEnumerable<TimeClockEntry>> GetAllAsync()
        {
            return await _context.TimeClockEntries.ToListAsync();
        }

        /// <summary>
        /// <see cref="ITimeClockEntryRepository"/>
        /// </summary>
        /// <param name="timeClockEntry"><see cref="ITimeClockEntryRepository"/></param>
        /// <returns><see cref="ITimeClockEntryRepository"/></returns>
        public async Task<int> AddAsync(TimeClockEntry timeClockEntry)
        {
            _context.TimeClockEntries.Add(timeClockEntry);
            await _context.SaveChangesAsync();
            return timeClockEntry.TimeClockEntryId;
        }

        /// <summary>
        /// <see cref="ITimeClockEntryRepository"/>
        /// </summary>
        /// <param name="timeClockEntry"><see cref="ITimeClockEntryRepository"/></param>
        public async Task UpdateAsync(TimeClockEntry timeClockEntry)
        {
            _context.Entry<TimeClockEntry>(timeClockEntry)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="ITimeClockEntryRepository"/>
        /// </summary>
        /// <param name="timeClockEntryId"><see cref="ITimeClockEntryRepository"/></param>
        public async Task DeleteAsync(int? timeClockEntryId)
        {
            var timeClockEntry = await _context.TimeClockEntries.FindAsync(timeClockEntryId);
            if (timeClockEntry != null)
            {
                _context.TimeClockEntries.Remove(timeClockEntry);
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

        public RepairReachContext GetContext()
        {
            return _context;
        }
    }
}
