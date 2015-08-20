using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using RepairReach.Core.Model;
using RepairReach.Core.Enum;
using RepairReach.Data.Repositories;
using RepairReach.Data;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.ViewModels;

namespace RepairReach.WebApplication.Controllers
{
    [Authorize]
    public class ApplianceController : Controller
    {
        private readonly IApplianceRepository _applianceRepository = null;
        private readonly ICustomerRepository _customerRepository = null;

        public ApplianceController(IApplianceRepository applianceRepository, ICustomerRepository customerRepository)
        {
            if (applianceRepository == null)
            {
                throw new ArgumentNullException("applianceRepository");
            }

            if (customerRepository == null)
            {
                throw new ArgumentNullException("customerRepository");
            }

            _applianceRepository = applianceRepository;
            _customerRepository = customerRepository;
        }

        // GET: /Appliance/
        public async Task<ActionResult> Index()
        {
            return View(await _applianceRepository.GetAllAsync());
        }

        // GET: /Appliance/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appliance appliance = await _applianceRepository.GetAsync(id);
            if (appliance == null)
            {
                return HttpNotFound();
            }
            return View(appliance);
        }

        // GET: /Appliance/Create
        public async Task<ActionResult> Create(int? jobId)
        {
            var viewModel = new ApplianceCreateViewModel();

            if (jobId.HasValue) viewModel.JobId = jobId.Value;

            var applianceTypes = from ApplianceTypeEnum a in Enum.GetValues(typeof(ApplianceTypeEnum))
                               select new { ID = a, Name = a.ToString() };
            ViewBag.Type = new SelectList(applianceTypes, "ID", "Name");

            return View(viewModel);
        }

        // POST: /Appliance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplianceCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var appliance = Mapper.Map<ApplianceCreateViewModel, Appliance>(viewModel);
                await _applianceRepository.AddAsync(appliance);
                return RedirectToAction("Edit", "Job", new { id = appliance.JobId });
            }

            var applianceTypes = from ApplianceTypeEnum a in Enum.GetValues(typeof(ApplianceTypeEnum))
                                 select new { ID = a, Name = a.ToString() };
            ViewBag.Type = new SelectList(applianceTypes, "ID", "Name", viewModel.Type);

            return View(viewModel);
        }

        // GET: /Appliance/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appliance appliance = await _applianceRepository.GetAsync(id);
            if (appliance == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Appliance, ApplianceEditViewModel>(appliance);

            var applianceTypes = from ApplianceTypeEnum a in Enum.GetValues(typeof(ApplianceTypeEnum))
                                 select new { ID = a, Name = a.ToString() };
            ViewBag.Type = new SelectList(applianceTypes, "ID", "Name", viewModel.Type);

            return View(viewModel);
        }

        // POST: /Appliance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplianceEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var appliance = Mapper.Map<ApplianceEditViewModel, Appliance>(viewModel);

                await _applianceRepository.UpdateAsync(appliance);
                return RedirectToAction("Edit", "Job", new { id = appliance.JobId });
            }

            var applianceTypes = from ApplianceTypeEnum a in Enum.GetValues(typeof(ApplianceTypeEnum))
                                 select new { ID = a, Name = a.ToString() };
            ViewBag.Type = new SelectList(applianceTypes, "ID", "Name", viewModel.Type);

            return View(viewModel);
        }

        // GET: /Appliance/Delete/5
        public async Task<ActionResult> Delete(int? id, int? jobId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appliance appliance = await _applianceRepository.GetAsync(id);
            ApplianceViewModel applianceViewModel = new ApplianceViewModel();
            applianceViewModel.Appliance = appliance;
            if (jobId.HasValue) applianceViewModel.Appliance.JobId = jobId.Value;

            return View(applianceViewModel);
        }

        // POST: /Appliance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(ApplianceViewModel applianceViewModel)
        {
            int? deletedApplianceJobId = applianceViewModel.Appliance.JobId;
            Appliance appliance = applianceViewModel.Appliance;
            await _applianceRepository.DeleteAsync(appliance.ApplianceId);
            return RedirectToAction("Edit", "Job", new { id = deletedApplianceJobId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _applianceRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
