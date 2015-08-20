using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepairReach.Core.Model;
using RepairReach.Data.Extensions;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.ViewModels.Reports;
using System.Threading.Tasks;

namespace RepairReach.WebApplication.Controllers
{
    public class EstimateReportController : Controller
    {
        private readonly IJobRepository _jobRepository = null;

        public EstimateReportController(IJobRepository jobRepository)
        {
            if (jobRepository == null)
            {
                throw new ArgumentNullException("jobRepository");
            }

            _jobRepository = jobRepository;
        }

        //
        // GET: /EstimateReport/
        public ActionResult NonAuthorizedJobs()
        {
            var filterViewModel = new NonAuthorizedJobsViewModel();
            //TODO: utc conversion
            filterViewModel.BeginDate = DateTime.UtcNow.GetStartOfMonth();
            filterViewModel.EndDate = DateTime.UtcNow.GetEndOfMonth();
            filterViewModel.ShowAll = false;
            filterViewModel.Jobs = new List<Job>();

            return View(filterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NonAuthorizedJobs(NonAuthorizedJobsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var reportJobs = await _jobRepository.GetNonAuthorized(viewModel.BeginDate, viewModel.EndDate,
                    viewModel.ShowAll);

                viewModel.Jobs = reportJobs;

                return View(viewModel);
            }

            return View(viewModel);
        }
	}
}