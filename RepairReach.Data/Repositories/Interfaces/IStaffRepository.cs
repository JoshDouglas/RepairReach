using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IStaffRepository : IDisposable, IBaseRepository
    {
        /// <summary>
        /// Get Staff by Id
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        Task<Staff> GetAsync(int? staffId);

        /// <summary>
        /// Get All Staffs
        /// </summary>
        /// <returns>List of Staffs</returns>
        Task<IEnumerable<Staff>> GetAllAsync();

        Task<IEnumerable<Staff>> GetAllTechniciansAsync();

        Task<IEnumerable<Staff>> GetAllTermAsync(string term);

        /// <summary>
        /// Add new Staff
        /// </summary>
        /// <param name="staff">Staff information</param>
        /// <returns>StaffId</returns>
        Task<int> AddAsync(Staff staff);

        /// <summary>
        /// Update Staff
        /// </summary>
        /// <param name="staff">Staff information</param>
        Task UpdateAsync(Staff staff);

        /// <summary>
        /// Delete Staff
        /// </summary>
        /// <param name="staffId">Staff to delete</param>
        Task DeleteAsync(int? staffId);

    }
}
