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
using AutoMapper;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories;
using RepairReach.Data;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.ViewModels;

namespace RepairReach.WebApplication.Controllers
{
    [Authorize]
    public class HowDidYouFindUsController : Controller
    {
        private readonly IHowDidYouFindUsRepository _howDidYouFindUsRepository = null;

        public HowDidYouFindUsController(IHowDidYouFindUsRepository howDidYouFindUsRepository)
        {
            if (howDidYouFindUsRepository == null)
            {
                throw new ArgumentNullException("howDidYouFindUsRepository");
            }

            _howDidYouFindUsRepository = howDidYouFindUsRepository;
        }

        // GET: /HowDidYouFindUs/
        public async Task<ActionResult> Index()
        {
            var all = await _howDidYouFindUsRepository.GetAllAsync();
            var viewModel =
                Mapper.Map<IList<HowDidYouFindUs>, IList<HowDidYouFindUsIndexViewModel>>(all.OrderBy(r => r.SequenceNumber).ToList());
            return View(viewModel);
        }

        // GET: /HowDidYouFindUs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HowDidYouFindUs howDidYouFindUs = await _howDidYouFindUsRepository.GetAsync(id);
            if (howDidYouFindUs == null)
            {
                return HttpNotFound();
            }
            return View(howDidYouFindUs);
        }

        // GET: /HowDidYouFindUs/Create
        public ActionResult Create()
        {
            var viewModel = new HowDidYouFindUsCreateViewModel();
            return View(viewModel);
        }

        // POST: /HowDidYouFindUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HowDidYouFindUsCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var howDidYouFindUs = Mapper.Map<HowDidYouFindUsCreateViewModel, HowDidYouFindUs>(viewModel);

                var nextSequenceNumber = await _howDidYouFindUsRepository.GetNextSequenceNumberAsync();
                howDidYouFindUs.SequenceNumber = nextSequenceNumber;

                await _howDidYouFindUsRepository.AddAsync(howDidYouFindUs);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /HowDidYouFindUs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HowDidYouFindUs HowDidYouFindUs = await _howDidYouFindUsRepository.GetAsync(id);
            if (HowDidYouFindUs == null)
            {
                return HttpNotFound();
            }
            return View(HowDidYouFindUs);
        }

        // POST: /HowDidYouFindUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(HowDidYouFindUs howDidYouFindUs)
        {
            if (ModelState.IsValid)
            {
                await _howDidYouFindUsRepository.UpdateAsync(howDidYouFindUs);
                return RedirectToAction("Index");
            }
            return View(howDidYouFindUs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(IList<HowDidYouFindUsIndexViewModel> viewModel)
        {
            bool isDistinct = SequencesAreDistinct(viewModel);

            if (isDistinct == false)
            {
                ModelState.AddModelError("SequencesNotDistinct", "Sequences must not repeat.");
            }

            if (ModelState.IsValid)
            {
                var howDidYouFindUses =
                    Mapper.Map<IList<HowDidYouFindUsIndexViewModel>, IList<HowDidYouFindUs>>(viewModel);
                foreach (var item in howDidYouFindUses)
                {
                    await _howDidYouFindUsRepository.UpdateAsync(item);
                }
                //Helper method to determine the 
                TempData["UpdateMessage"] = "Updated successfully.";
                return RedirectToAction("Index");
            }
            //Error handling for it
            return View("Index", viewModel);
        }

        // GET: /HowDidYouFindUs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            HowDidYouFindUs howDidYouFindUs = await _howDidYouFindUsRepository.GetAsync(id);
            return View(howDidYouFindUs);
        }

        // POST: /HowDidYouFindUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HowDidYouFindUs HowDidYouFindUs = await _howDidYouFindUsRepository.GetAsync(id);
            await _howDidYouFindUsRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool SequencesAreDistinct(IEnumerable<HowDidYouFindUsIndexViewModel> howDidYouFindUses)
        {
            var sequenceNumbers = new List<int>();

            foreach (var howDidYouFindUs in howDidYouFindUses)
            {
                if (sequenceNumbers.Contains(howDidYouFindUs.SequenceNumber))
                {
                    return false;
                }
                else
                {
                    sequenceNumbers.Add(howDidYouFindUs.SequenceNumber);
                }
            }

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _howDidYouFindUsRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
