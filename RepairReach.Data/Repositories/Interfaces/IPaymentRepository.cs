using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IPaymentRepository : IDisposable
    {
        /// <summary>
        /// Get Payment by Id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<Payment> GetAsync(int? paymentId);

        /// <summary>
        /// Get All Payments
        /// </summary>
        /// <returns>List of Payments</returns>
        Task<IEnumerable<Payment>> GetAllAsync();

        Task<IEnumerable<Payment>> GetForJobAsync(int? jobId);

        /// <summary>
        /// Add new Payment
        /// </summary>
        /// <param name="payment">Payment information</param>
        /// <returns>PaymentId</returns>
        Task<int> AddAsync(Payment payment);

        /// <summary>
        /// Update Payment
        /// </summary>
        /// <param name="payment">Payment information</param>
        Task UpdateAsync(Payment payment);

        /// <summary>
        /// Delete Payment
        /// </summary>
        /// <param name="paymentId">Payment to delete</param>
        Task DeleteAsync(int? paymentId);
    }
}
