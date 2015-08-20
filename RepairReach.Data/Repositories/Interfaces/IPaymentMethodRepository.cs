using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IPaymentMethodRepository : IDisposable
    {
        /// <summary>
        /// Get PaymentMethod by Id
        /// </summary>
        /// <param name="paymentMethodId"></param>
        /// <returns></returns>
        Task<PaymentMethod> GetAsync(int? paymentMethodId);

        /// <summary>
        /// Get All PaymentMethods
        /// </summary>
        /// <returns>List of PaymentMethods</returns>
        Task<IEnumerable<PaymentMethod>> GetAllAsync();

        /// <summary>
        /// Add new PaymentMethod
        /// </summary>
        /// <param name="paymentMethod">PaymentMethod information</param>
        /// <returns>PaymentMethodId</returns>
        Task<int> AddAsync(PaymentMethod paymentMethod);

        /// <summary>
        /// Update PaymentMethod
        /// </summary>
        /// <param name="paymentMethod">PaymentMethod information</param>
        Task UpdateAsync(PaymentMethod paymentMethod);

        /// <summary>
        /// Delete PaymentMethod
        /// </summary>
        /// <param name="paymentMethodId">PaymentMethod to delete</param>
        Task DeleteAsync(int? paymentMethodId);

        Task<int> GetNextSequenceNumberAsync();
    }
}
