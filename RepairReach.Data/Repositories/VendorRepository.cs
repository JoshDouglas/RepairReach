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
    public class VendorRepository : IVendorRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of VendorRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public VendorRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IVendorRepository"/>
        /// </summary>
        /// <param name="vendorId"><see cref="IVendorRepository"/></param>
        /// <returns><see cref="IVendorRepository"/></returns>
        public async Task<Vendor> GetAsync(int? vendorId)
        {
            return await _context.Vendors.FindAsync(vendorId);

        }

        /// <summary>
        /// <see cref="IVendorRepository"/>
        /// </summary>
        /// <returns><see cref="IVendorRepository"/></returns>
        public async Task<IEnumerable<Vendor>> GetAllAsync()
        {
            return await _context.Vendors.ToListAsync();

        }

        /// <summary>
        /// <see cref="IVendorRepository"/>
        /// </summary>
        /// <param name="vendor"><see cref="IVendorRepository"/></param>
        /// <returns><see cref="IVendorRepository"/></returns>
        public async Task<int> AddAsync(Vendor vendor)
        {
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return vendor.VendorId;
        }

        /// <summary>
        /// <see cref="IVendorRepository"/>
        /// </summary>
        /// <param name="vendor"><see cref="IVendorRepository"/></param>
        public async Task UpdateAsync(Vendor vendor)
        {
            _context.Entry<Vendor>(vendor)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IVendorRepository"/>
        /// </summary>
        /// <param name="vendorId"><see cref="IVendorRepository"/></param>
        public async Task DeleteAsync(int? vendorId)
        {
            var vendor = await _context.Vendors.FindAsync(vendorId);
            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
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
