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
    public class LineItemRepository : ILineItemRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of LineItemRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public LineItemRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="ILineItemRepository"/>
        /// </summary>
        /// <param name="lineItemId"><see cref="ILineItemRepository"/></param>
        /// <returns><see cref="ILineItemRepository"/></returns>
        public async Task<LineItem> GetAsync(int? lineItemId)
        {
            return await _context.LineItems.FindAsync(lineItemId);

        }

        /// <summary>
        /// <see cref="ILineItemRepository"/>
        /// </summary>
        /// <returns><see cref="ILineItemRepository"/></returns>
        public async Task<IEnumerable<LineItem>> GetAllAsync()
        {
            return await _context.LineItems.ToListAsync();

        }

        public async Task<IEnumerable<LineItem>> GetAllByJobAsync(int jobId)
        {
            return await _context.LineItems.Where(l => l.JobId == jobId).OrderBy(l => l.LineItemNumber).ToListAsync();
        }

        public async Task<int> GetMaxLineItemByJob(int jobId)
        {
            if (_context.LineItems.Where(l => l.JobId == jobId).Count() > 0)
            {
                var maxLineNumberTask = Task.Factory.StartNew(() => _context.LineItems.Where(l => l.JobId == jobId).MaxBy(l => l.LineItemNumber));
                await maxLineNumberTask;
                return maxLineNumberTask.Result.LineItemNumber;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// <see cref="ILineItemRepository"/>
        /// </summary>
        /// <param name="lineItem"><see cref="ILineItemRepository"/></param>
        /// <returns><see cref="ILineItemRepository"/></returns>
        public async Task<int> AddAsync(LineItem lineItem)
        {
            _context.LineItems.Add(lineItem);
            await _context.SaveChangesAsync();
            return lineItem.LineItemId;
        }

        /// <summary>
        /// <see cref="ILineItemRepository"/>
        /// </summary>
        /// <param name="lineItem"><see cref="ILineItemRepository"/></param>
        public async Task UpdateAsync(LineItem lineItem)
        {
            _context.Entry<LineItem>(lineItem)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="ILineItemRepository"/>
        /// </summary>
        /// <param name="lineItemId"><see cref="ILineItemRepository"/></param>
        public async Task DeleteAsync(int? lineItemId)
        {
            var lineItem = await _context.LineItems.FindAsync(lineItemId);
            if (lineItem != null)
            {
                _context.LineItems.Remove(lineItem);
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
