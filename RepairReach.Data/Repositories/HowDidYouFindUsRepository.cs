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
    public class HowDidYouFindUsRepository : IHowDidYouFindUsRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of HowDidYouFindUsRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public HowDidYouFindUsRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IHowDidYouFindUsRepository"/>
        /// </summary>
        /// <param name="howDidYouFindUsId"><see cref="IHowDidYouFindUsRepository"/></param>
        /// <returns><see cref="IHowDidYouFindUsRepository"/></returns>
        public async Task<HowDidYouFindUs> GetAsync(int? howDidYouFindUsId)
        {
            return await _context.HowDidYouFindUses.FindAsync(howDidYouFindUsId);

        }

        /// <summary>
        /// <see cref="IHowDidYouFindUsRepository"/>
        /// </summary>
        /// <returns><see cref="IHowDidYouFindUsRepository"/></returns>
        public async Task<IEnumerable<HowDidYouFindUs>> GetAllAsync()
        {
            return await _context.HowDidYouFindUses.OrderBy(j => j.SequenceNumber).ToListAsync();

        }

        /// <summary>
        /// <see cref="IHowDidYouFindUsRepository"/>
        /// </summary>
        /// <param name="howDidYouFindUs"><see cref="IHowDidYouFindUsRepository"/></param>
        /// <returns><see cref="IHowDidYouFindUsRepository"/></returns>
        public async Task<int> AddAsync(HowDidYouFindUs howDidYouFindUs)
        {
            _context.HowDidYouFindUses.Add(howDidYouFindUs);
            await _context.SaveChangesAsync();
            return howDidYouFindUs.HowDidYouFindUsId;
        }

        /// <summary>
        /// <see cref="IHowDidYouFindUsRepository"/>
        /// </summary>
        /// <param name="howDidYouFindUs"><see cref="IHowDidYouFindUsRepository"/></param>
        public async Task UpdateAsync(HowDidYouFindUs howDidYouFindUs)
        {
            _context.Entry<HowDidYouFindUs>(howDidYouFindUs)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IHowDidYouFindUsRepository"/>
        /// </summary>
        /// <param name="howDidYouFindUsId"><see cref="IHowDidYouFindUsRepository"/></param>
        public async Task DeleteAsync(int? howDidYouFindUsId)
        {
            var howDidYouFindUs = await _context.HowDidYouFindUses.FindAsync(howDidYouFindUsId);
            if (howDidYouFindUs != null)
            {
                _context.HowDidYouFindUses.Remove(howDidYouFindUs);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetNextSequenceNumberAsync()
        {
            var maxSequenceNumber = await (from h in _context.HowDidYouFindUses
                select (int?) h.SequenceNumber).MaxAsync();

            if (maxSequenceNumber.HasValue)
            {
                return maxSequenceNumber.Value + 1;
            }
            else
            {
                return 1;
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
