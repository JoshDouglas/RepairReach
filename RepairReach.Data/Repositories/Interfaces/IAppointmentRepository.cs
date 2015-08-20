using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepairReach.Core.Model;

namespace RepairReach.Data.Repositories.Interfaces
{
    public interface IAppointmentRepository : IDisposable
    {
        /// <summary>
        /// Get Appointment by Id
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        Task<Appointment> GetAsync(int? appointmentId);

        /// <summary>
        /// Get All Appointments
        /// </summary>
        /// <returns>List of Appointments</returns>
        Task<IEnumerable<Appointment>> GetAllAsync();

        Task<IEnumerable<Appointment>> GetForJobAsync(int jobId);

        Task<IEnumerable<Appointment>> GetAllTodayAsync();

        Task<IEnumerable<Appointment>> GetAllUpcomingAsync();

        Task<IEnumerable<Appointment>> GetAllUpcomingFromTodayAsync();

        Task<IEnumerable<Appointment>> GetAllPastDueAsync();

        Task<IEnumerable<Appointment>> GetAllDateRangeUTCAsync(DateTime start, DateTime end);

        Task<IEnumerable<Appointment>> GetAllDateRangeLocalAsync(DateTime start, DateTime end);

        /// <summary>
        /// Add new Appointment
        /// </summary>
        /// <param name="appointment">Appointment information</param>
        /// <returns>AppointmentId</returns>
        Task<int> AddAsync(Appointment appointment);

        /// <summary>
        /// Update Appointment
        /// </summary>
        /// <param name="appointment">Appointment information</param>
        Task UpdateAsync(Appointment appointment);

        /// <summary>
        /// Delete Appointment
        /// </summary>
        /// <param name="appointmentId">Appointment to delete</param>
        Task DeleteAsync(int? appointmentId);
    }
}
