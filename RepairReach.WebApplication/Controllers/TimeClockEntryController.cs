using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories;
using RepairReach.Data;
using RepairReach.Data.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RepairReach.Data.Infrastructure.Identity;
using RepairReach.WebApplication.ViewModels;

namespace RepairReach.WebApplication.Controllers
{
    public class TimeClockEntryController : Controller
    {
        private readonly ITimeClockEntryRepository _timeClockEntryRepository = null;
        private readonly IStaffRepository _staffRepository = null;

        public TimeClockEntryController(ITimeClockEntryRepository timeClockEntryRepository, IStaffRepository staffRepository)
        {
            if (timeClockEntryRepository == null)
            {
                throw new ArgumentNullException("timeClockEntryRepository");
            }

            if (staffRepository == null)
            {
                throw new ArgumentNullException("staffRepository");
            }

            _timeClockEntryRepository = timeClockEntryRepository;
            _staffRepository = staffRepository;
        }

        // GET: /TimeClockEntry/
        public async Task<ActionResult> Index()
        {
            var currentUserStaff = await GetCurrentUserStaff();

            ViewBag.CanClockOut = currentUserStaff.IsClockedIn;
            ViewBag.CanClockIn = currentUserStaff.IsClockedOut;
            ViewBag.StaffId = currentUserStaff.StaffId;

            if (currentUserStaff.StaffId > 0) return View(await _timeClockEntryRepository.GetAllForEmployeeAsync(currentUserStaff.StaffId));
            return View(await _timeClockEntryRepository.GetAllForEmployeeAsync(0));
        }

        // GET: /TimeClockEntry/
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ManagerIndex()
        {
            if (User.Identity.Name != "DemoAdmin") return RedirectToAction("Index");

            var timeClockEntires = await _timeClockEntryRepository.GetAllAsync();

            return View(timeClockEntires);
        }

        // GET: /TimeClockEntry/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeClockEntry timeClockEntry = await _timeClockEntryRepository.GetAsync(id);
            if (timeClockEntry == null)
            {
                return HttpNotFound();
            }
            return View(timeClockEntry);
        }

        // GET: /TimeClockEntry/Create
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var staffs = await _staffRepository.GetAllAsync();
            ViewBag.StaffId = new SelectList(staffs, "StaffId", "DisplayName");

            var timeClockEntryViewModel = new TimeClockEntryCreateViewModel();
            timeClockEntryViewModel.DateWorked = DateTime.Today;
            timeClockEntryViewModel.SetTimeOut = true;
            //timeClockEntryViewModel.TimeIn = DateTime.Today;
            //timeClockEntryViewModel.TimeOut = DateTime.Today;

