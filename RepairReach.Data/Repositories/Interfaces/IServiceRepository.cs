using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IServiceRepository : IDisposable
    {
        /// <summary>
        /// Get Service by Id
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        Task<Service> GetAsync(int? serviceId);

        /// <summary>
        /// Get All Services
        /// </summary>
        /// <returns>List of Services</returns>
        Task<IEnumerable<Service>> GetAllAsync();

        /// <summary>
        /// Add new Service
        /// </summary>
        /// <param name="service">Service information</param>
        /// <returns>ServiceId</returns>
        Task<int> AddAsync(Service service);

        /// <summary>
        /// Update Service
        /// </summary>
        /// <param name="service">Service information</param>
        Task UpdateAsync(Service service);

        /// <summary>
        /// Delete Service
        /// </summary>
        /// <param name="serviceId">Service to delete</param>
        Task DeleteAsync(int? serviceId);
    }
}
