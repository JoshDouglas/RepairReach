using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RepairReach.Core.Model;
using RepairReach.Core.Service;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.ViewModels;

namespace RepairReach.WebApplication.Controllers
{
    public class MapController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository = null;
        private readonly IStaffRepository _staffRepository = null;
        private readonly ICompanyRepository _companyRepository = null;
        private IGeocodingService _geocodingService;

        public MapController(IAppointmentRepository appointmentRepository, IStaffRepository staffRepository, 
            IGeocodingService geocodingService, ICompanyRepository companyRepository)
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

            _appointmentRepository = appointmentRepository;
            _staffRepository = staffRepository;
            _geocodingService = geocodingService;
            _companyRepository = companyRepository;
        }
        // GET: Map
        public async Task<ActionResult> Index()
        {
            MapViewModel mapViewModel = new MapViewModel();
            //JDD - Can add this back when we do color support
            //mapViewModel.Appointments = await GetAppointmentEvents(DateTime.Now.Date.AddDays(-1), DateTime.Now.Date.AddDays(7));
            
            //get local datetime for today through next week.
            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            //for maps on view
            ViewBag.TimeZoneInfo = timeZoneInfo;

            var from = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);
            var to = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow.AddDays(7), timeZoneInfo);

            mapViewModel.Appointments = await _appointmentRepository.GetAllDateRangeLocalAsync(from, to);

            mapViewModel.ScheduledFrom = from;
            mapViewModel.ScheduledTo = to;

            return View(mapViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(MapViewModel mapViewModel)
        {
            //JDD - Can add this back when we do color support
            //mapViewModel.Appointments = await GetAppointmentEvents(mapViewModel.ScheduledFrom, mapViewModel.ScheduledTo);

            //get local datetime for today through next week.
            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            ViewBag.TimeZoneInfo = timeZoneInfo;

            mapViewModel.Appointments = await _appointmentRepository.GetAllDateRangeLocalAsync(mapViewModel.ScheduledFrom, mapViewModel.ScheduledTo);

            return View(mapViewModel);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentEvents(DateTime startDate, DateTime endDate)
        {
            var appointments = await _appointmentRepository.GetAllDateRangeUTCAsync(startDate, endDate);

            //TODO: for now hard-code colors up to 10 technicians, this probably ain't the best though
            string[] eventColors = { "#0066FF", "#993300", "#00CC00", "#FF33CC", "#CCCCFF", "#FFCC33", "#003333", "#00CC99", "#FFFFCC", "#996633" };

            return appointments;
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