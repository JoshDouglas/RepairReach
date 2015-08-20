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

namespace RepairReach.WebApplication.Controllers
{
    [Authorize]
    public class PartController : Controller
    {
        private readonly IPartRepository _partRepository = null;

        public PartController(IPartRepository partRepository)
        {
            if (partRepository == null)
            {
                throw new ArgumentNullException("partRepository");
            }

            _partRepository = partRepository;
        }

        public async Task<ActionResult> GetPartById(int id)
        {
            Part part = await _partRepository.GetAsync(id);

            var result = new { Name = part.Name, Amount = part.Amount.ToString("N2"), Cost = part.CostAmount.ToString("N2"), PartNumber = part.PartNumber };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: /Part/
        public async Task<ActionResult> Index()
        {
            return View(await _partRepository.GetAllAsync());
        }

        // GET: /Part/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = await _partRepository.GetAsync(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // GET: /Part/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Part/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Part part)
        {
            if (ModelState.IsValid)
            {
                await _partRepository.AddAsync(part);
                return RedirectToAction("Index");
            }

            return View(part);
        }

        // GET: /Part/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = await _partRepository.GetAsync(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // POST: /Part/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Part part)
        {
            if (ModelState.IsValid)
            {
                await _partRepository.UpdateAsync(part);
                return RedirectToAction("Index");
            }
            return View(part);
        }

        // GET: /Part/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = await _partRepository.GetAsync(id);
            return View(part);
        }

        // POST: /Part/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Part part = await _partRepository.GetAsync(id);
            await _partRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _partRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
