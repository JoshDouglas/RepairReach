using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories;
using RepairReach.Data;
using RepairReach.Data.Repositories.Interfaces;
using RepairReach.WebApplication.ViewModels;
using System.Threading.Tasks;
using AutoMapper;

namespace RepairReach.WebApplication.Controllers
{
    public class QuickLineItemController : Controller
    {
        private readonly IQuickLineItemRepository _quickLineItemRepository;

        public QuickLineItemController(IQuickLineItemRepository quickLineItemRepository)
        {
            if (quickLineItemRepository == null)
            {
                throw new ArgumentNullException("quickLineItemRepository");
            }

            _quickLineItemRepository = quickLineItemRepository;
        }

        // GET: QuickLineItem
        public async Task<ActionResult> Index()
        {
            var quickLineItems = await _quickLineItemRepository.GetAllAsync();
            var viewModel = Mapper.Map<IEnumerable<QuickLineItem>, IEnumerable<QuickLineItemIndexViewModel>>(quickLineItems);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            var quickLineItem = new QuickLineItemCreateViewModel();

            return View(quickLineItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuickLineItemCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var quickLineItem = Mapper.Map<QuickLineItemCreateViewModel, QuickLineItem>(viewModel);
                await _quickLineItemRepository.AddAsync(quickLineItem);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuickLineItem quickLineItem = await _quickLineItemRepository.GetAsync(id);
            if (quickLineItem == null)
            {
                return HttpNotFound();
            }

            var viewModel = Mapper.Map<QuickLineItem, QuickLineItemEditViewModel>(quickLineItem);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QuickLineItemEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var quickLineItem = Mapper.Map<QuickLineItemEditViewModel, QuickLineItem>(viewModel);
                await _quickLineItemRepository.UpdateAsync(quickLineItem);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuickLineItem quickLineItem = await _quickLineItemRepository.GetAsync(id);
            return View(quickLineItem);
        }

        // POST: /LineItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuickLineItem quickLineItem = await _quickLineItemRepository.GetAsync(id);
            await _quickLineItemRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetJsonAsync(int id)
        {
            var quickLineItem = await _quickLineItemRepository.GetAsync(id);

            var result = new
            {
                quickLineItem.Description, quickLineItem.PartName, quickLineItem.PartNumber, quickLineItem.PartQty, quickLineItem.PartEach,
                quickLineItem.PartCost, quickLineItem.ServiceName, quickLineItem.ServiceQty, quickLineItem.ServiceEach, quickLineItem.ServiceCost,
                quickLineItem.QuickLineItemId
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetTermJsonAsync(string term)
        {
            var quickLineItems = await _quickLineItemRepository.GetTermAsync(term);

            return Json(quickLineItems, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _quickLineItemRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}