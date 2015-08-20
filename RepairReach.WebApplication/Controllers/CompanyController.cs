using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using AutoMapper;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories;
using RepairReach.Data;
using RepairReach.Data.Repositories.Interfaces;
using System.IO;
using RepairReach.WebApplication.ViewModels;

namespace RepairReach.WebApplication.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository = null;

        public CompanyController(ICompanyRepository companyRepository)
        {
            if (companyRepository == null)
            {
                throw new ArgumentNullException("companyRepository");
            }

            _companyRepository = companyRepository;
        }

        // GET: /Company/
        public async Task<ActionResult> Index()
        {
            Company company;
            CompanyIndexViewModel viewModel;
            try
            {
                company = await _companyRepository.GetFirstAsync();
                viewModel = Mapper.Map<Company, CompanyIndexViewModel>(company);
            }
            catch (Exception)
            {
                return View();
            }

            return View(viewModel);
        }

        // GET: /Company/Details/5
        public async Task<ActionResult> Details()
        {
            Company company;
            try
            {
                company = await _companyRepository.GetFirstAsync();
                if (company == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(company);
        }

        // GET: /Company/Create
        public ActionResult Create()
        {
            var viewModel = new CompanyCreateViewModel();
            return View(viewModel);
        }

        // POST: /Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CompanyCreateViewModel viewModel, HttpPostedFileBase logoPath)
        {
            //image upload
            if (logoPath != null && logoPath.ContentLength > 0)
            {
                const string directory = "~/Content/flat/img/uploads";
                if (!Directory.Exists(Server.MapPath(directory)))
                {
                    Directory.CreateDirectory(Server.MapPath(directory));
                }

                var filePath = Path.Combine(Server.MapPath(directory), Path.GetFileName(logoPath.FileName));

                //re-size image
                var image = new WebImage(logoPath.InputStream);
                if (image.Width > 300 || image.Height > 100) image.Resize(300, 100).Crop(1, 1); //we have to crop this because the web helper adds a border for some wierd reason
                image.Save(filePath);

                viewModel.LogoPath = directory + "/" + Path.GetFileName(logoPath.FileName); //need to save this in relative format
            }

            if (ModelState.IsValid)
            {
                var company = Mapper.Map<CompanyCreateViewModel, Company>(viewModel);

                await _companyRepository.AddAsync(company);
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /Company/Edit/5
        public async Task<ActionResult> Edit()
        {
            Company company;
            CompanyEditViewModel viewModel;
            try
            {
                company = await _companyRepository.GetFirstAsync();
                
                if (company == null)
                {
                    return HttpNotFound();
                }

                viewModel = Mapper.Map<Company, CompanyEditViewModel>(company);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(viewModel);
        }

        // POST: /Company/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompanyEditViewModel viewModel, HttpPostedFileBase logoPath)
        {
            //image upload
            if (logoPath != null && logoPath.ContentLength > 0)
            {
                const string directory = "~/Content/flat/img/uploads";
                if (!Directory.Exists(Server.MapPath(directory)))
                {
                    Directory.CreateDirectory(Server.MapPath(directory));
                }

                var filePath = Path.Combine(Server.MapPath(directory), Path.GetFileName(logoPath.FileName));

                //re-size image
                var image = new WebImage(logoPath.InputStream);
                if (image.Width > 300 || image.Height > 100) image.Resize(300, 100).Crop(1, 1); //we have to crop this because the web helper adds a border for some wierd reason
                image.Save(filePath);

                viewModel.LogoPath = directory + "/" + Path.GetFileName(logoPath.FileName); //need to save this in relative format
            }

            //if they did not upload an image and they have uploaded a logo we need to grab it again or it will get set to null because of the input control
            if (logoPath == null)
            {
                var companyBefore = await _companyRepository.GetFirstAsync();
                viewModel.LogoPath = companyBefore.LogoPath;
            }

            if (ModelState.IsValid)
            {
                var company = Mapper.Map<CompanyEditViewModel, Company>(viewModel);

                await _companyRepository.UpdateAsync(company);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        ////// GET: /Company/Delete/5
        ////public async Task<ActionResult> Delete(int? id)
        ////{
        ////    if (id == null)
        ////    {
        ////        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        ////    }
        ////    await _companyRepository.DeleteAsync(id);
        ////    return View();
        ////}

        ////// POST: /Company/Delete/5
        ////[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        ////public async Task<ActionResult> DeleteConfirmed(int id)
        ////{
        ////    Company company = await _companyRepository.GetAsync(id);
        ////    await _companyRepository.DeleteAsync(id);
        ////    return RedirectToAction("Index");
        ////}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _companyRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
