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
using AutoMapper;
using RepairReach.WebApplication.ViewModels;

namespace RepairReach.WebApplication.Controllers
{
    public class VendorController : Controller
    {
        private readonly IVendorRepository _vendorRepository = null;

        public VendorController(IVendorRepository vendorRepository)
        {
            if (vendorRepository == null)
            {
                throw new ArgumentNullException("vendorRepository");
            }

            _vendorRepository = vendorRepository;
        }

        // GET: /Vendor/
        public async Task<ActionResult> Index()
        {
            var vendors = await _vendorRepository.GetAllAsync();
            var viewModel = Mapper.Map<IEnumerable<Vendor>, IEnumerable<VendorIndexViewModel>>(vendors);
            return View(viewModel);
        }

        // GET: /Vendor/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = await _vendorRepository.GetAsync(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        // GET: /Vendor/Create
        public ActionResult Create()
        {
            var viewModel = new VendorCreateViewModel();
            return View(viewModel);
        }

        // POST: /Vendor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VendorCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vendor = Mapper.Map<VendorCreateViewModel, Vendor>(viewModel);
                await _vendorRepository.AddAsync(vendor);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /Vendor/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = await _vendorRepository.GetAsync(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Vendor, VendorEditViewModel>(vendor);

            return View(viewModel);
        }

        // POST: /Vendor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(VendorEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var vendor = Mapper.Map<VendorEditViewModel, Vendor>(viewModel);
                await _vendorRepository.UpdateAsync(vendor);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: /Vendor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendor vendor = await _vendorRepository.GetAsync(id);
            return View(vendor);
        }

        // POST: /Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Vendor vendor = await _vendorRepository.GetAsync(id);
            await _vendorRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vendorRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
