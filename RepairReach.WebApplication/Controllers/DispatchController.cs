using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.ViewModels;

namespace RepairReach.WebApplication.Controllers
{
    [Authorize]
    public class DispatchController : Controller
    {
        private readonly IJobRepository _jobRepository = null;

        public DispatchController(IJobRepository jobRepository)
        {
            if (jobRepository == null)
            {
                throw new ArgumentNullException("jobRepository");
            }
            _jobRepository = jobRepository;
        }
        //
        // GET: /Dispatch/
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Jobs";

            var jobs = await _jobRepository.GetAllAsync();

            MapViewModel vm = new MapViewModel();
            foreach (var j in jobs)
            {
                JobMapViewModel jm = new JobMapViewModel();
                //jm.Job = j;
                //jm.Latitude =
                //jm.Longitude =
                //jm.Name = "Test";
            }

            return View(await _jobRepository.GetAllAsync());
        }

        //
        // GET: /Dispatch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Dispatch/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Dispatch/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Dispatch/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Dispatch/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Dispatch/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Dispatch/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
