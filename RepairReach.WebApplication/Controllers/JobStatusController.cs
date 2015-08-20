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
    public class JobStatusController : Controller
    {
        private readonly IJobStatusRepository _jobStatusRepository = null;

        public JobStatusController(IJobStatusRepository jobStatusRepository)
        {
            if (jobStatusRepository == null)
            {
                throw new ArgumentNullException("jobStatusRepository");
            }

            _jobStatusRepository = jobStatusRepository;
        }

        // GET: /JobStatus/
        public async Task<ActionResult> Index()
        {
            var jobStatuses = await _jobStatusRepository.GetAllAsync();
            var viewModel = Mapper.Map<IList<JobStatus>, IList<JobStatusIndexViewModel>>(jobStatuses.OrderBy(r => r.SequenceNumber).ToList());
            return View(viewModel);
        }

        // GET: /JobStatus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobStatus jobStatus = await _jobStatusRepository.GetAsync(id);
            if (jobStatus == null)
            {
                return HttpNotFound();
            }
            return View(jobStatus);
        }

        // GET: /JobStatus/Create
        public ActionResult Create()
        {
            var viewModel = new JobStatusCreateViewModel();
            return View(viewModel);
        }

        // POST: /JobStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobStatusCreateViewModel viewModel)
        {
            if (viewModel.Description.ToLower().Equals("scheduled") || viewModel.Description.ToLower().Equals("closed") || viewModel.Description.ToLower().Equals("reschedule")
                || viewModel.Description.ToLower().Equals("needs approval") || viewModel.Description.ToLower().Equals("awaiting payment") || viewModel.Description.ToLower().Equals("on hold"))
            {
                ModelState.AddModelError("CreateMandatoryStatus", "The status " + viewModel.Description + " is mandatory and already exists.");
            }

            if (ModelState.IsValid)
            {
                var jobStatus = Mapper.Map<JobStatusCreateViewModel, JobStatus>(viewModel);

                var nextSequenceNumber = await _jobStatusRepository.GetNextSequenceNumberAsync();
                jobStatus.SequenceNumber = nextSequenceNumber;

                await _jobStatusRepository.AddAsync(jobStatus);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /JobStatus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobStatus JobStatus = await _jobStatusRepository.GetAsync(id);
            if (JobStatus == null)
            {
                return HttpNotFound();
            }
            return View(JobStatus);
        }

        // POST: /JobStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(JobStatus jobStatus)
        {
            if (ModelState.IsValid)
            {
                await _jobStatusRepository.UpdateAsync(jobStatus);
                return RedirectToAction("Index");
            }
            return View(jobStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(IList<JobStatusIndexViewModel> viewModel)
        {
            bool isDistinct = SequencesAreDistinct(viewModel);

            if (isDistinct == false)
            {
                ModelState.AddModelError("SequencesNotDistinct", "Department sequences must not repeat.");
            }

            if (ModelState.IsValid)
            {
                var jobStatuses = Mapper.Map<IList<JobStatusIndexViewModel>, IList<JobStatus>>(viewModel);
                foreach (var item in jobStatuses)
                {
                    await _jobStatusRepository.UpdateAsync(item);
                }
                //Helper method to determine the 
                TempData["UpdateMessage"] = "Departments updated successfully.";
                return RedirectToAction("Index");
            }
            //Error handling for it
            return View("Index", viewModel);
        }

        // GET: /JobStatus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            JobStatus jobStatus = await _jobStatusRepository.GetAsync(id);
            return View(jobStatus);
        }

        // POST: /JobStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            JobStatus JobStatus = await _jobStatusRepository.GetAsync(id);
            await _jobStatusRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool SequencesAreDistinct(IEnumerable<JobStatusIndexViewModel> jobStatuses)
        {
            var sequenceNumbers = new List<int>();

            foreach (var jobStatus in jobStatuses)
            {
                if (sequenceNumbers.Contains(jobStatus.SequenceNumber))
                {
                    return false;
                }
                else
                {
                    sequenceNumbers.Add(jobStatus.SequenceNumber);
                }
            }

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _jobStatusRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
