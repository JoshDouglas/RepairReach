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
using RepairReach.WebApplication.ViewModels;

namespace RepairReach.WebApplication.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly IJobRepository _jobRepository = null;
        private readonly ICustomerRepository _customerRepository = null;

        public SearchController(IJobRepository jobRepository, ICustomerRepository customerRepository)
        {
            if (jobRepository == null)
            {
                throw new ArgumentNullException("jobRepository");
            }

            if (customerRepository == null)
            {
                throw new ArgumentNullException("customerRepository");
            }

            _jobRepository = jobRepository;
            _customerRepository = customerRepository;
        }

        //
        // GET: /Search/
        public async Task<ActionResult> Results(string searchTerm)
        {
            SearchViewModel viewModel = new SearchViewModel();
            viewModel.SearchTerm = searchTerm;

            if (searchTerm.Length > 0)
            {
                string searchTermLower = searchTerm.ToLower();

                var jobs = await _jobRepository.GetAllBySearchAsync(searchTermLower);
                var customers = await _customerRepository.GetAllBySearchAsync(searchTermLower);

                viewModel.Jobs = jobs;
                viewModel.Customers = customers;
            }

            return View(viewModel);
        }
	}
}