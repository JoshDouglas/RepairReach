using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RepairReach.Core.Model;
using RepairReach.Data;
using RepairReach.Data.Repositories.Interfaces;

namespace RepairReach.WebAPI.Controllers
{
    public class CompanyController : ApiController
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

        // GET api/Company
        public async Task<IEnumerable<Company>> GetCompany()
        {
            return await _companyRepository.GetAllAsync();
        }

        // GET api/Company/5
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> GetCompany(int id)
        {
            Company company = await _companyRepository.GetAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // PUT api/Company/5
        public async Task<IHttpActionResult> PutCompany(int id, Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (company == null)
            {
                return BadRequest();
            }

            if (id != company.CompanyId)
            {
                return BadRequest();
            }

            await _companyRepository.UpdateAsync(company);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Company
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> PostCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _companyRepository.AddAsync(company);

            return CreatedAtRoute("DefaultApi", new { id = company.CompanyId }, company);
        }

        // DELETE api/Company/5
        [ResponseType(typeof(Company))]
        public async Task<IHttpActionResult> DeleteCompany(int id)
        {
            Company company = await _companyRepository.GetAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            await _companyRepository.DeleteAsync(id);

            return Ok(company);
        }

        protected override void Dispose(bool disposing)
        {
            _companyRepository.Dispose();
        }

    }
}