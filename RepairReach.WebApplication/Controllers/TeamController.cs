using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RepairReach.Core.Model;
using RepairReach.Core.Enum;
using RepairReach.Data.Infrastructure.Identity;
using RepairReach.Data.Repositories;
using RepairReach.Data;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AutoMapper;

namespace RepairReach.WebApplication.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        private readonly IStaffRepository _staffRepository = null;

        public TeamController(IStaffRepository staffRepository)
        {
            if (staffRepository == null)
            {
                throw new ArgumentNullException("staffRepository");
            }

            _staffRepository = staffRepository;
        }

        // GET: /Staff/
        public async Task<ActionResult> Index()
        {
            var staffs = await _staffRepository.GetAllAsync();
            var viewModel = Mapper.Map<IEnumerable<Staff>, IEnumerable<TeamIndexViewModel>>(staffs);

            return View(viewModel);
        }

        // GET: /Staff/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = await _staffRepository.GetAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: /Staff/Create
        public ActionResult Create()
        {
            var userTitles = from UserTitleEnum u in Enum.GetValues(typeof(UserTitleEnum))
                                 select new { ID = u, Name = u.ToString() };
            ViewBag.UserTitle = new SelectList(userTitles, "ID", "Name");

            var viewModel = new TeamCreateViewModel();

            return View(viewModel);
        }

        // POST: /Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TeamCreateViewModel viewModel)
        {
            var userTitles = from UserTitleEnum u in Enum.GetValues(typeof(UserTitleEnum))
                             select new { ID = u, Name = u.ToString() };
            ViewBag.UserTitle = new SelectList(userTitles, "ID", "Name", viewModel.UserTitle);

            try
            {
                if (ModelState.IsValid)
                {
                    var staff = Mapper.Map<TeamCreateViewModel, Staff>(viewModel);
                    staff.IsActive = true;

                    int staffId = await _staffRepository.AddAsync(staff);

                    //update the user account
                    if (string.IsNullOrEmpty(viewModel.Password) == false && string.IsNullOrEmpty(viewModel.Username) == false)
                    {
                        await UpdateUserAccount(staffId, staff.Username, viewModel.Password, viewModel.UserTitle);
                    }

                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                var errors = ex.EntityValidationErrors.First(); //.ValidationErrors.First();
                foreach (var propertyError in errors.ValidationErrors)
                {
                    this.ModelState.AddModelError
                     (propertyError.PropertyName, propertyError.ErrorMessage);
                }
                return View();
            }

            return View(viewModel);
        }

        // GET: /Staff/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Staff staff = await _staffRepository.GetAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Staff, TeamEditViewModel>(staff);

            var userTitles = from UserTitleEnum u in Enum.GetValues(typeof(UserTitleEnum))
                             select new { ID = u, Name = u.ToString() };
            ViewBag.UserTitle = new SelectList(userTitles, "ID", "Name", viewModel.UserTitle);

            return View(viewModel);
        }

        // POST: /Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeamEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var staff = Mapper.Map<TeamEditViewModel, Staff>(viewModel);

                await _staffRepository.UpdateAsync(staff);

                //update user account
                if (string.IsNullOrEmpty(viewModel.Password) == false && string.IsNullOrEmpty(viewModel.Username) == false)
                {
                    await UpdateUserAccount(staff.StaffId, staff.Username, viewModel.Password, viewModel.UserTitle);
                }

                return RedirectToAction("Index");
            }

            var userTitles = from UserTitleEnum u in Enum.GetValues(typeof(UserTitleEnum))
                             select new { ID = u, Name = u.ToString() };
            ViewBag.UserTitle = new SelectList(userTitles, "ID", "Name", viewModel.UserTitle);

            return View(viewModel);
        }

        // GET: /Staff/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = await _staffRepository.GetAsync(id);
            return View(staff);
        }

        // POST: /Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //05.10.2014 JDD - mark as no longer active instead of delete
            Staff staff = await _staffRepository.GetAsync(id);
            staff.IsActive = false;
            //await _staffRepository.DeleteAsync(id);
            await _staffRepository.UpdateAsync(staff);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _staffRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task CreateUserAccount(int staffId, string name, string password, UserTitleEnum userTitle)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.UserName = name;
            applicationUser.StaffId = staffId;
            var identityResult = await userManager.CreateAsync(applicationUser, password);
            if (identityResult.Succeeded)
            {
                applicationUser = await userManager.FindByNameAsync(name);
                await userManager.AddToRoleAsync(applicationUser.Id, userTitle.ToString());
            }
        }

        private async Task UpdateUserAccountPassword(string name, string password, UserTitleEnum userTitle)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
            ApplicationUser user = new ApplicationUser();
            user = await userManager.FindByNameAsync(name);
            await userManager.RemovePasswordAsync(user.Id);
            await userManager.AddPasswordAsync(user.Id, password);
            //TODO: change role
        }

        private async Task UpdateUserAccount(int staffId, string name, string password, UserTitleEnum userTitle)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
            var user = await userManager.FindByNameAsync(name);
            if (user != null)
            {
                await UpdateUserAccountPassword(name, password, userTitle);
            }
            else
            {
                await CreateUserAccount(staffId, name, password, userTitle);
            }
        }

        public async Task<JsonResult> GetCalendarTechnicians()
        {
            var technicians = await _staffRepository.GetAllTechniciansAsync();
            List<TechnicianJsonViewModel> technicianViewModels;
            technicianViewModels = new List<TechnicianJsonViewModel>();
            foreach (Staff s in technicians)
            {
                var technicianViewModel = new TechnicianJsonViewModel();
                technicianViewModel.name = s.DisplayName;
                technicianViewModel.id = s.StaffId.ToString();
                technicianViewModels.Add(technicianViewModel);
            }

            return Json(technicianViewModels, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTermJsonAsync(string term)
        {
            var team = await _staffRepository.GetAllTermAsync(term);
            var teamJson = new List<Object>();
            foreach (Staff staff in team)
            {
                teamJson.Add(new { DisplayName = staff.DisplayName });
            }

            return Json(teamJson, JsonRequestBehavior.AllowGet);
        }
    }
}
