using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices;
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
    [Authorize]
    public class PaymentMethodController : Controller
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository = null;

        public PaymentMethodController(IPaymentMethodRepository paymentMethodRepository)
        {
            if (paymentMethodRepository == null)
            {
                throw new ArgumentNullException("paymentMethodRepository");
            }

            _paymentMethodRepository = paymentMethodRepository;
        }

        // GET: /PaymentMethod/
        public async Task<ActionResult> Index()
        {
            var jobCategories = await _paymentMethodRepository.GetAllAsync();
            var viewModel =
                Mapper.Map<IList<PaymentMethod>, IList<PaymentMethodIndexViewModel>>(jobCategories.OrderBy(r => r.SequenceNumber).ToList());
            return View(viewModel);
        }

        // GET: /PaymentMethod/Create
        public ActionResult Create()
        {
            var viewModel = new PaymentMethodCreateViewModel();
            return View(viewModel);
        }

        // POST: /PaymentMethod/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentMethodCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var paymentMethod = Mapper.Map<PaymentMethodCreateViewModel, PaymentMethod>(viewModel);

                var nextSequenceNumber = await _paymentMethodRepository.GetNextSequenceNumberAsync();
                paymentMethod.SequenceNumber = nextSequenceNumber;

                await _paymentMethodRepository.AddAsync(paymentMethod);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(IList<PaymentMethodIndexViewModel> viewModel)
        {
            bool isDistinct = SequencesAreDistinct(viewModel);

            if (isDistinct == false)
            {
                ModelState.AddModelError("SequencesNotDistinct", "Payment Method sequences must not repeat.");
            }

            if (ModelState.IsValid)
            {
                var paymentMethodes = Mapper.Map<IList<PaymentMethodIndexViewModel>, IList<PaymentMethod>>(viewModel);
                foreach (var item in paymentMethodes)
                {
                    await _paymentMethodRepository.UpdateAsync(item);
                }
                //Helper method to determine the 
                TempData["UpdateMessage"] = "Payment Methods updated successfully.";
                return RedirectToAction("Index");
            }
            //Error handling for it
            return View("Index", viewModel);
        }

        // GET: /PaymentMethod/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PaymentMethod paymentMethod = await _paymentMethodRepository.GetAsync(id);
            return View(paymentMethod);
        }

        // POST: /PaymentMethod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PaymentMethod PaymentMethod = await _paymentMethodRepository.GetAsync(id);
            await _paymentMethodRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool SequencesAreDistinct(IEnumerable<PaymentMethodIndexViewModel> paymentMethodes)
        {
            var sequenceNumbers = new List<int>();

            foreach (var paymentMethod in paymentMethodes)
            {
                if (sequenceNumbers.Contains(paymentMethod.SequenceNumber))
                {
                    return false;
                }
                else
                {
                    sequenceNumbers.Add(paymentMethod.SequenceNumber);
                }
            }

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _paymentMethodRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
