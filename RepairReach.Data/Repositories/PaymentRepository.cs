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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of PaymentRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public PaymentRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IPaymentRepository"/>
        /// </summary>
        /// <param name="paymentId"><see cref="IPaymentRepository"/></param>
        /// <returns><see cref="IPaymentRepository"/></returns>
        public async Task<Payment> GetAsync(int? paymentId)
        {
            return await _context.Payments.FindAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetForJobAsync(int? jobId)
        {
            var jobIdValue = 0;
            if (jobId.HasValue) jobIdValue = jobId.Value;
            return await _context.Payments.Where(p => p.JobId == jobIdValue).ToListAsync();
        }

        /// <summary>
        /// <see cref="IPaymentRepository"/>
        /// </summary>
        /// <returns><see cref="IPaymentRepository"/></returns>
        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();

        }

        /// <summary>
        /// <see cref="IPaymentRepository"/>
        /// </summary>
        /// <param name="payment"><see cref="IPaymentRepository"/></param>
        /// <returns><see cref="IPaymentRepository"/></returns>
        public async Task<int> AddAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment.PaymentId;
        }

        /// <summary>
        /// <see cref="IPaymentRepository"/>
        /// </summary>
        /// <param name="payment"><see cref="IPaymentRepository"/></param>
        public async Task UpdateAsync(Payment payment)
        {
            //_context.Entry<Payment>(payment)
            //    .State = EntityState.Modified;
            var currentPayment = await _context.Payments.FindAsync(payment.PaymentId);
            _context.Entry(currentPayment).CurrentValues.SetValues(payment);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IPaymentRepository"/>
        /// </summary>
        /// <param name="paymentId"><see cref="IPaymentRepository"/></param>
        public async Task DeleteAsync(int? paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
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
