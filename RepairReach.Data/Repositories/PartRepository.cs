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
    public class PartRepository : IPartRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of PartRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public PartRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IPartRepository"/>
        /// </summary>
        /// <param name="partId"><see cref="IPartRepository"/></param>
        /// <returns><see cref="IPartRepository"/></returns>
        public async Task<Part> GetAsync(int? partId)
        {
            return await _context.Parts.FindAsync(partId);

        }

        /// <summary>
        /// <see cref="IPartRepository"/>
        /// </summary>
        /// <returns><see cref="IPartRepository"/></returns>
        public async Task<IEnumerable<Part>> GetAllAsync()
        {
            return await _context.Parts.ToListAsync();

        }

        /// <summary>
        /// <see cref="IPartRepository"/>
        /// </summary>
        /// <param name="part"><see cref="IPartRepository"/></param>
        /// <returns><see cref="IPartRepository"/></returns>
        public async Task<int> AddAsync(Part part)
        {
            _context.Parts.Add(part);
            await _context.SaveChangesAsync();
            return part.PartId;
        }

        /// <summary>
        /// <see cref="IPartRepository"/>
        /// </summary>
        /// <param name="part"><see cref="IPartRepository"/></param>
        public async Task UpdateAsync(Part part)
        {
            _context.Entry<Part>(part)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IPartRepository"/>
        /// </summary>
        /// <param name="partId"><see cref="IPartRepository"/></param>
        public async Task DeleteAsync(int? partId)
        {
            var part = await _context.Parts.FindAsync(partId);
            if (part != null)
            {
                _context.Parts.Remove(part);
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
