using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories;
using RepairReach.Data;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RepairReach.Data.Infrastructure.Identity;

namespace RepairReach.WebApplication.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository = null;
        private readonly IStaffRepository _staffRepository = null;
        private readonly ICompanyRepository _companyRepository = null;
        private readonly ICustomerRepository _customerRepository = null;

        public AppointmentController(IAppointmentRepository appointmentRepository, IStaffRepository staffRepository, 
            ICompanyRepository companyRepository, ICustomerRepository customerRepository)
        {
            if (appointmentRepository == null)
            {
                throw new ArgumentNullException("appointmentRepository");
            }

            if (staffRepository == null)
            {
                throw new ArgumentNullException("staffRepository");
            }

            if (companyRepository == null)
            {
                throw new ArgumentNullException("companyRepository");
            }

            if (customerRepository == null)
            {
                throw new ArgumentNullException("customerRepository");
            }

            _appointmentRepository = appointmentRepository;
            _staffRepository = staffRepository;
            _companyRepository = companyRepository;
            _customerRepository = customerRepository;
        }

        // GET: /Appointment/Create
        [Authorize]
        public async Task<ActionResult> Create(int jobId)
        {
            var viewModel = new AppointmentCreateViewModel();
            viewModel.JobId = jobId;

            var customer = await _customerRepository.GetForJobAsync(jobId);
            viewModel.CallOnWay = customer.CallOnWay;
            viewModel.TextOnWay = customer.PrefersTextMessaging;

            var technicians = await _staffRepository.GetAllTechniciansAsync();
            var techniciansViewModel = Mapper.Map<IList<Staff>, IList<TeamIndexViewModel>>(technicians.ToList());

            var appointments = await _appointmentRepository.GetAllUpcomingFromTodayAsync();
            var mapViewModel = new MapViewModel();
            mapViewModel.Appointments = appointments;

            //for calendar
            viewModel.Technicians = techniciansViewModel;

            //for map
            viewModel.Map = mapViewModel;

            ViewBag.TechnicianStaffId = new SelectList(technicians, "StaffId", "DisplayName");

            //time zone stuff for map
            var company = await _companyRepository.GetFirstAsync();
            @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            return View(viewModel);
        }

        // POST: /Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AppointmentCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var appointment = Mapper.Map<AppointmentCreateViewModel, Appointment>(viewModel);

                //convert local to utc & assign to appointment
                var startDate = await GetUtcDateTime(viewModel.StartDate);
                var startTime = await GetUtcDateTime(viewModel.StartTime);
                var endDate = await GetUtcDateTime(viewModel.EndDate);
                var endTime = await GetUtcDateTime(viewModel.EndTime);
                appointment.StartTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, 0);
                appointment.EndTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, 0);
                
                //get current user
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
                ApplicationUser user = new ApplicationUser();
                user = await userManager.FindByNameAsync(User.Identity.Name);

                //created
                appointment.CreatedBy = user.Staff.DisplayName;
                appointment.Created = DateTime.UtcNow;

                await _appointmentRepository.AddAsync(appointment);
                return RedirectToAction("Edit", "Job", new { id = viewModel.JobId });
            }

            var technicians = await _staffRepository.GetAllTechniciansAsync();
            var techniciansViewModel = Mapper.Map<IList<Staff>, IList<TeamIndexViewModel>>(technicians.ToList());

            var appointments = await _appointmentRepository.GetAllUpcomingFromTodayAsync();
            var mapViewModel = new MapViewModel();
            mapViewModel.Appointments = appointments;

            //for calendar
            viewModel.Technicians = techniciansViewModel;
            //for map
            viewModel.Map = mapViewModel;

            ViewBag.TechnicianStaffId = new SelectList(technicians, "StaffId", "DisplayName", viewModel.TechnicianStaffId);

            //time zone stuff for map
            var company = await _companyRepository.GetFirstAsync();
            @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            return View(viewModel);
        }

        // GET: /Appointment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await _appointmentRepository.GetAsync(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            //for calendar
            var technicians = await _staffRepository.GetAllTechniciansAsync();
            var techniciansViewModel = Mapper.Map<IList<Staff>, IList<TeamIndexViewModel>>(technicians.ToList());

            //for map
            var appointments = await _appointmentRepository.GetAllUpcomingFromTodayAsync();
            var mapViewModel = new MapViewModel();
            mapViewModel.Appointments = appointments;

            ViewBag.TechnicianStaffId = new SelectList(technicians, "StaffId", "DisplayName", appointment.TechnicianStaffId);

            var viewModel = Mapper.Map<Appointment, AppointmentEditViewModel>(appointment);
            viewModel.Technicians = techniciansViewModel;
            viewModel.Map = mapViewModel;

            //convert utc to local (appointment saves 1 date and we split it into 2 for ux)
            viewModel.StartDate = await GetLocalDate(appointment.StartTime);
            viewModel.StartTime = await GetLocalDate(appointment.StartTime);
            viewModel.EndDate = await GetLocalDate(appointment.EndTime);
            viewModel.EndTime = await GetLocalDate(appointment.EndTime);

            //time zone stuff for map
            var company = await _companyRepository.GetFirstAsync();
            @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            return View(viewModel);
        }

        // POST: /Appointment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AppointmentEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var appointment = Mapper.Map<AppointmentEditViewModel, Appointment>(viewModel);

                //local to utc
                var startDate = await GetUtcDateTime(viewModel.StartDate);
                var startTime = await GetUtcDateTime(viewModel.StartTime);
                var endDate = await GetUtcDateTime(viewModel.EndDate);
                var endTime = await GetUtcDateTime(viewModel.EndTime);
                appointment.StartTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startTime.Hour, startTime.Minute, 0);
                appointment.EndTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endTime.Hour, endTime.Minute, 0);

                //completed
                if (appointment.IsCompleted && appointment.CompletedTime.HasValue == false) appointment.CompletedTime = DateTime.UtcNow;
                if (appointment.IsCompleted == false && appointment.CompletedTime.HasValue) appointment.CompletedTime = null;

                await _appointmentRepository.UpdateAsync(appointment);
                return RedirectToAction("Edit", "Job", new { id = viewModel.JobId });
            }

            //for calendar
            var technicians = await _staffRepository.GetAllTechniciansAsync();
            var techniciansViewModel = Mapper.Map<IList<Staff>, IList<TeamIndexViewModel>>(technicians.ToList());

            //for map
            var appointments = await _appointmentRepository.GetAllUpcomingFromTodayAsync();
            var mapViewModel = new MapViewModel();
            mapViewModel.Appointments = appointments;

            ViewBag.TechnicianStaffId = new SelectList(technicians, "StaffId", "DisplayName", viewModel.TechnicianStaffId);

            viewModel.Technicians = techniciansViewModel;
            viewModel.Map = mapViewModel;

            //time zone stuff for map
            var company = await _companyRepository.GetFirstAsync();
            @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            return View(viewModel);
        }

        // GET: /Appointment/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = await _appointmentRepository.GetAsync(id);
            return View(appointment);
        }

        // POST: /Appointment/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Appointment appointment = await _appointmentRepository.GetAsync(id);
            int jobId = appointment.JobId;
            await _appointmentRepository.DeleteAsync(id);
            return RedirectToAction("Edit", "Job", new { id = jobId });
        }

        private async Task<DateTime> GetLocalDate(DateTime? utcDateTime)
        {
            if (utcDateTime.HasValue == false) return DateTime.Now;

            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);
            var localDateTime = utcDateTime;

            localDateTime = TimeZoneInfo.ConvertTimeFromUtc(localDateTime.Value, timeZoneInfo);

            return localDateTime.Value;
        }

        private async Task<DateTime> GetUtcDateTime(DateTime? localDateTime)
        {
            if (localDateTime.HasValue == false) return DateTime.Now;

            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);
            var utcDateTime = localDateTime;

            utcDateTime = DateTime.SpecifyKind(utcDateTime.Value, DateTimeKind.Unspecified);
            utcDateTime = TimeZoneInfo.ConvertTimeToUtc(utcDateTime.Value, timeZoneInfo);

            return utcDateTime.Value;
        }

        private async Task<Appointment> GetFromViewModel(AppointmentViewModel viewModel)
        {
            var appointment = new Appointment();

            appointment.AppointmentId = viewModel.AppointmentId;
            appointment.JobId = viewModel.JobId;
            appointment.TechnicianStaffId = viewModel.TechnicianStaffId;
            appointment.Created = viewModel.Created;
            appointment.CreatedBy = viewModel.CreatedBy;
            appointment.Note = viewModel.Note;
            appointment.IsCompleted = viewModel.IsCompleted;
            var startDate = Convert.ToDateTime(viewModel.StartDate);
            var startTime = Convert.ToDateTime(viewModel.StartTime);
            var endDate = Convert.ToDateTime(viewModel.EndDate);
            var endTime = Convert.ToDateTime(viewModel.EndTime);

            //convert to utc
            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Unspecified);
            startTime = DateTime.SpecifyKind(startTime, DateTimeKind.Unspecified);
            endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Unspecified);
            endTime = DateTime.SpecifyKind(endTime, DateTimeKind.Unspecified);

            startDate = TimeZoneInfo.ConvertTimeToUtc(startDate, timeZoneInfo);
            startTime = TimeZoneInfo.ConvertTimeToUtc(startTime, timeZoneInfo);
            endDate = TimeZoneInfo.ConvertTimeToUtc(endDate, timeZoneInfo);
            endTime = TimeZoneInfo.ConvertTimeToUtc(endTime, timeZoneInfo);

            appointment.StartTime = new DateTime(startDate.Year, startDate.Month,
                startDate.Day, startTime.Hour, startTime.Minute, 0);
            appointment.EndTime = new DateTime(endDate.Year, endDate.Month,
                endDate.Day, endTime.Hour, endTime.Minute, 0);

            return appointment;
        }

        private async Task<AppointmentViewModel> GetViewModelFrom(Appointment appointment)
        {
            var viewModel = new AppointmentViewModel();

            viewModel.AppointmentId = appointment.AppointmentId;
            viewModel.JobId = appointment.JobId;
            viewModel.TechnicianStaffId = appointment.TechnicianStaffId;
            viewModel.Created = appointment.Created;
            viewModel.CreatedBy = appointment.CreatedBy;
            viewModel.Note = appointment.Note;

            //convert from utc
            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            viewModel.StartDate = TimeZoneInfo.ConvertTimeFromUtc(appointment.StartTime, timeZoneInfo);
            viewModel.StartTime = TimeZoneInfo.ConvertTimeFromUtc(appointment.StartTime, timeZoneInfo);
            viewModel.EndDate = TimeZoneInfo.ConvertTimeFromUtc(appointment.EndTime, timeZoneInfo);
            viewModel.EndTime = TimeZoneInfo.ConvertTimeFromUtc(appointment.EndTime, timeZoneInfo);

            viewModel.IsCompleted = appointment.IsCompleted;

            return viewModel;
        }

        public async Task<JsonResult> GetAppointmentEvents(double start, double end)
        {
            DateTime startDate = UnixTimeStampToDateTime(start);
            DateTime endDate = UnixTimeStampToDateTime(end);
            //07.08.2014 JDD - Apparently full calendar recognizes server as UTC - so this looks okay.
            var appointments = await _appointmentRepository.GetAllDateRangeUTCAsync(startDate, endDate);
            var appointmentViewModels = new List<AppointmentJSONViewModel>();

            //TODO: for now hard-code colors up to 10 technicians, this probably ain't the best though
            string[] eventColors = { "#0066FF", "#993300", "#00CC00", "#FF33CC", "#CCCCFF", "#FFCC33", "#003333", "#00CC99", "#FFFFCC", "#996633" };
            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            foreach (Appointment a in appointments)
            {
                var startDateString = TimeZoneInfo.ConvertTimeFromUtc(a.StartTime, timeZoneInfo).ToShortDateString();
                var startTimeString = TimeZoneInfo.ConvertTimeFromUtc(a.StartTime, timeZoneInfo).ToShortTimeString();
                var endTimeString = TimeZoneInfo.ConvertTimeFromUtc(a.EndTime, timeZoneInfo).ToShortTimeString();

                var viewModel = new AppointmentJSONViewModel();
                viewModel.id = a.AppointmentId.ToString();
                viewModel.start = TimeZoneInfo.ConvertTimeFromUtc(a.StartTime, timeZoneInfo).ToString();
                viewModel.end = TimeZoneInfo.ConvertTimeFromUtc(a.EndTime, timeZoneInfo).ToString();
                viewModel.resourceId = a.TechnicianStaffId.ToString();
                viewModel.title = "Job # " + a.Job.JobNumber + Environment.NewLine +
                                  a.Job.Customer.DisplayName + Environment.NewLine +
                                  a.Job.FullAddress;
                viewModel.tooltipDescription = startDateString + "<br />" +
                                    startTimeString + " - " + endTimeString + "<br />" +
                                    "#" + a.Job.JobNumber + "<br />" +
                                    a.Job.Customer.DisplayName + "<br />" +
                                    a.Job.FullAddress;
                viewModel.allDay = false;
                viewModel.url = "/Job/Edit?id=" + a.JobId;
                viewModel.backgroundColor = "#0066FF"; //default color for now
                if (a.TechnicianStaffId <= 9) viewModel.backgroundColor = eventColors[a.TechnicianStaffId];
                appointmentViewModels.Add(viewModel);
            }

            return Json(appointmentViewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UpdateCalendarAppointment(int appointmentId, string start, string end, int? resourceId)
        {
            Appointment appointment = await _appointmentRepository.GetAsync(appointmentId);

            //DateTime startTime = UnixTimeStampToDateTime(start);
            //DateTime endTime = UnixTimeStampToDateTime(end);

            //apparently fullcalendar recognizes server as utc - so this appears good

            DateTime startTime = Convert.ToDateTime(start);
            DateTime endTime = Convert.ToDateTime(end);

            appointment.StartTime = startTime;
            appointment.EndTime = endTime;

            if (resourceId.HasValue) appointment.TechnicianStaffId = resourceId.Value;

            await _appointmentRepository.UpdateAsync(appointment);

            return Json(new {Success = true, Value = ""}, JsonRequestBehavior.AllowGet);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _appointmentRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
