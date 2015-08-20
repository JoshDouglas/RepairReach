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
    public class JobCategoryController : Controller
    {
        private readonly IJobCategoryRepository _jobCategoryRepository = null;

        public JobCategoryController(IJobCategoryRepository jobCategoryRepository)
        {
            if (jobCategoryRepository == null)
            {
                throw new ArgumentNullException("jobCategoryRepository");
            }

            _jobCategoryRepository = jobCategoryRepository;
        }

        // GET: /JobCategory/
        public async Task<ActionResult> Index()
        {
            var jobCategories = await _jobCategoryRepository.GetAllAsync();
            var viewModel =
                Mapper.Map<IList<JobCategory>, IList<JobCategoryIndexViewModel>>(jobCategories.OrderBy(r => r.SequenceNumber).ToList());
            return View(viewModel);
        }

        // GET: /JobCategory/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCategory jobCategory = await _jobCategoryRepository.GetAsync(id);
            if (jobCategory == null)
            {
                return HttpNotFound();
            }
            return View(jobCategory);
        }

        // GET: /JobCategory/Create
        public ActionResult Create()
        {
            var viewModel = new JobCategoryCreateViewModel();
            return View(viewModel);
        }

        // POST: /JobCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobCategoryCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var jobCategory = Mapper.Map<JobCategoryCreateViewModel, JobCategory>(viewModel);

                var nextSequenceNumber = await _jobCategoryRepository.GetNextSequenceNumberAsync();
                jobCategory.SequenceNumber = nextSequenceNumber;

                await _jobCategoryRepository.AddAsync(jobCategory);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /JobCategory/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobCategory JobCategory = await _jobCategoryRepository.GetAsync(id);
            if (JobCategory == null)
            {
                return HttpNotFound();
            }
            return View(JobCategory);
        }

        // POST: /JobCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                await _jobCategoryRepository.UpdateAsync(jobCategory);
                return RedirectToAction("Index");
            }
            return View(jobCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(IList<JobCategoryIndexViewModel> viewModel)
        {
            bool isDistinct = SequencesAreDistinct(viewModel);

            if (isDistinct == false)
            {
                ModelState.AddModelError("SequencesNotDistinct", "Category sequences must not repeat.");
            }

            if (ModelState.IsValid)
            {
                var jobCategoryes = Mapper.Map<IList<JobCategoryIndexViewModel>, IList<JobCategory>>(viewModel);
                foreach (var item in jobCategoryes)
                {
                    await _jobCategoryRepository.UpdateAsync(item);
                }
                //Helper method to determine the 
                TempData["UpdateMessage"] = "Categories updated successfully.";
                return RedirectToAction("Index");
            }
            //Error handling for it
            return View("Index", viewModel);
        }

        // GET: /JobCategory/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            JobCategory jobCategory = await _jobCategoryRepository.GetAsync(id);
            return View(jobCategory);
        }

        // POST: /JobCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            JobCategory JobCategory = await _jobCategoryRepository.GetAsync(id);
            await _jobCategoryRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool SequencesAreDistinct(IEnumerable<JobCategoryIndexViewModel> jobCategoryes)
        {
            var sequenceNumbers = new List<int>();

            foreach (var jobCategory in jobCategoryes)
            {
                if (sequenceNumbers.Contains(jobCategory.SequenceNumber))
                {
                    return false;
                }
                else
                {
                    sequenceNumbers.Add(jobCategory.SequenceNumber);
                }
            }

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _jobCategoryRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
