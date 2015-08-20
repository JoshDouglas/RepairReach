using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RepairReach.Core.Model;
using RepairReach.Data.Infrastructure.Identity;
using RepairReach.Data.Repositories;
using RepairReach.Data;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.ViewModels;
using AutoMapper;

namespace RepairReach.WebApplication.Controllers
{
    [Authorize]
    public class LineItemController : Controller
    {
        private readonly ILineItemRepository _lineItemRepository = null;
        private readonly IApplianceRepository _applianceRepository = null;
        private readonly IServiceRepository _serviceRepository = null;
        private readonly IStaffRepository _staffRepository = null;
        private readonly ITaxRateRepository _taxRateRepository = null;
        private readonly IPartRepository _partRepository = null;
        private readonly IJobRepository _jobRepository = null;
        private readonly IQuickLineItemRepository _quickLineItemRepository;

        public LineItemController(ILineItemRepository lineItemRepository, IApplianceRepository applianceRepository,
            IServiceRepository serviceRepository, IStaffRepository staffRepository, ITaxRateRepository taxRateRepository,
            IPartRepository partRepository, IJobRepository jobRepository, IQuickLineItemRepository quickLineItemRepository)
        {
            if (lineItemRepository == null)
            {
                throw new ArgumentNullException("lineItemRepository");
            }

            if (applianceRepository == null)
            {
                throw new ArgumentNullException("applianceRepository");
            }

            if (serviceRepository == null)
            {
                throw new ArgumentNullException("serviceRepository");
            }

            if (staffRepository == null)
            {
                throw new ArgumentNullException("staffRepository");
            }

            if (taxRateRepository == null)
            {
                throw new ArgumentNullException("taxRateRepository");
            }

            if (partRepository == null)
            {
                throw new ArgumentNullException("partRepository");
            }

            if (jobRepository == null)
            {
                throw new ArgumentNullException("jobRepository");
            }

            if (quickLineItemRepository == null)
            {
                throw new ArgumentNullException("quickLineItemRepository");
            }

            _lineItemRepository = lineItemRepository;
            _applianceRepository = applianceRepository;
            _serviceRepository = serviceRepository;
            _staffRepository = staffRepository;
            _taxRateRepository = taxRateRepository;
            _partRepository = partRepository;
            _jobRepository = jobRepository;
            _quickLineItemRepository = quickLineItemRepository;
        }

        // GET: /LineItem/
        public async Task<ActionResult> Index(int? jobId)
        {
            ViewBag.JobId = jobId;
            return View(await _lineItemRepository.GetAllByJobAsync(jobId.Value));
        }

        // GET: /LineItem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineItem lineItem = await _lineItemRepository.GetAsync(id);
            if (lineItem == null)
            {
                return HttpNotFound();
            }
            return View(lineItem);
        }

        // GET: /LineItem/Create
        public async Task<ActionResult> Create(int? jobId)
        {
            var viewModel = new LineItemCreateViewModel();
            viewModel.JobId = jobId.Value;
            viewModel.LineItemNumber = await _lineItemRepository.GetMaxLineItemByJob(jobId.Value) + 1;

            var technicians = await _staffRepository.GetAllAsync();
            var taxRates = await _taxRateRepository.GetAllAsync();
            var services = await _serviceRepository.GetAllAsync();
            var parts = await _partRepository.GetAllAsync();
            var quickLineItems = await _quickLineItemRepository.GetAllAsync();

            //default user
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
            ApplicationUser user = new ApplicationUser();
            user = await userManager.FindByNameAsync(User.Identity.Name);

            //get default tax rate to be selected
            TaxRate defaultTaxRate = new TaxRate();
            defaultTaxRate = await _taxRateRepository.GetDefaultRateAsync();

            ViewBag.StaffId = new SelectList(technicians, "StaffId", "DisplayName", user.StaffId);
            ViewBag.TaxRateId = new SelectList(taxRates, "TaxRateId", "DisplayName", defaultTaxRate.TaxRateId);
            ViewBag.ServiceId = new SelectList(services, "ServiceId", "Name");
            ViewBag.PartId = new SelectList(parts, "PartId", "DisplayName");
            ViewBag.QuickLineItemId = new SelectList(quickLineItems, "QuickLineItemId", "Description");

            return View(viewModel);
        }

        // POST: /LineItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LineItemCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var lineItem = Mapper.Map<LineItemCreateViewModel, LineItem>(viewModel);

