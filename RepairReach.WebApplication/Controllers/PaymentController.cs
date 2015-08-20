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
using RepairReach.WebApplication.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RepairReach.Data.Infrastructure.Identity;

namespace RepairReach.WebApplication.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository = null;
        private readonly IActivityEventRepository _activityEventRepository = null;
        private readonly IJobRepository _jobRepository = null;
        private readonly IPaymentMethodRepository _paymentMethodRepository = null;
        private readonly IStaffRepository _staffRepository = null;

        public PaymentController(IPaymentRepository paymentRepository, IActivityEventRepository activityEventRepository, 
            IJobRepository jobRepository, IPaymentMethodRepository paymentMethodRepository, IStaffRepository staffRepository)
        {
            if (paymentRepository == null)
            {
                throw new ArgumentNullException("paymentRepository");
            }

            if (activityEventRepository == null)
            {
                throw new ArgumentNullException("activityEventRepository");
            }

            if (jobRepository == null)
            {
                throw new ArgumentNullException("jobRepository");
            }

            if (paymentMethodRepository == null)
            {
                throw new ArgumentNullException("paymentMethodRepository");
            }

            if (staffRepository == null)
            {
                throw new ArgumentNullException("staffRepository");
            }

            _paymentRepository = paymentRepository;
            _activityEventRepository = activityEventRepository;
            _jobRepository = jobRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _staffRepository = staffRepository;
        }

        // GET: /Payment/
        public async Task<ActionResult> Index()
        {
            return View(await _paymentRepository.GetAllAsync());
        }

        // GET: /Payment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await _paymentRepository.GetAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: /Payment/Create
        public async Task<ActionResult> Create(int jobId)
        {
            var paymentMethods = await _paymentMethodRepository.GetAllAsync();
            ViewBag.PaymentMethodId = new SelectList(paymentMethods.OrderBy(p => p.SequenceNumber), "PaymentMethodId", "Description");

            var viewModel = new PaymentCreateViewModel();
            viewModel.JobId = jobId;
            viewModel.DatePaid = DateTime.Today;
            
            //default amount is balance due
            var job = await _jobRepository.GetAsync(jobId);
            if (job != null) viewModel.PaymentAmount = job.BalanceDue;

            return View(viewModel);
        }

        // POST: /Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var payment = Mapper.Map<PaymentCreateViewModel, Payment>(viewModel);

                //default user
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
                ApplicationUser user = new ApplicationUser();
                user = await userManager.FindByNameAsync(User.Identity.Name);

                payment.EnteredBy = user.Staff.DisplayName;
                await _paymentRepository.AddAsync(payment);

                var activityEvent = new ActivityEvent();
                activityEvent.JobId = payment.JobId;
                activityEvent.EventTime = DateTime.UtcNow;
                activityEvent.Description = "Payment added in the amount of " + payment.PaymentAmount.ToString("C");
                activityEvent.CausedBy = user.Staff.DisplayName;
                await _activityEventRepository.AddAsync(activityEvent);

                return RedirectToAction("Edit", "Job", new { id = payment.JobId });
            }

            var paymentMethods = await _paymentMethodRepository.GetAllAsync();
            ViewBag.PaymentMethodId = new SelectList(paymentMethods.OrderBy(p => p.SequenceNumber), "PaymentMethodId", "Description", viewModel.PaymentMethodId);

            return View(viewModel);
        }

        // GET: /Payment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await _paymentRepository.GetAsync(id);
            if (payment == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Payment, PaymentEditViewModel>(payment);

            var paymentMethods = await _paymentMethodRepository.GetAllAsync();
            ViewBag.PaymentMethodId = new SelectList(paymentMethods.OrderBy(p => p.SequenceNumber), "PaymentMethodId", "Description", viewModel.PaymentMethodId);

            return View(viewModel);
        }

        // POST: /Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PaymentEditViewModel viewModel)
        {
            var activityEventPaymentUpdated = false;
            var previousPayment = await _paymentRepository.GetAsync(viewModel.PaymentId);
            var previousPaymentAmount = previousPayment.PaymentAmount;
            if (viewModel.PaymentAmount != previousPaymentAmount) activityEventPaymentUpdated = true;

            if (ModelState.IsValid)
            {
                var payment = Mapper.Map<PaymentEditViewModel, Payment>(viewModel);

                await _paymentRepository.UpdateAsync(payment);

                if (activityEventPaymentUpdated == true)
                {
                    //get current user
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
                    ApplicationUser user = new ApplicationUser();
                    user = await userManager.FindByNameAsync(User.Identity.Name);

                    var activityEvent = new ActivityEvent();
                    activityEvent.JobId = payment.JobId;
                    activityEvent.EventTime = DateTime.UtcNow;
                    activityEvent.Description = "Payment updated from the amount of " + previousPaymentAmount.ToString("C") + " to the amount of " + payment.PaymentAmount.ToString("C");
                    activityEvent.CausedBy = user.Staff.DisplayName;
                    await _activityEventRepository.AddAsync(activityEvent);
                }

                return RedirectToAction("Edit", "Job", new { id = payment.JobId });
            }

            var paymentMethods = await _paymentMethodRepository.GetAllAsync();
            ViewBag.PaymentMethodId = new SelectList(paymentMethods.OrderBy(p => p.SequenceNumber), "PaymentMethodId", "Description", viewModel.PaymentMethodId);

            return View(viewModel);
        }

        // GET: /Payment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = await _paymentRepository.GetAsync(id);
            return View(payment);
        }

        // POST: /Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Payment payment = await _paymentRepository.GetAsync(id);
            decimal deletedPaymentAmount = payment.PaymentAmount;
            var deletedPaymentJobId = payment.JobId;

            await _paymentRepository.DeleteAsync(id);

            //get current user
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
            ApplicationUser user = new ApplicationUser();
            user = await userManager.FindByNameAsync(User.Identity.Name);

            var activityEvent = new ActivityEvent();
            activityEvent.JobId = deletedPaymentJobId;
            activityEvent.EventTime = DateTime.UtcNow;
            activityEvent.Description = "Payment removed for the amount of " + deletedPaymentAmount.ToString("C");
            activityEvent.CausedBy = user.Staff.DisplayName;
            await _activityEventRepository.AddAsync(activityEvent);

            return RedirectToAction("Edit", "Job", new { id = deletedPaymentJobId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _paymentRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        private IEnumerable<string> GetPaymentMethods()
        {
            var paymentMethods = new List<string> {"Adjustment", "Cash", "Check", "VISA", "MasterCard", "Discover", "EFT", "Credit Card"};
            return paymentMethods;
        }
    }
}
