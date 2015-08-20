using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository = null;
        private readonly IHowDidYouFindUsRepository _howDidYouFindUsRepository = null;
        private readonly IJobRepository _jobRepository = null;

        public CustomerController(ICustomerRepository customerRepository, IHowDidYouFindUsRepository howDidYouFindUsRepository, IJobRepository jobRepository)
        {
            if (customerRepository == null)
            {
                throw new ArgumentNullException("customerRepository");
            }

            if (howDidYouFindUsRepository == null)
            {
                throw new ArgumentNullException("howDidYouFindUsRepository");
            }

            if (jobRepository == null)
            {
                throw new ArgumentNullException("jobRepository");
            }

            _customerRepository = customerRepository;
            _howDidYouFindUsRepository = howDidYouFindUsRepository;
            _jobRepository = jobRepository;
        }

        // GET: /Customer/
        public async Task<ActionResult> Index(string designation, string nameLetter, int? page)
        {
            //var nameLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
            var nameLetters = from char c in "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray() select new {Name = c.ToString()};

            var designations = from CustomerDesignationEnum d in Enum.GetValues(typeof(CustomerDesignationEnum))
                               select new { ID = d, Name = d.ToString() };

            ViewBag.Designation = new SelectList(designations, "ID", "Name");
            ViewBag.NameLetters = new SelectList(nameLetters, "Name", "Name", nameLetter);

            if (string.IsNullOrEmpty(designation) == false) ViewBag.Title = designation;

            //pagination
            int pageNumber = page ?? 1;
            int pageSize = 25;
            int numberOfCustomers = await _customerRepository.GetCountForDesignationAndNameLetterAsync(designation, nameLetter);
            int numberOfPages = (int)Math.Ceiling((double)numberOfCustomers / pageSize);

            ViewBag.NumberOfPages = numberOfPages;
            ViewBag.PageNumber = pageNumber;
            ViewBag.NumberOfCustomers = numberOfCustomers;
            ViewBag.SelectedDesignation = designation;
            ViewBag.SelectedNameLetter = nameLetter;
            ViewBag.PageSize = pageSize;

            //var customers = await _customerRepository.GetAllByDesignationAndNameLetterAsync(designation, nameLetter);
            var customers = await _customerRepository.GetByDesignationAndNameLetterPagedAsync(designation, nameLetter, pageNumber, pageSize);

            var viewModel = Mapper.Map<IList<Customer>, IList<CustomerIndexViewModel>>(customers.ToList());

            return View(viewModel);
        }

        // GET: /Customer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await _customerRepository.GetAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: /Customer/Create
        public async Task<ActionResult> Create()
        {
            var designations = from CustomerDesignationEnum d in Enum.GetValues(typeof(CustomerDesignationEnum))
                               select new {ID = d, Name = d.ToString()};
            var howDidYouFindUses = await _howDidYouFindUsRepository.GetAllAsync();

            ViewBag.Designation = new SelectList(designations, "ID", "Name");
            ViewBag.HowDidYouFindUsId = new SelectList(howDidYouFindUses, "HowDidYouFindUsId", "Description");

            var viewModel = new CustomerCreateViewModel();

            return View(viewModel);
        }

        // POST: /Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = Mapper.Map<CustomerCreateViewModel, Customer>(viewModel);

                int newCustomerId = await _customerRepository.AddAsync(customer);
                return RedirectToAction("Edit", new { id = newCustomerId });
            }

            var designations = from CustomerDesignationEnum d in Enum.GetValues(typeof(CustomerDesignationEnum))
                               select new { ID = d, Name = d.ToString() };
            var howDidYouFindUses = await _howDidYouFindUsRepository.GetAllAsync();

            ViewBag.Designation = new SelectList(designations, "ID", "Name", viewModel.Designation);
            ViewBag.HowDidYouFindUsId = new SelectList(howDidYouFindUses, "HowDidYouFindUsId", "Description", viewModel.HowDidYouFindUsId);

            return View(viewModel);
        }

        // GET: /Customer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await _customerRepository.GetAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<Customer, CustomerEditViewModel>(customer);

            var designations = from CustomerDesignationEnum d in Enum.GetValues(typeof(CustomerDesignationEnum))
                               select new { ID = d, Name = d.ToString() };
            var howDidYouFindUses = await _howDidYouFindUsRepository.GetAllAsync();

            ViewBag.Designation = new SelectList(designations, "ID", "Name", viewModel.Designation);
            ViewBag.HowDidYouFindUsId = new SelectList(howDidYouFindUses, "HowDidYouFindUsId", "Description", viewModel.HowDidYouFindUsId);

            return View(viewModel);
        }

        // POST: /Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = Mapper.Map<CustomerEditViewModel, Customer>(viewModel);

                await _customerRepository.UpdateAsync(customer);
                return RedirectToAction("Dashboard", "Home");
            }

            var designations = from CustomerDesignationEnum d in Enum.GetValues(typeof(CustomerDesignationEnum))
                               select new { ID = d, Name = d.ToString() };
            var howDidYouFindUses = await _howDidYouFindUsRepository.GetAllAsync();

            ViewBag.Designation = new SelectList(designations, "ID", "Name", viewModel.Designation);
            ViewBag.HowDidYouFindUsId = new SelectList(howDidYouFindUses, "HowDidYouFindUsId", "Description", viewModel.HowDidYouFindUsId);

            //set child collections if validation failed
            var jobs = await _jobRepository.GetAllForCustomer(viewModel.CustomerId);
            var jobsViewModel = Mapper.Map<IList<Job>, IList<JobIndexViewModel>>(jobs.ToList());
            viewModel.Jobs = jobsViewModel;

            return View(viewModel);
        }

        // GET: /Customer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await _customerRepository.GetAsync(id);
            return View(customer);
        }

        // POST: /Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer Customer = await _customerRepository.GetAsync(id);
            await _customerRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetTermJsonAsync(string term)
        {
            var customers = await _customerRepository.GetTermAsync(term);

            return Json(customers.Select(x => new
            {
                CustomerId = x.CustomerId,
                DisplayName = x.DisplayName
            }) , JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetJsonAsync(int id)
        {
            var customer = await _customerRepository.GetAsync(id);

            var result = new
            {
                customer.DisplayName,
                customer.Address1,
                customer.Address2,
                customer.City,
                customer.State,
                customer.Zipcode,
                customer.FirstName,
                customer.LastName,
                customer.Phone1,
                customer.Phone2,
                customer.CustomerId
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _customerRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