                await _lineItemRepository.AddAsync(lineItem);
                return RedirectToAction("Edit", "Job", new { id = lineItem.JobId });
            }

            var technicians = await _staffRepository.GetAllAsync();
            var taxRates = await _taxRateRepository.GetAllAsync();
            var services = await _serviceRepository.GetAllAsync();
            var parts = await _partRepository.GetAllAsync();
            var quickLineItems = await _quickLineItemRepository.GetAllAsync();

            ViewBag.StaffId = new SelectList(technicians, "StaffId", "DisplayName");
            ViewBag.TaxRateId = new SelectList(taxRates, "TaxRateId", "DisplayName");
            ViewBag.ServiceId = new SelectList(services, "ServiceId", "Name");
            ViewBag.PartId = new SelectList(parts, "PartId", "DisplayName");
            ViewBag.QuickLineItemId = new SelectList(quickLineItems, "QuickLineItemId", "Description");

            return View(viewModel);
        }

        // GET: /LineItem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LineItem lineItem = await _lineItemRepository.GetAsync(id);
            if (lineItem == null)
            {
                return HttpNotFound();
            }

            var job = await _jobRepository.GetAsync(lineItem.JobId);
            var technicians = await _staffRepository.GetAllAsync();
            var taxRates = await _taxRateRepository.GetAllAsync();
            var services = await _serviceRepository.GetAllAsync();
            var parts = await _partRepository.GetAllAsync();
            var quickLineItems = await _quickLineItemRepository.GetAllAsync();

            ViewBag.StaffId = new SelectList(technicians, "StaffId", "DisplayName", lineItem.StaffId);
            ViewBag.TaxRateId = new SelectList(taxRates, "TaxRateId", "DisplayName", lineItem.TaxRateId);
            ViewBag.ServiceId = new SelectList(services, "ServiceId", "Name");
            ViewBag.PartId = new SelectList(parts, "PartId", "DisplayName");
            ViewBag.QuickLineItemId = new SelectList(quickLineItems, "QuickLineItemId", "Description");

            var viewModel = Mapper.Map<LineItem, LineItemEditViewModel>(lineItem);

            return View(viewModel);
        }

        // POST: /LineItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LineItemEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var lineItem = Mapper.Map<LineItemEditViewModel, LineItem>(viewModel);

                await _lineItemRepository.UpdateAsync(lineItem);
                return RedirectToAction("Edit", "Job", new { id = lineItem.JobId });
            }

            var job = await _jobRepository.GetAsync(viewModel.JobId);
            var technicians = await _staffRepository.GetAllAsync();
            var taxRates = await _taxRateRepository.GetAllAsync();
            var services = await _serviceRepository.GetAllAsync();
            var parts = await _partRepository.GetAllAsync();
            var quickLineItems = await _quickLineItemRepository.GetAllAsync();

            ViewBag.StaffId = new SelectList(technicians, "StaffId", "DisplayName", viewModel.StaffId);
            ViewBag.TaxRateId = new SelectList(taxRates, "TaxRateId", "DisplayName", viewModel.TaxRateId);
            ViewBag.ServiceId = new SelectList(services, "ServiceId", "Name");
            ViewBag.PartId = new SelectList(parts, "PartId", "DisplayName");
            ViewBag.QuickLineItemId = new SelectList(quickLineItems, "QuickLineItemId", "Description");

            return View(viewModel);
        }

        // GET: /LineItem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineItem lineItem = await _lineItemRepository.GetAsync(id);
            return View(lineItem);
        }

        // POST: /LineItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LineItem LineItem = await _lineItemRepository.GetAsync(id);
            int deletedLineJobId = LineItem.JobId;
            await _lineItemRepository.DeleteAsync(id);
            await RenumberLineItemsFor(deletedLineJobId);
            return RedirectToAction("Edit", "Job", new { id = deletedLineJobId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _lineItemRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task RenumberLineItemsFor(int jobId)
        {
            IEnumerable<LineItem> lineItems = await _lineItemRepository.GetAllByJobAsync(jobId);
            int lineNumber = 1;
            foreach (LineItem lineItem in lineItems)
            {
                lineItem.LineItemNumber = lineNumber;
                await _lineItemRepository.UpdateAsync(lineItem);
                lineNumber++;
            }
        }
    }
}
