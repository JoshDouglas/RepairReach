using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories.Interfaces;
using MoreLinq;

namespace RepairReach.Data.Repositories
{
    public class QuickLineItemRepository : IQuickLineItemRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of QuickLineItemRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public QuickLineItemRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IQuickLineItemRepository"/>
        /// </summary>
        /// <param name="quickLineItemId"><see cref="IQuickLineItemRepository"/></param>
        /// <returns><see cref="IQuickLineItemRepository"/></returns>
        public async Task<QuickLineItem> GetAsync(int? quickLineItemId)
        {
            return await _context.QuickLineItems.FindAsync(quickLineItemId);

        }

        /// <summary>
        /// <see cref="IQuickLineItemRepository"/>
        /// </summary>
        /// <returns><see cref="IQuickLineItemRepository"/></returns>
        public async Task<IEnumerable<QuickLineItem>> GetAllAsync()
        {
            return await _context.QuickLineItems.ToListAsync();

        }

        public async Task<IEnumerable<QuickLineItem>> GetTermAsync(string term)
        {
            return await _context.QuickLineItems.Where(s => s.Description.ToLower().Contains(term.ToLower())).ToListAsync();
        }

        /// <summary>
        /// <see cref="IQuickLineItemRepository"/>
        /// </summary>
        /// <param name="quickLineItem"><see cref="IQuickLineItemRepository"/></param>
        /// <returns><see cref="IQuickLineItemRepository"/></returns>
        public async Task<int> AddAsync(QuickLineItem quickLineItem)
        {
            _context.QuickLineItems.Add(quickLineItem);
            await _context.SaveChangesAsync();
            return quickLineItem.QuickLineItemId;
        }

        /// <summary>
        /// <see cref="IQuickLineItemRepository"/>
        /// </summary>
        /// <param name="quickLineItem"><see cref="IQuickLineItemRepository"/></param>
        public async Task UpdateAsync(QuickLineItem quickLineItem)
        {
            _context.Entry<QuickLineItem>(quickLineItem)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IQuickLineItemRepository"/>
        /// </summary>
        /// <param name="quickLineItemId"><see cref="IQuickLineItemRepository"/></param>
        public async Task DeleteAsync(int? quickLineItemId)
        {
            var quickLineItem = await _context.QuickLineItems.FindAsync(quickLineItemId);
            if (quickLineItem != null)
            {
                _context.QuickLineItems.Remove(quickLineItem);
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
