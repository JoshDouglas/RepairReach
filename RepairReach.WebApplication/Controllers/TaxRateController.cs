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

namespace RepairReach.WebApplication.Controllers
{
    [Authorize]
    public class TaxRateController : Controller
    {
        private readonly ITaxRateRepository _taxRateRepository = null;

        public TaxRateController(ITaxRateRepository taxRateRepository)
        {
            if (taxRateRepository == null)
            {
                throw new ArgumentNullException("taxRateRepository");
            }

            _taxRateRepository = taxRateRepository;
        }

        // GET: /TaxRate/
        public async Task<ActionResult> Index()
        {
            var taxRates = await _taxRateRepository.GetAllAsync();
            var viewModel = Mapper.Map<IList<TaxRate>, IList<TaxRateIndexViewModel>>(taxRates.ToList());
            return View(viewModel);
        }

        // GET: /TaxRate/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxRate taxRate = await _taxRateRepository.GetAsync(id);
            if (taxRate == null)
            {
                return HttpNotFound();
            }
            return View(taxRate);
        }

        // GET: /TaxRate/Create
        public ActionResult Create()
        {
            var viewModel = new TaxRateCreateViewModel();
            return View(viewModel);
        }

        // POST: /TaxRate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TaxRateCreateViewModel viewModel)

        {
            if (ModelState.IsValid)
            {
                var taxRate = Mapper.Map<TaxRateCreateViewModel, TaxRate>(viewModel);

                int newTaxRateId = await _taxRateRepository.AddAsync(taxRate);
                if (taxRate.IsDefaultRate == true) await UpdateTaxRatesDefaultsFalse(newTaxRateId);
                
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /TaxRate/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxRate taxRate = await _taxRateRepository.GetAsync(id);
            if (taxRate == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<TaxRate, TaxRateEditViewModel>(taxRate);

            return View(viewModel);
        }

        // POST: /TaxRate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TaxRateEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var taxRate = Mapper.Map<TaxRateEditViewModel, TaxRate>(viewModel);

                await _taxRateRepository.UpdateAsync(taxRate);
                if (taxRate.IsDefaultRate == true) await UpdateTaxRatesDefaultsFalse(taxRate.TaxRateId);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /TaxRate/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxRate taxRate = await _taxRateRepository.GetAsync(id);
            return View(taxRate);
        }

        // POST: /TaxRate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TaxRate TaxRate = await _taxRateRepository.GetAsync(id);
            await _taxRateRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _taxRateRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<bool> DefaultRateSetBefore(int id)
        {
            bool defaultRateIsSet = false;
            IEnumerable<TaxRate> taxRates = await _taxRateRepository.GetAllAsync();

            foreach (TaxRate taxRate in taxRates)
            {
                if (taxRate.IsDefaultRate == true && taxRate.TaxRateId != id)
                {
                    defaultRateIsSet = true;
                }
            }

            return defaultRateIsSet;
        }

        private async Task UpdateTaxRatesDefaultsFalse(int id)
        {
            IEnumerable<TaxRate> taxRates = await _taxRateRepository.GetAllAsync();
            foreach (TaxRate taxRate in taxRates)
            {
                if (taxRate.TaxRateId != id)
                {
                    taxRate.IsDefaultRate = false;
                    await _taxRateRepository.UpdateAsync(taxRate);
                }
            }
        }
    }
}
