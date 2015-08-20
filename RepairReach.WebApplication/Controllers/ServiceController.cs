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
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository = null;

        public ServiceController(IServiceRepository serviceRepository)
        {
            if (serviceRepository == null)
            {
                throw new ArgumentNullException("serviceRepository");
            }

            _serviceRepository = serviceRepository;
        }

        public async Task<ActionResult> GetServiceById(int id)
        {
            Service service = await _serviceRepository.GetAsync(id);

            var result = new { Name = service.Name, Amount = service.Amount.ToString("N2"), Cost = service.CostAmount.ToString("N2") };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        // GET: /Customer/
        public async Task<ActionResult> Index()
        {
            return View(await _serviceRepository.GetAllAsync());
        }

        // GET: /Service/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = await _serviceRepository.GetAsync(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: /Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Service/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Service service)
        {
            if (ModelState.IsValid)
            {
                await _serviceRepository.AddAsync(service);
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // GET: /Service/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service Service = await _serviceRepository.GetAsync(id);
            if (Service == null)
            {
                return HttpNotFound();
            }
            return View(Service);
        }

        // POST: /Service/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                await _serviceRepository.UpdateAsync(service);
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: /Service/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = await _serviceRepository.GetAsync(id);
            return View(service);
        }

        // POST: /Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Service Service = await _serviceRepository.GetAsync(id);
            await _serviceRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _serviceRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
