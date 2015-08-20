using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IPartRepository : IDisposable
    {
        /// <summary>
        /// Get Part by Id
        /// </summary>
        /// <param name="partId"></param>
        /// <returns></returns>
        Task<Part> GetAsync(int? partId);

        /// <summary>
        /// Get All Parts
        /// </summary>
        /// <returns>List of Parts</returns>
        Task<IEnumerable<Part>> GetAllAsync();

        /// <summary>
        /// Add new Part
        /// </summary>
        /// <param name="part">Part information</param>
        /// <returns>PartId</returns>
        Task<int> AddAsync(Part part);

        /// <summary>
        /// Update Part
        /// </summary>
        /// <param name="part">Part information</param>
        Task UpdateAsync(Part part);

        /// <summary>
        /// Delete Part
        /// </summary>
        /// <param name="partId">Part to delete</param>
        Task DeleteAsync(int? partId);
    }
}