            return View(timeClockEntryViewModel);
        }

        // POST: /TimeClockEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(TimeClockEntryCreateViewModel timeClockEntryViewModel)
        {
            if (ModelState.IsValid)
            {
                var staff = await _staffRepository.GetAsync(timeClockEntryViewModel.StaffId);

                var timeClockEntry = new TimeClockEntry();
                timeClockEntry.StaffId = timeClockEntryViewModel.StaffId;
                timeClockEntry.TimeIn = timeClockEntryViewModel.TimeIn;
                timeClockEntry.HourlyRate = staff.HourlyRate;
                timeClockEntry.TimeOut = timeClockEntryViewModel.TimeOut;
                timeClockEntry.DatePaid = timeClockEntryViewModel.DatePaid;

                //date for time in/out
                timeClockEntry.TimeIn = new DateTime(timeClockEntryViewModel.DateWorked.Year,
                    timeClockEntryViewModel.DateWorked.Month, timeClockEntryViewModel.DateWorked.Day,
                    timeClockEntry.TimeIn.Value.Hour, timeClockEntry.TimeIn.Value.Minute, 0);

                if (timeClockEntryViewModel.TimeOut.HasValue && timeClockEntry.TimeOut.HasValue && timeClockEntryViewModel.SetTimeOut == true)
                {
                    timeClockEntry.TimeOut = new DateTime(timeClockEntryViewModel.DateWorked.Year,
                        timeClockEntryViewModel.DateWorked.Month, timeClockEntryViewModel.DateWorked.Day,
                        timeClockEntry.TimeOut.Value.Hour, timeClockEntry.TimeOut.Value.Minute, 0);
                }
                else
                {
                    timeClockEntry.TimeOut = null;
                }

                await _timeClockEntryRepository.AddAsync(timeClockEntry);
                return RedirectToAction("ManagerIndex");
            }

            var staffs = await _staffRepository.GetAllAsync();
            ViewBag.StaffId = new SelectList(staffs, "StaffId", "DisplayName");

            return View(timeClockEntryViewModel);
        }

        // GET: /TimeClockEntry/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeClockEntry timeClockEntry = await _timeClockEntryRepository.GetAsync(id);
            if (timeClockEntry == null)
            {
                return HttpNotFound();
            }

            var staffs = await _staffRepository.GetAllAsync();
            ViewBag.StaffId = new SelectList(staffs, "StaffId", "DisplayName", timeClockEntry.StaffId);

            var timeClockEditViewModel = new TimeClockEntryEditViewModel();
            timeClockEditViewModel.TimeClockEntryId = timeClockEntry.TimeClockEntryId;
            timeClockEditViewModel.StaffId = timeClockEntry.StaffId;
            timeClockEditViewModel.HourlyRate = timeClockEntry.HourlyRate;
            timeClockEditViewModel.TimeIn = (timeClockEntry.TimeIn.HasValue) ? timeClockEntry.TimeIn.Value : DateTime.UtcNow;
            timeClockEditViewModel.TimeOut = timeClockEntry.TimeOut;
            timeClockEditViewModel.DatePaid = timeClockEntry.DatePaid;
            timeClockEditViewModel.DateWorked = (timeClockEntry.TimeIn.HasValue)
                ? timeClockEntry.TimeIn.Value.Date
                : DateTime.Today;
            timeClockEditViewModel.SetTimeOut = true;

            return View(timeClockEditViewModel);
        }

        // POST: /TimeClockEntry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(TimeClockEntryEditViewModel timeClockEditViewModel)
        {
            if (ModelState.IsValid)
            {
                var staff = await _staffRepository.GetAsync(timeClockEditViewModel.StaffId);

                var timeClockEntry = new TimeClockEntry();
                timeClockEntry.StaffId = timeClockEditViewModel.StaffId;
                timeClockEntry.TimeIn = timeClockEditViewModel.TimeIn;
                timeClockEntry.HourlyRate = timeClockEditViewModel.HourlyRate;
                timeClockEntry.TimeOut = timeClockEditViewModel.TimeOut;
                timeClockEntry.DatePaid = timeClockEditViewModel.DatePaid;

                //date for time in/out
                timeClockEntry.TimeIn = new DateTime(timeClockEditViewModel.DateWorked.Year,
                    timeClockEditViewModel.DateWorked.Month, timeClockEditViewModel.DateWorked.Day,
                    timeClockEntry.TimeIn.Value.Hour, timeClockEntry.TimeIn.Value.Minute, 0);

                if (timeClockEditViewModel.TimeOut.HasValue && timeClockEntry.TimeOut.HasValue && timeClockEditViewModel.SetTimeOut == true)
                {
                    timeClockEntry.TimeOut = new DateTime(timeClockEditViewModel.DateWorked.Year,
                        timeClockEditViewModel.DateWorked.Month, timeClockEditViewModel.DateWorked.Day,
                        timeClockEntry.TimeOut.Value.Hour, timeClockEntry.TimeOut.Value.Minute, 0);
                }
                else
                {
                    timeClockEntry.TimeOut = null;
                }

                await _timeClockEntryRepository.AddAsync(timeClockEntry);
                return RedirectToAction("ManagerIndex");
            }

            var staffs = await _staffRepository.GetAllAsync();
            ViewBag.StaffId = new SelectList(staffs, "StaffId", "DisplayName");

            return View(timeClockEditViewModel);
        }

        // GET: /TimeClockEntry/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeClockEntry timeClockEntry = await _timeClockEntryRepository.GetAsync(id);
            return View(timeClockEntry);
        }

        // POST: /TimeClockEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TimeClockEntry TimeClockEntry = await _timeClockEntryRepository.GetAsync(id);
            await _timeClockEntryRepository.DeleteAsync(id);
            return RedirectToAction("ManagerIndex");
        }

        public async Task<ActionResult> ClockIn(int staffId)
        {
            var staff = await _staffRepository.GetAsync(staffId);
            var newTimeClockEntry = new TimeClockEntry();
            newTimeClockEntry.StaffId = staff.StaffId;
            newTimeClockEntry.TimeIn = DateTime.UtcNow;
            newTimeClockEntry.HourlyRate = staff.HourlyRate;

            //TODO: model valid?
            await _timeClockEntryRepository.AddAsync(newTimeClockEntry);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ClockOut(int staffId)
        {
            var lastTimeClockEntry = await _timeClockEntryRepository.GetLastForEmployeeAsync(staffId);
            lastTimeClockEntry.TimeOut = DateTime.UtcNow;

            //TODO: model valid?
            await _timeClockEntryRepository.UpdateAsync(lastTimeClockEntry);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timeClockEntryRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<Staff> GetCurrentUserStaff()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_timeClockEntryRepository.GetContext()));
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user.StaffId != null) return user.Staff;

            return new Staff();
        }
    }
}
