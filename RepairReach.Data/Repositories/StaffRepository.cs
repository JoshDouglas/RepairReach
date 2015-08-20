using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Enum;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories.Interfaces;

namespace RepairReach.Data.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of StaffRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public StaffRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IStaffRepository"/>
        /// </summary>
        /// <param name="staffId"><see cref="IStaffRepository"/></param>
        /// <returns><see cref="IStaffRepository"/></returns>
        public async Task<Staff> GetAsync(int? staffId)
        {
            return await _context.Staff.FindAsync(staffId);

        }

        /// <summary>
        /// <see cref="IStaffRepository"/>
        /// </summary>
        /// <returns><see cref="IStaffRepository"/></returns>
        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await _context.Staff.Where(s => s.IsActive == true).ToListAsync();
        }

        public async Task<IEnumerable<Staff>> GetAllTechniciansAsync()
        {
            return await _context.Staff.Where(s => s.IsActive == true && s.UserTitle == UserTitleEnum.Technician).ToListAsync();
        }

        public async Task<IEnumerable<Staff>> GetAllTermAsync(string term)
        {
            return await _context.Staff.Where(s => s.IsActive == true && s.DisplayName.ToLower().Contains(term.ToLower())).ToListAsync();
        }

        /// <summary>
        /// <see cref="IStaffRepository"/>
        /// </summary>
        /// <param name="staff"><see cref="IStaffRepository"/></param>
        /// <returns><see cref="IStaffRepository"/></returns>
        public async Task<int> AddAsync(Staff staff)
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();
            return staff.StaffId;
        }

        /// <summary>
        /// <see cref="IStaffRepository"/>
        /// </summary>
        /// <param name="staff"><see cref="IStaffRepository"/></param>
        public async Task UpdateAsync(Staff staff)
        {
            _context.Entry<Staff>(staff)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IStaffRepository"/>
        /// </summary>
        /// <param name="staffId"><see cref="IStaffRepository"/></param>
        public async Task DeleteAsync(int? staffId)
        {
            var staff = await _context.Staff.FindAsync(staffId);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
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
