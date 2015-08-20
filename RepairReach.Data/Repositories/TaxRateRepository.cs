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
    public class TaxRateRepository : ITaxRateRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of TaxRateRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public TaxRateRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="ITaxRateRepository"/>
        /// </summary>
        /// <param name="taxRateId"><see cref="ITaxRateRepository"/></param>
        /// <returns><see cref="ITaxRateRepository"/></returns>
        public async Task<TaxRate> GetAsync(int? taxRateId)
        {
            return await _context.TaxRates.FindAsync(taxRateId);

        }

        public async Task<TaxRate> GetDefaultRateAsync()
        {
            //get primary key for default rate
            if (_context.TaxRates.Where(t => t.IsDefaultRate == true).Count() > 0)
            {
                var defaultTaxRates = await _context.TaxRates.Where(t => t.IsDefaultRate == true).ToListAsync();
                return defaultTaxRates[0];
            }
            return new TaxRate();
        }

        /// <summary>
        /// <see cref="ITaxRateRepository"/>
        /// </summary>
        /// <returns><see cref="ITaxRateRepository"/></returns>
        public async Task<IEnumerable<TaxRate>> GetAllAsync()
        {
            return await _context.TaxRates.ToListAsync();

        }

        /// <summary>
        /// <see cref="ITaxRateRepository"/>
        /// </summary>
        /// <param name="taxRate"><see cref="ITaxRateRepository"/></param>
        /// <returns><see cref="ITaxRateRepository"/></returns>
        public async Task<int> AddAsync(TaxRate taxRate)
        {
            _context.TaxRates.Add(taxRate);
            await _context.SaveChangesAsync();
            return taxRate.TaxRateId;
        }

        /// <summary>
        /// <see cref="ITaxRateRepository"/>
        /// </summary>
        /// <param name="taxRate"><see cref="ITaxRateRepository"/></param>
        public async Task UpdateAsync(TaxRate taxRate)
        {
            _context.Entry<TaxRate>(taxRate)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="ITaxRateRepository"/>
        /// </summary>
        /// <param name="taxRateId"><see cref="ITaxRateRepository"/></param>
        public async Task DeleteAsync(int? taxRateId)
        {
            var taxRate = await _context.TaxRates.FindAsync(taxRateId);
            if (taxRate != null)
            {
                _context.TaxRates.Remove(taxRate);
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
