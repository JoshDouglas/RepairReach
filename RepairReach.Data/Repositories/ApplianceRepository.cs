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
    public class ApplianceRepository : IApplianceRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of ApplianceRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public ApplianceRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IApplianceRepository"/>
        /// </summary>
        /// <param name="applianceId"><see cref="IApplianceRepository"/></param>
        /// <returns><see cref="IApplianceRepository"/></returns>
        public async Task<Appliance> GetAsync(int? applianceId)
        {
            return await _context.Appliances.FindAsync(applianceId);

        }

        /// <summary>
        /// <see cref="IApplianceRepository"/>
        /// </summary>
        /// <returns><see cref="IApplianceRepository"/></returns>
        public async Task<IEnumerable<Appliance>> GetAllAsync()
        {
            return await _context.Appliances.ToListAsync();

        }

        public async Task<IEnumerable<Appliance>> GetAllForJobAsync(int jobId)
        {
            return await _context.Appliances.Where(a => a.JobId == jobId).ToListAsync();
        }

        /// <summary>
        /// <see cref="IApplianceRepository"/>
        /// </summary>
        /// <param name="appliance"><see cref="IApplianceRepository"/></param>
        /// <returns><see cref="IApplianceRepository"/></returns>
        public async Task<int> AddAsync(Appliance appliance)
        {
            _context.Appliances.Add(appliance);
            await _context.SaveChangesAsync();
            return appliance.ApplianceId;
        }

        /// <summary>
        /// <see cref="IApplianceRepository"/>
        /// </summary>
        /// <param name="appliance"><see cref="IApplianceRepository"/></param>
        public async Task UpdateAsync(Appliance appliance)
        {
            _context.Entry<Appliance>(appliance)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IApplianceRepository"/>
        /// </summary>
        /// <param name="applianceId"><see cref="IApplianceRepository"/></param>
        public async Task DeleteAsync(int? applianceId)
        {
            var appliance = await _context.Appliances.FindAsync(applianceId);
            if (appliance != null)
            {
                _context.Appliances.Remove(appliance);
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
