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
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of PaymentMethodRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public PaymentMethodRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IPaymentMethodRepository"/>
        /// </summary>
        /// <param name="paymentMethodId"><see cref="IPaymentMethodRepository"/></param>
        /// <returns><see cref="IPaymentMethodRepository"/></returns>
        public async Task<PaymentMethod> GetAsync(int? paymentMethodId)
        {
            return await _context.PaymentMethods.FindAsync(paymentMethodId);

        }

        /// <summary>
        /// <see cref="IPaymentMethodRepository"/>
        /// </summary>
        /// <returns><see cref="IPaymentMethodRepository"/></returns>
        public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        /// <summary>
        /// <see cref="IPaymentMethodRepository"/>
        /// </summary>
        /// <param name="paymentMethod"><see cref="IPaymentMethodRepository"/></param>
        /// <returns><see cref="IPaymentMethodRepository"/></returns>
        public async Task<int> AddAsync(PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Add(paymentMethod);
            await _context.SaveChangesAsync();
            return paymentMethod.PaymentMethodId;
        }

        /// <summary>
        /// <see cref="IPaymentMethodRepository"/>
        /// </summary>
        /// <param name="paymentMethod"><see cref="IPaymentMethodRepository"/></param>
        public async Task UpdateAsync(PaymentMethod paymentMethod)
        {
            _context.Entry<PaymentMethod>(paymentMethod)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IPaymentMethodRepository"/>
        /// </summary>
        /// <param name="paymentMethodId"><see cref="IPaymentMethodRepository"/></param>
        public async Task DeleteAsync(int? paymentMethodId)
        {
            var paymentMethod = await _context.PaymentMethods.FindAsync(paymentMethodId);
            if (paymentMethod != null)
            {
                _context.PaymentMethods.Remove(paymentMethod);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetNextSequenceNumberAsync()
        {
            var maxSequenceNumber = await (from pm in _context.PaymentMethods
                                           select (int?)pm.SequenceNumber).MaxAsync();

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
