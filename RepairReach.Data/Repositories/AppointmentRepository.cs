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
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of AppointmentRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public AppointmentRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="IAppointmentRepository"/>
        /// </summary>
        /// <param name="appointmentId"><see cref="IAppointmentRepository"/></param>
        /// <returns><see cref="IAppointmentRepository"/></returns>
        public async Task<Appointment> GetAsync(int? appointmentId)
        {
            return await _context.Appointments.FindAsync(appointmentId);

        }

        public async Task<IEnumerable<Appointment>> GetForJobAsync(int jobId)
        {
            return await _context.Appointments.Where(a => a.JobId == jobId).ToListAsync();
        }

        /// <summary>
        /// <see cref="IAppointmentRepository"/>
        /// </summary>
        /// <returns><see cref="IAppointmentRepository"/></returns>
        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments.ToListAsync();

        }

        public async Task<IEnumerable<Appointment>> GetAllTodayAsync()
        {
            //azure utc stuff
            var company = await _context.Companies.FirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            //var appointments = await _context.Appointments.Where(a => a.IsCompleted == false).ToListAsync();
            var appointments = await _context.Appointments.ToListAsync();
            return appointments.Where(a => TimeZoneInfo.ConvertTimeFromUtc(a.StartTime, timeZoneInfo).Date == TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo).Date).OrderBy(a => a.StartTime);
        }

        public async Task<IEnumerable<Appointment>> GetAllUpcomingAsync()
        {
            //azure utc stuff
            var company = await _context.Companies.FirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            //var appointments = await _context.Appointments.Where(a => a.IsCompleted == false).ToListAsync();
            var appointments = await _context.Appointments.ToListAsync();
            return appointments.Where(a => (TimeZoneInfo.ConvertTimeFromUtc(a.StartTime, timeZoneInfo).Date - TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo).Date).Days == 1).OrderBy(a => a.StartTime);
        }

        public async Task<IEnumerable<Appointment>> GetAllUpcomingFromTodayAsync()
        {
            var company = await _context.Companies.FirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            //var appointments = await _context.Appointments.ToListAsync();
            //var result =
            //    appointments.Where(
            //        a =>
            //            (TimeZoneInfo.ConvertTimeFromUtc(a.StartTime, timeZoneInfo).Date >= DateTime.UtcNow))
            //        .OrderBy(a => a.StartTime);

            //var appointments = await _context.Appointments.Where(a => a.StartTime >= DateTime.UtcNow).ToListAsync();

            var appointments = await _context.Appointments.ToListAsync();
            return appointments.Where(a => (TimeZoneInfo.ConvertTimeFromUtc(a.StartTime, timeZoneInfo).Date - TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo).Date).Days >= 0).OrderBy(a => a.StartTime);

            return appointments;
        }

        public async Task<IEnumerable<Appointment>> GetAllPastDueAsync()
        {
            //JDD This is the summary of the logic for this (because it is kind of complicated):
                //appointments that have jobs in status "scheduled"
                //whose end times are less than the current time
                //whose job doesn't have another appointment where that end time is greater than the current time
                //only take the last appointment for that job (i.e. 2 appts both are past due we want the last one they made)

            //azure utc stuff
            var company = await _context.Companies.FirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            var allAppointments = await _context.Appointments.ToListAsync();
            var appointments =
                    allAppointments.Where(
                        a =>
                            a.Job.JobStatus.Description.ToLower().Equals("scheduled") &&
                            TimeZoneInfo.ConvertTimeFromUtc(a.EndTime, timeZoneInfo) <
                            TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo) &&
                            a.Job.Appointments.Count(
                                ja =>
                                    TimeZoneInfo.ConvertTimeFromUtc(ja.EndTime, timeZoneInfo) >
                                    TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo)) == 0)
                        .OrderBy(a => a.AppointmentId).ToList();
            
            //07.04.2014 JDD - this is tough with linq, we only want the last appointments for cases where there are multiple appointments -
            //so I am going to iterate and remove the earlier one. I think this should be okay since we are only grabbing past due appointments anyways.
            var jobsWithAppointments = new List<int>();

            for (int i = appointments.Count() - 1; i >= 0; i--)
            {
                if (jobsWithAppointments.Contains(appointments[i].JobId) == false)
                {
                    jobsWithAppointments.Add(appointments[i].JobId);
                }
                else
                {
                    appointments.RemoveAt(i);
                }
            }

            return appointments.OrderBy(a => a.EndTime);
        }

        public async Task<IEnumerable<Appointment>> GetAllDateRangeUTCAsync(DateTime start, DateTime end)
        {
            return await _context.Appointments.Where(a => a.StartTime >= start && a.StartTime <= end).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAllDateRangeLocalAsync(DateTime start, DateTime end)
        {
            //set times
            start = start.Date;
            end = end.Date.AddDays(1).AddMilliseconds(-1);

            //convert to utc before hitting db
            var company = await _context.Companies.FirstOrDefaultAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);
            start = TimeZoneInfo.ConvertTimeToUtc(start, timeZoneInfo);
            end = TimeZoneInfo.ConvertTimeToUtc(end, timeZoneInfo);

            return await _context.Appointments.Where(a => a.StartTime >= start && a.StartTime <= end).ToListAsync();
        }

        /// <summary>
        /// <see cref="IAppointmentRepository"/>
        /// </summary>
        /// <param name="appointment"><see cref="IAppointmentRepository"/></param>
        /// <returns><see cref="IAppointmentRepository"/></returns>
        public async Task<int> AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment.AppointmentId;
        }

        /// <summary>
        /// <see cref="IAppointmentRepository"/>
        /// </summary>
        /// <param name="appointment"><see cref="IAppointmentRepository"/></param>
        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Entry<Appointment>(appointment)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="IAppointmentRepository"/>
        /// </summary>
        /// <param name="appointmentId"><see cref="IAppointmentRepository"/></param>
        public async Task DeleteAsync(int? appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
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
