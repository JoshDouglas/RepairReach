using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.Core.Model;
using System.Threading.Tasks;
using System.Net;
using RepairReach.WebApplication.ViewModels;
using AutoMapper;

namespace RepairReach.WebApplication.Controllers
{
    public class ManagementController : Controller
    {
        private readonly IJobRepository _jobRepository = null;
        private readonly IActivityEventRepository _activityEventRepository = null;
        private readonly ICompanyRepository _companyRepository = null;

        public ManagementController(IJobRepository jobRepository, IActivityEventRepository activityEventRepository, ICompanyRepository companyRepository)
        {
            if (jobRepository == null)
            {
                throw new ArgumentNullException("jobRepository");
            }

            if (activityEventRepository == null)
            {
                throw new ArgumentNullException("activityEventRepository");
            }

            if (companyRepository == null)
            {
                throw new ArgumentNullException("companyRepository");
            }

            _jobRepository = jobRepository;
            _activityEventRepository = activityEventRepository;
            _companyRepository = companyRepository;
        }

        // GET: /Management/Receivables
        public async Task<ActionResult> Receivables()
        {
            ViewBag.Title = "Receivables";
            var jobs = await _jobRepository.GetAllClosedAsync();
            var receivableJobs = jobs.Where(j => j.BalanceDue > 0);
            return View(receivableJobs);
        }

        public async Task<ActionResult> ActivityLog(DateTime? startTime, DateTime? endTime, string createdBy)
        {
            //time zone stuff for now
            var company = await _companyRepository.GetFirstAsync();
            @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
            if (company != null) @ViewBag.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(company.TimeZoneInfo);

            IEnumerable<ActivityEvent> activityEvents = null;

            //default
            if (startTime.HasValue == false && endTime.HasValue == false && string.IsNullOrEmpty(createdBy) == true) 
                activityEvents = await _activityEventRepository.GetLastXAsync(100);

            //user only
            if (string.IsNullOrEmpty(createdBy) == false && startTime.HasValue == false && endTime.HasValue == false) 
                activityEvents = await _activityEventRepository.GetByDateName(null, null, createdBy);

            //date only
            if (startTime.HasValue && endTime.HasValue && string.IsNullOrEmpty(createdBy) == true) 
                activityEvents = await _activityEventRepository.GetByDateName(startTime, endTime, null);

            //user & date
            if (startTime.HasValue && endTime.HasValue && string.IsNullOrEmpty(createdBy) == false)
                activityEvents = await _activityEventRepository.GetByDateName(startTime, endTime, createdBy);

            var viewModel = Mapper.Map<IEnumerable<ActivityEvent>, IEnumerable<ManagementActivityLogViewModel>>(activityEvents);

            return View(viewModel);
        }
	}
}