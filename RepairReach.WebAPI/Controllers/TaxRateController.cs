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
    public class TaxRateController : ApiController
    {
        private readonly ITaxRateRepository _taxRateRepository = null;

        public TaxRateController(ITaxRateRepository taxRateRepository)
        {
            if (taxRateRepository == null)
            {
                throw new ArgumentNullException("taxRateRepository");
            }

            _taxRateRepository = taxRateRepository;
        }

        // GET api/TaxRate
        public async Task<IEnumerable<TaxRate>> GetTaxRate()
        {
            return await _taxRateRepository.GetAllAsync();
        }

        // GET api/TaxRate/5
        [ResponseType(typeof(TaxRate))]
        public async Task<IHttpActionResult> GetTaxRate(int id)
        {
            TaxRate taxRate = await _taxRateRepository.GetAsync(id);
            if (taxRate == null)
            {
                return NotFound();
            }

            return Ok(taxRate);
        }

        // PUT api/TaxRate/5
        public async Task<IHttpActionResult> PutTaxRate(int id, TaxRate taxRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (taxRate == null)
            {
                return BadRequest();
            }

            if (id != taxRate.TaxRateId)
            {
                return BadRequest();
            }

            await _taxRateRepository.UpdateAsync(taxRate);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/TaxRate
        [ResponseType(typeof(TaxRate))]
        public async Task<IHttpActionResult> PostTaxRate(TaxRate taxRate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _taxRateRepository.AddAsync(taxRate);

            return CreatedAtRoute("DefaultApi", new { id = taxRate.TaxRateId }, taxRate);
        }

        // DELETE api/TaxRate/5
        [ResponseType(typeof(TaxRate))]
        public async Task<IHttpActionResult> DeleteTaxRate(int id)
        {
            TaxRate taxRate = await _taxRateRepository.GetAsync(id);
            if (taxRate == null)
            {
                return NotFound();
            }

            await _taxRateRepository.DeleteAsync(id);

            return Ok(taxRate);
        }

        protected override void Dispose(bool disposing)
        {
            _taxRateRepository.Dispose();
        }

    }
}