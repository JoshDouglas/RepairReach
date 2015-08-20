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
    public class ServiceRepository : IServiceRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of ServiceRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public ServiceRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IServiceRepository"/>
        /// </summary>
        /// <param name="serviceId"><see cref="IServiceRepository"/></param>
        /// <returns><see cref="IServiceRepository"/></returns>
        public async Task<Service> GetAsync(int? serviceId)
        {
            return await _context.Services.FindAsync(serviceId);

        }

        /// <summary>
        /// <see cref="IServiceRepository"/>
        /// </summary>
        /// <returns><see cref="IServiceRepository"/></returns>
        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();

        }

        /// <summary>
        /// <see cref="IServiceRepository"/>
        /// </summary>
        /// <param name="service"><see cref="IServiceRepository"/></param>
        /// <returns><see cref="IServiceRepository"/></returns>
        public async Task<int> AddAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service.ServiceId;
        }

        /// <summary>
        /// <see cref="IServiceRepository"/>
        /// </summary>
        /// <param name="service"><see cref="IServiceRepository"/></param>
        public async Task UpdateAsync(Service service)
        {
            _context.Entry<Service>(service)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IServiceRepository"/>
        /// </summary>
        /// <param name="serviceId"><see cref="IServiceRepository"/></param>
        public async Task DeleteAsync(int? serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service != null)
            {
                _context.Services.Remove(service);
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
