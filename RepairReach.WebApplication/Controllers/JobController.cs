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
using RepairReach.Core.Enum;
using RepairReach.Core.Model;
using RepairReach.Core.Service;
using RepairReach.Data.Repositories;
using RepairReach.Data;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.ViewModels;
using Rotativa;
using Rotativa.Options;
using System.IO;
using System.IO.Compression;
using RepairReach.WebApplication.Mailer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RepairReach.Data.Infrastructure.Identity;

namespace RepairReach.WebApplication.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobRepository _jobRepository = null;
        private readonly ICustomerRepository _customerRepository = null;
        private readonly IJobCategoryRepository _jobCategoryRepository = null;
        private readonly IJobStatusRepository _jobStatusRepository = null;
        private readonly IStaffRepository _staffRepository = null;
        private readonly ICompanyRepository _companyRepository = null;
        private readonly IActivityEventRepository _activityEventRepository = null;
        private readonly IGeocodingService _geocodingService;
        private readonly IApplianceRepository _applianceRepository = null;
        private readonly IAppointmentRepository _appointmentRepository = null;
        private readonly IJobNoteRepository _jobNoteRepository = null;
        private readonly ILineItemRepository _lineItemRepository = null;
        private readonly IPaymentRepository _paymentRepository = null;

        public JobController(IJobRepository jobRepository, ICustomerRepository customerRepository, 
            IJobCategoryRepository jobCategoryRepository, IJobStatusRepository jobStatusRepository, 
            IStaffRepository staffRepository, ICompanyRepository companyRepository, IActivityEventRepository activityEventRepository, 
            IGeocodingService geocodingService, IApplianceRepository applianceRepository, IAppointmentRepository appointmentRepository,
            IJobNoteRepository jobNoteRepository, ILineItemRepository lineItemRepository, IPaymentRepository paymentRepository)
        {
            if (jobRepository == null)
            {
                throw new ArgumentNullException("jobRepository");
            }

            if (customerRepository == null)
            {
                throw new ArgumentNullException("customerRepository");
            }

            if (jobCategoryRepository == null)
            {
                throw new ArgumentNullException("jobCategoryRepository");
            }

            if (jobStatusRepository == null)
            {
                throw new ArgumentNullException("jobStatusRepository");
            }

            if (staffRepository == null)
            {
                throw new ArgumentNullException("staffRepository");
            }

            if (companyRepository == null)
            {
                throw new ArgumentNullException("companyRepository");
            }

            if (activityEventRepository == null)
            {
                throw new ArgumentNullException("activityEventRepository");
            }

            if (applianceRepository == null)
            {
                throw new ArgumentNullException("applianceRepository");
            }

            if (appointmentRepository == null)
            {
                throw new ArgumentNullException("appointmentRepository");
            }

            if (jobNoteRepository == null)
            {
                throw new ArgumentNullException("jobNoteRepository");
            }

            if (lineItemRepository == null)
            {
                throw new ArgumentNullException("lineItemRepository");
            }

            if (paymentRepository == null)
            {
                throw new ArgumentNullException("paymentRepository");
            }

            _jobRepository = jobRepository;
            _customerRepository = customerRepository;
            _jobCategoryRepository = jobCategoryRepository;
            _jobStatusRepository = jobStatusRepository;
            _staffRepository = staffRepository;
            _companyRepository = companyRepository;
            _activityEventRepository = activityEventRepository;
            _applianceRepository = applianceRepository;
            _appointmentRepository = appointmentRepository;
            _jobNoteRepository = jobNoteRepository;
            _lineItemRepository = lineItemRepository;
            _paymentRepository = paymentRepository;

            _geocodingService = geocodingService;
        }

        // GET: /Job/
        public async Task<ActionResult> Index(string status, string category, string jobSubType, bool? isClosed, int? page)
        {
            var jobStatuses = await _jobStatusRepository.GetAllAsync();
            ViewBag.JobStatusId = new SelectList(jobStatuses, "Description", "Description", status); //no id beacuse the val is for redirect

            @ViewBag.IsClosed = false;
            if (isClosed.HasValue) @ViewBag.IsClosed = isClosed.Value;

            if (string.IsNullOrEmpty(status) == false) ViewBag.Title = status;
            if (isClosed.HasValue)
            {
                if (isClosed.Value == true && string.IsNullOrEmpty(status) == false)
                {
                    @ViewBag.Title = "Closed " + status;
                }
                else if (isClosed.Value == true) ViewBag.Title = "Closed";
            }

            //OLD
            //if (string.IsNullOrEmpty(status) == false)
            //{
            //    ViewBag.Title = status;
            //    return View(await _jobRepository.GetAllByStatusAsync(status));
            //}

            //if (string.IsNullOrEmpty(category) == false)
            //{
            //    ViewBag.Title = category;
            //    return View(await _jobRepository.GetAllByCategoryAsync(category));
            //}

            //if (string.IsNullOrEmpty(jobSubType) == false)
            //{
            //    ViewBag.Title = jobSubType;
            //    return View(await _jobRepository.GetAllByJobSubTypeAsync(jobSubType));
            //}

            //if (isClosed.HasValue == false) return View(await _jobRepository.GetAllOpenAsync());
            //if (isClosed.Value == true)
            //{
            //    ViewBag.Title = "Closed";
            //    return View(await _jobRepository.GetAllClosedAsync());
            //}

            //return View(await _jobRepository.GetAllByStatusAndClosedAsync(status, isClosed));
            //END OLD

            //pagination
            int pageNumber = page ?? 1;
            int pageSize = 25;
            int numberOfJobs = await _jobRepository.GetCountForStatusAsync(status);
            int numberOfPages = (int)Math.Ceiling((double)numberOfJobs / pageSize);

            ViewBag.NumberOfPages = numberOfPages;
            ViewBag.PageNumber = pageNumber;
            ViewBag.NumberOfJobs = numberOfJobs;
            ViewBag.SelectedStatus = status;
            ViewBag.PageSize = pageSize;

            var jobs = await _jobRepository.GetByStatusPagedAsync(status, pageNumber, pageSize);
            var viewModel = Mapper.Map<IEnumerable<Job>, IEnumerable<JobIndexViewModel>>(jobs);

            return View(viewModel);
        }

        // GET: /Job/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await _jobRepository.GetAsync(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: /Job/Create
        public async Task<ActionResult> Create(int? customerId)
        {
            var viewModel = new JobCreateViewModel();
            viewModel.JobNumber = await _jobRepository.GetMaxJobNumber() + 1;

            //var customers = await _customerRepository.GetAllAsync();
            var jobCategories = await _jobCategoryRepository.GetAllAsync();
            var jobStatuses = await _jobStatusRepository.GetAllAsync();
            var staff = await _staffRepository.GetAllAsync();

            //coming from customer - pre-populate customer if individual
            Customer customer = new Customer();
            if (customerId.HasValue == true)
            {
                customer = await _customerRepository.GetAsync(customerId.Value);
                if (customer.Designation == CustomerDesignationEnum.Individual)
                {
                    viewModel.Address1 = customer.Address1;
                    viewModel.Address2 = customer.Address2;
                    viewModel.City = customer.City;
                    viewModel.State = customer.State;
                    viewModel.Zipcode = customer.Zipcode;
                    viewModel.ContactFirstName = customer.FirstName;
                    viewModel.ContactLastName = customer.LastName;
                    viewModel.ContactPhone1 = customer.Phone1;
                    viewModel.ContactPhone2 = customer.Phone2;
                    viewModel.CustomerId = customer.CustomerId;
                    viewModel.CustomerDisplayName = customer.DisplayName;
                }
            }

            //ViewBag.CustomerId = new SelectList(customers, "CustomerId", "DisplayName", customer.CustomerId);
            ViewBag.JobCategoryId = new SelectList(jobCategories, "JobCategoryId", "Description");
            ViewBag.JobStatusId = new SelectList(jobStatuses, "JobStatusId", "Description");
            ViewBag.StaffId = new SelectList(staff, "StaffId", "DisplayName");
            return View(viewModel);
        }

        // POST: /Job/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobCreateViewModel viewModel)
        {
            //if there is no customer we should check if they exist and assign, or create a new one (individual types only)
            if (viewModel.CustomerId == 0)
            {
                //does customer exist?
                var foundCustomers = await _customerRepository.GetAllIndividualByFirstLast(viewModel.ContactFirstName,
                    viewModel.ContactLastName);

                //yes - assign it
                if (foundCustomers.Any())
                {
                    viewModel.CustomerId = foundCustomers.First().CustomerId;
                }
                else //no - create it
                {
                    var newCustomer = new Customer();
                    newCustomer.Designation = CustomerDesignationEnum.Individual;
                    newCustomer.FirstName = viewModel.ContactFirstName;
                    newCustomer.LastName = viewModel.ContactLastName;
                    newCustomer.Address1 = viewModel.Address1;
                    newCustomer.Address2 = viewModel.Address2;
                    newCustomer.City = viewModel.City;
                    newCustomer.State = viewModel.State;
                    newCustomer.Zipcode = viewModel.Zipcode;
                    newCustomer.Phone1 = viewModel.ContactPhone1;
                    newCustomer.Phone2 = viewModel.ContactPhone2;
                    var newCustomerId = await _customerRepository.AddAsync(newCustomer);
                    viewModel.CustomerId = newCustomerId;
                }
            }
    
            if (ModelState.IsValid)
            {
                var job = Mapper.Map<JobCreateViewModel, Job>(viewModel);

                job.JobCreated = DateTime.UtcNow;
                if (job.IsAuthorized) job.JobAuthorized = DateTime.UtcNow;

                //get current user
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
                ApplicationUser user = new ApplicationUser();
                user = await userManager.FindByNameAsync(User.Identity.Name);

                job.LastViewedTime = DateTime.UtcNow;
                job.LastViewedBy = user.Staff.DisplayName;

                job.Location = await _geocodingService.GetLocation(job.FullAddress);

                int newJobId = await _jobRepository.AddAsync(job);
                return RedirectToAction("Edit", new {id = newJobId});
            }

            var jobCategories = await _jobCategoryRepository.GetAllAsync();
            var jobStatuses = await _jobStatusRepository.GetAllAsync();
            var staff = await _staffRepository.GetAllAsync();

            ViewBag.JobCategoryId = new SelectList(jobCategories, "JobCategoryId", "Description", viewModel.JobCategoryId);
            ViewBag.JobStatusId = new SelectList(jobStatuses, "JobStatusId", "Description", viewModel.JobStatusId);
            ViewBag.StaffId = new SelectList(staff, "StaffId", "DisplayName", viewModel.StaffId);

            return View(viewModel);
        }

        // GET: /Job/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Job job = await _jobRepository.GetAsync(id);

            if (job == null)
            {
                return HttpNotFound();
            }

            //get current user
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
            ApplicationUser user = new ApplicationUser();
            user = await userManager.FindByNameAsync(User.Identity.Name);

            //update last viewed
            job.LastViewedTime = DateTime.UtcNow;
            job.LastViewedBy = user.Staff.DisplayName;
            await _jobRepository.UpdateAsync(job);

            //time zone azure stuff
            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);
            ViewBag.TimeZoneInfo = timeZoneInfo;

            //can send emails?
            bool canSendEmails = (string.IsNullOrEmpty(job.Customer.Email) == false && string.IsNullOrEmpty(company.Email) == false);
            ViewBag.CanSendEmails = canSendEmails;

            var jobCategories = await _jobCategoryRepository.GetAllAsync();
            var jobStatuses = await _jobStatusRepository.GetAllAsync();
            var staff = await _staffRepository.GetAllAsync();

            var viewModel = Mapper.Map<Job, JobEditViewModel>(job);
            viewModel.ActivityEvents = viewModel.ActivityEvents.OrderByDescending(a => a.EventTime);

            ViewBag.JobCategoryId = new SelectList(jobCategories, "JobCategoryId", "Description", viewModel.JobCategoryId);
            ViewBag.JobStatusId = new SelectList(jobStatuses, "JobStatusId", "Description", viewModel.JobStatusId);
            ViewBag.JobQuickStatusId = new SelectList(jobStatuses, "JobStatusId", "Description", viewModel.JobStatusId);
            ViewBag.StaffId = new SelectList(staff, "StaffId", "DisplayName", viewModel.StaffId);

            return View(viewModel);
        }

        // POST: /Job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(JobEditViewModel viewModel)
        {
            var createActivityEventStatusChanged = false;
            var jobBefore = await _jobRepository.GetAsync(viewModel.JobId);
            var jobBeforeStatusId = jobBefore.JobStatusId;
            if (jobBeforeStatusId != viewModel.JobStatusId) createActivityEventStatusChanged = true;

            if (ModelState.IsValid)
            {
                var job = Mapper.Map<JobEditViewModel, Job>(viewModel);

                //geolocation
                job.Location = await _geocodingService.GetLocation(job.FullAddress);

                //if status is closed set the job closed timestamp
                var selectedStatus = await _jobStatusRepository.GetAsync(job.JobStatusId);
                if (job.JobClosed.HasValue == false && selectedStatus.Description.ToLower().Equals("closed") == true)
                {
                    job.JobClosed = DateTime.UtcNow;
                }

                //if changing from closed to something else
                if (job.JobClosed.HasValue == true && selectedStatus.Description.ToLower().Equals("closed") == false)
                {
                    job.JobClosed = null;
                }

                await _jobRepository.UpdateAsync(job);
                
                if (createActivityEventStatusChanged == true)
                {
                    //get current user
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
                    ApplicationUser user = new ApplicationUser();
                    user = await userManager.FindByNameAsync(User.Identity.Name);

                    var beforeStatus = await _jobStatusRepository.GetAsync(jobBeforeStatusId);
                    var afterStatus = await _jobStatusRepository.GetAsync(job.JobStatusId);

                    var activityEvent = new ActivityEvent();
                    activityEvent.JobId = job.JobId;
                    activityEvent.EventTime = DateTime.UtcNow;
                    activityEvent.Description = "Status changed from " + beforeStatus.Description + " to " + afterStatus.Description;
                    activityEvent.CausedBy = user.Staff.DisplayName;
                    await _activityEventRepository.AddAsync(activityEvent);
                }

                return RedirectToAction("Dashboard", "Home");
            }

            //time zone azure stuff
            var company = await _companyRepository.GetFirstAsync();
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);
            ViewBag.TimeZoneInfo = timeZoneInfo;

            var jobCategories = await _jobCategoryRepository.GetAllAsync();
            var jobStatuses = await _jobStatusRepository.GetAllAsync();
            var staff = await _staffRepository.GetAllAsync();

            ViewBag.JobCategoryId = new SelectList(jobCategories, "JobCategoryId", "Description", viewModel.JobCategoryId);
            ViewBag.JobStatusId = new SelectList(jobStatuses, "JobStatusId", "Description", viewModel.JobStatusId);
            ViewBag.StaffId = new SelectList(staff, "StaffId", "DisplayName", viewModel.StaffId);

            //set child collections if validation failed for the view
            var appliances = await _applianceRepository.GetAllForJobAsync(viewModel.JobId);
            var appointments = await _appointmentRepository.GetForJobAsync(viewModel.JobId);
            var jobNotes = await _jobNoteRepository.GetForJobAsync(viewModel.JobId);
            var lineItems = await _lineItemRepository.GetAllByJobAsync(viewModel.JobId);
            var payments = await _paymentRepository.GetForJobAsync(viewModel.JobId);
            var activityEvents = await _activityEventRepository.GetForJobAsync(viewModel.JobId);

            var applianceIndexViewModel = Mapper.Map<IList<Appliance>, IList<ApplianceIndexViewModel>>(appliances.ToList());
            var appointmentIndexViewModel = Mapper.Map<IList<Appointment>, IList<AppointmentIndexViewModel>>(appointments.ToList());
            var jobNoteIndexViewModel = Mapper.Map<IList<JobNote>, IList<JobNoteIndexViewModel>>(jobNotes.ToList());
            var lineItemIndexViewModel = Mapper.Map<IList<LineItem>, IList<LineItemIndexViewModel>>(lineItems.ToList());
            var paymentIndexViewModel = Mapper.Map<IList<Payment>, IList<PaymentIndexViewModel>>(payments.ToList());
            var activityEventViewModel = Mapper.Map<IList<ActivityEvent>, IList<JobEditActivityEventViewModel>>(activityEvents.ToList());

            viewModel.Appliances = applianceIndexViewModel;
            viewModel.Appointments = appointmentIndexViewModel;
            viewModel.JobNotes = jobNoteIndexViewModel;
            viewModel.LineItems = lineItemIndexViewModel;
            viewModel.Payments = paymentIndexViewModel;
            viewModel.ActivityEvents = activityEventViewModel.OrderByDescending(a => a.EventTime);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateStatus(int jobId, int jobQuickStatusId)
        {
            var job = await _jobRepository.GetAsync(jobId);

            if (job == null)
            {
                return HttpNotFound();
            }

            var jobBeforeStatusId = job.JobStatusId;
            var createActivityEventStatusChanged = jobBeforeStatusId != jobQuickStatusId;

            job.JobStatusId = jobQuickStatusId;

            //if status is closed set the job closed timestamp
            var selectedStatus = await _jobStatusRepository.GetAsync(job.JobStatusId);
            if (job.JobClosed.HasValue == false && selectedStatus.Description.ToLower().Equals("closed") == true)
            {
                job.JobClosed = DateTime.UtcNow;
            }

            //if changing from closed to something else
            if (job.JobClosed.HasValue == true && selectedStatus.Description.ToLower().Equals("closed") == false)
            {
                job.JobClosed = null;
            }

            await _jobRepository.UpdateAsync(job);

            if (createActivityEventStatusChanged == true)
            {
                //get current uesr
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
                ApplicationUser user = new ApplicationUser();
                user = await userManager.FindByNameAsync(User.Identity.Name);

                var beforeStatus = await _jobStatusRepository.GetAsync(jobBeforeStatusId);
                var afterStatus = await _jobStatusRepository.GetAsync(jobQuickStatusId);

                var activityEvent = new ActivityEvent();
                activityEvent.JobId = job.JobId;
                activityEvent.EventTime = DateTime.UtcNow;
                activityEvent.Description = "Status changed from " + beforeStatus.Description + " to " + afterStatus.Description;
                activityEvent.CausedBy = user.Staff.DisplayName;
                await _activityEventRepository.AddAsync(activityEvent);
            }

            return RedirectToAction("Dashboard", "Home");
        }

        // GET: /Job/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = await _jobRepository.GetAsync(id);
            return View(job);
        }

        // POST: /Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Job Job = await _jobRepository.GetAsync(id);
            await _jobRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Estimate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(id);
            ViewBag.Company = await _companyRepository.GetFirstAsync();

            return View(job);
        }

        public async Task<ActionResult> EstimatePdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(id);
            ViewBag.Company = await _companyRepository.GetFirstAsync();

            return new ViewAsPdf(job);
        }

        public async Task<ActionResult> RepairOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(id);
            ViewBag.Company = await _companyRepository.GetFirstAsync();

            return View(job);
        }

        public async Task<ActionResult> RepairOrderPdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(id);
            ViewBag.Company = await _companyRepository.GetFirstAsync();

            return new ViewAsPdf(job);
        }

        public async Task<ActionResult> Invoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(id);
            ViewBag.Company = await _companyRepository.GetFirstAsync();

            return View(job);
        }

        public async Task<ActionResult> InvoicePdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(id);
            ViewBag.Company = await _companyRepository.GetFirstAsync();

            return new ViewAsPdf(job);
        }

        public async Task<ActionResult> PaymentReceipt(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(id);
            ViewBag.Company = await _companyRepository.GetFirstAsync();

            return View(job);
        }

        public async Task<ActionResult> PaymentReceiptPdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(id);
            ViewBag.Company = await _companyRepository.GetFirstAsync();

            return new ViewAsPdf(job);
        }

        public async Task<ActionResult> RepairAuthorization(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(id);
            ViewBag.Company = await _companyRepository.GetFirstAsync();

            return View(job);
        }

        public async Task<ActionResult> RepairAuthorizationPdf(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(id);
            ViewBag.Company = await _companyRepository.GetFirstAsync();

            return new ViewAsPdf(job);
        }

        [Authorize]
        public async Task<ActionResult> InvoiceEmail(int? jobId)
        {
            if (jobId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var job = await _jobRepository.GetAsync(jobId);

            //save pdf to server so we can attach it
            const string directory = "~/Content/flat/pdf/";
            if (!Directory.Exists(Server.MapPath(directory)))
            {
                Directory.CreateDirectory(Server.MapPath(directory));
            }

            var fileName = "Invoice #" + job.JobNumber + ".pdf";
            var filePath = Path.Combine(Server.MapPath(directory), fileName);
            string pdfUrl = Url.Action("InvoicePdf", "Job", new { id = jobId }, Request.Url.Scheme);

            SaveHttpResponseAsFile(pdfUrl, filePath);

            //send the email
            var company = await _companyRepository.GetFirstAsync();
            ITransactionMailer mailer = new MandrillMailer();
            bool emailSent = await mailer.SendInvoiceEmailAsync(job.Customer.Email, company.Email, company.Name, filePath);

            ViewBag.EmailSent = emailSent;
            ViewBag.JobId = job.JobId;
            ViewBag.CustomerEmail = job.Customer.Email;

            //delete the pdf
            System.IO.File.Delete(filePath);

            //TODO: change this to some regular view or notify some other way
            return View();
        }

        //source: http://stackoverflow.com/questions/11854642/how-do-i-intercept-the-output-stream-of-the-current-actionresult-in-net-mvc3
        public static void SaveHttpResponseAsFile(string RequestUrl, string FilePath)
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(RequestUrl);
                httpRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                httpRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)httpRequest.GetResponse();
                }
                catch (System.Net.WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                        response = (HttpWebResponse)ex.Response;
                }

                using (Stream responseStream = response.GetResponseStream())
                {
                    Stream FinalStream = responseStream;
                    if (response.ContentEncoding.ToLower().Contains("gzip"))
                        FinalStream = new GZipStream(FinalStream, CompressionMode.Decompress);
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))
                        FinalStream = new DeflateStream(FinalStream, CompressionMode.Decompress);

                    using (var fileStream = System.IO.File.Create(FilePath))
                    {
                        FinalStream.CopyTo(fileStream);
                    }

                    response.Close();
                    FinalStream.Close();
                }
            }
            catch
            { }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _jobRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
