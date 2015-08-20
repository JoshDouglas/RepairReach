using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IVendorRepository : IDisposable
    {
        /// <summary>
        /// Get Vendor by Id
        /// </summary>
        /// <param name="vendorId"></param>
        /// <returns></returns>
        Task<Vendor> GetAsync(int? vendorId);

        /// <summary>
        /// Get All Vendors
        /// </summary>
        /// <returns>List of Vendors</returns>
        Task<IEnumerable<Vendor>> GetAllAsync();

        /// <summary>
        /// Add new Vendor
        /// </summary>
        /// <param name="vendor">Vendor information</param>
        /// <returns>VendorId</returns>
        Task<int> AddAsync(Vendor vendor);

        /// <summary>
        /// Update Vendor
        /// </summary>
        /// <param name="vendor">Vendor information</param>
        Task UpdateAsync(Vendor vendor);

        /// <summary>
        /// Delete Vendor
        /// </summary>
        /// <param name="vendorId">Vendor to delete</param>
        Task DeleteAsync(int? vendorId);
    }
}
