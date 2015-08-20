using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IApplianceRepository : IDisposable
    {
        /// <summary>
        /// Get Appliance by Id
        /// </summary>
        /// <param name="applianceId"></param>
        /// <returns></returns>
        Task<Appliance> GetAsync(int? applianceId);

        /// <summary>
        /// Get All Appliances
        /// </summary>
        /// <returns>List of Appliances</returns>
        Task<IEnumerable<Appliance>> GetAllAsync();

        Task<IEnumerable<Appliance>> GetAllForJobAsync(int jobId);

        /// <summary>
        /// Add new Appliance
        /// </summary>
        /// <param name="appliance">Appliance information</param>
        /// <returns>ApplianceId</returns>
        Task<int> AddAsync(Appliance appliance);

        /// <summary>
        /// Update Appliance
        /// </summary>
        /// <param name="appliance">Appliance information</param>
        Task UpdateAsync(Appliance appliance);

        /// <summary>
        /// Delete Appliance
        /// </summary>
        /// <param name="applianceId">Appliance to delete</param>
        Task DeleteAsync(int? applianceId);
    }
}
