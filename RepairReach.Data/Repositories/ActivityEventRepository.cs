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
    public class ActivityEventRepository : IActivityEventRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of ActivityEventRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public ActivityEventRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IActivityEventRepository"/>
        /// </summary>
        /// <param name="activityEventId"><see cref="IActivityEventRepository"/></param>
        /// <returns><see cref="IActivityEventRepository"/></returns>
        public async Task<ActivityEvent> GetAsync(int? activityEventId)
        {
            return await _context.ActivityEvents.FindAsync(activityEventId);

        }

        public async Task<IEnumerable<ActivityEvent>> GetLast10Async()
        {
            return await _context.ActivityEvents.OrderByDescending(a => a.ActivityEventId).Take(10).ToListAsync();
        }

        public async Task<IEnumerable<ActivityEvent>> GetLastXAsync(int x)
        {
            return await _context.ActivityEvents.OrderByDescending(a => a.ActivityEventId).Take(x).ToListAsync();
        }

        public async Task<IEnumerable<ActivityEvent>> GetByDateName(DateTime? startTime, DateTime? endTime, string createdBy)
        {
            //max time for end time
            if (endTime.HasValue) endTime = endTime.Value.AddDays(1).AddMilliseconds(-1);

            //gotta convert this shit to utc for azure
            if (startTime.HasValue && endTime.HasValue)
            {
                var company = await _context.Companies.FirstOrDefaultAsync();
                var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);
                startTime = TimeZoneInfo.ConvertTimeToUtc(startTime.Value, timeZoneInfo);
                endTime = TimeZoneInfo.ConvertTimeToUtc(endTime.Value, timeZoneInfo);
            }

            //both?
            if (startTime.HasValue && endTime.HasValue && string.IsNullOrEmpty(createdBy) == false)
            {
                return await _context.ActivityEvents.Where(a => a.EventTime >= startTime.Value && a.EventTime <= endTime.Value && a.CausedBy.ToLower().Contains(createdBy.ToLower())).OrderByDescending(a => a.ActivityEventId).ToListAsync();
            }
            
            //date only?
            if (startTime.HasValue && endTime.HasValue)
            {
                return await _context.ActivityEvents.Where(a => a.EventTime >= startTime.Value && a.EventTime <= endTime.Value).OrderByDescending(a => a.ActivityEventId).ToListAsync();
            }
            
            //user only?
            if (string.IsNullOrEmpty(createdBy) == false)
            {
                return await _context.ActivityEvents.Where(a => a.CausedBy.ToLower().Contains(createdBy.ToLower())).OrderByDescending(a => a.ActivityEventId).ToListAsync();
            }

            //default
            return await _context.ActivityEvents.OrderByDescending(a => a.ActivityEventId).Take(100).ToListAsync();
        }

        public async Task<IEnumerable<ActivityEvent>> GetForJobAsync(int jobId)
        {
            return await _context.ActivityEvents.Where(a => a.JobId == jobId).ToListAsync();
        }

        /// <summary>
        /// <see cref="IActivityEventRepository"/>
        /// </summary>
        /// <returns><see cref="IActivityEventRepository"/></returns>
        public async Task<IEnumerable<ActivityEvent>> GetAllAsync()
        {
            return await _context.ActivityEvents.ToListAsync();

        }

        /// <summary>
        /// <see cref="IActivityEventRepository"/>
        /// </summary>
        /// <param name="activityEvent"><see cref="IActivityEventRepository"/></param>
        /// <returns><see cref="IActivityEventRepository"/></returns>
        public async Task<int> AddAsync(ActivityEvent activityEvent)
        {
            _context.ActivityEvents.Add(activityEvent);
            await _context.SaveChangesAsync();
            return activityEvent.ActivityEventId;
        }

        /// <summary>
        /// <see cref="IActivityEventRepository"/>
        /// </summary>
        /// <param name="activityEvent"><see cref="IActivityEventRepository"/></param>
        public async Task UpdateAsync(ActivityEvent activityEvent)
        {
            _context.Entry<ActivityEvent>(activityEvent)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IActivityEventRepository"/>
        /// </summary>
        /// <param name="activityEventId"><see cref="IActivityEventRepository"/></param>
        public async Task DeleteAsync(int? activityEventId)
        {
            var activityEvent = await _context.ActivityEvents.FindAsync(activityEventId);
            if (activityEvent != null)
            {
                _context.ActivityEvents.Remove(activityEvent);
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
