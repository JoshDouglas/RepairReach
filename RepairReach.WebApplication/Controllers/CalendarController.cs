using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories.Interfaces;
using System.Threading.Tasks;

namespace RepairReach.WebApplication.Controllers
{
    public class CalendarController : Controller
    {
        private readonly IStaffRepository _staffRepository = null;

        public CalendarController(IStaffRepository staffRepository)
        {
            if (staffRepository == null)
            {
                throw new ArgumentNullException("staffRepository");
            }

            _staffRepository = staffRepository;
        }

        // GET: Calendar
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var technicians = await _staffRepository.GetAllTechniciansAsync();
            return View(technicians.ToList());
        }
    }
}