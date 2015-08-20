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
using RepairReach.Core.Enum;
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
    public class JobNoteController : Controller
    {
        private readonly IJobNoteRepository _jobNoteRepository = null;
        private readonly IStaffRepository _staffRepository = null;

        public JobNoteController(IJobNoteRepository jobNoteRepository, IStaffRepository staffRepository)
        {
            if (jobNoteRepository == null)
            {
                throw new ArgumentNullException("jobNoteRepository");
            }

            if (staffRepository == null)
            {
                throw new ArgumentNullException("staffRepository");
            }

            _jobNoteRepository = jobNoteRepository;
            _staffRepository = staffRepository;
        }

        // GET: /jobNote/
        public async Task<ActionResult> Index()
        {
            return View(await _jobNoteRepository.GetAllAsync());
        }

        // GET: /jobNote/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobNote jobNote = await _jobNoteRepository.GetAsync(id);
            if (jobNote == null)
            {
                return HttpNotFound();
            }
            return View(jobNote);
        }

        // GET: /JobNote/Create
        public ActionResult Create(int jobId)
        {
            var viewModel = new JobNoteCreateViewModel();
            viewModel.JobId = jobId;

            return View(viewModel);
        }

        // POST: /JobNote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(JobNoteCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var jobNote = Mapper.Map<JobNoteCreateViewModel, JobNote>(viewModel);

                //get current user
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_staffRepository.GetContext()));
                ApplicationUser user = new ApplicationUser();
                user = await userManager.FindByNameAsync(User.Identity.Name);

                jobNote.CreatedBy = user.Staff.DisplayName;
                jobNote.CreatedDate = DateTime.UtcNow;

                await _jobNoteRepository.AddAsync(jobNote);
                return RedirectToAction("Edit", "Job", new { id = jobNote.JobId });
            }

            return View(viewModel);
        }

        // GET: /JobNote/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobNote jobNote = await _jobNoteRepository.GetAsync(id);
            if (jobNote == null)
            {
                return HttpNotFound();
            }

            @ViewBag.NoteCreatedByDifferentUser = (jobNote.CreatedBy != User.Identity.Name);

            var viewModel = Mapper.Map<JobNote, JobNoteEditViewModel>(jobNote);

            return View(viewModel);
        }

        // POST: /JobNote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(JobNoteEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var jobNote = Mapper.Map<JobNoteEditViewModel, JobNote>(viewModel);

                await _jobNoteRepository.UpdateAsync(jobNote);
                return RedirectToAction("Edit", "Job", new { id = jobNote.JobId });
            }

            return View(viewModel);
        }

        // GET: /JobNote/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobNote jobNote = await _jobNoteRepository.GetAsync(id);
            return View(jobNote);
        }

        // POST: /JobNote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            JobNote jobNote = await _jobNoteRepository.GetAsync(id);
            int deletedJobId = jobNote.JobId;
            await _jobNoteRepository.DeleteAsync(id);
            return RedirectToAction("Edit", "Job", new { id = deletedJobId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _jobNoteRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
