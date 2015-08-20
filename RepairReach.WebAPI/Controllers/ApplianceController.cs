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
    public class ApplianceController : ApiController
    {
        private readonly IApplianceRepository _applianceRepository = null;

        public ApplianceController(IApplianceRepository applianceRepository)
        {
            if (applianceRepository == null)
            {
                throw new ArgumentNullException("applianceRepository");
            }

            _applianceRepository = applianceRepository;
        }

        // GET api/Appliance
        public async Task<IEnumerable<Appliance>> GetAppliance()
        {
            return await _applianceRepository.GetAllAsync();
        }

        // GET api/Appliance/5
        [ResponseType(typeof(Appliance))]
        public async Task<IHttpActionResult> GetAppliance(int id)
        {
            Appliance appliance = await _applianceRepository.GetAsync(id);
            if (appliance == null)
            {
                return NotFound();
            }

            return Ok(appliance);
        }

        // PUT api/Appliance/5
        public async Task<IHttpActionResult> PutAppliance(int id, Appliance appliance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (appliance == null)
            {
                return BadRequest();
            }

            if (id != appliance.ApplianceId)
            {
                return BadRequest();
            }

            await _applianceRepository.UpdateAsync(appliance);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Appliance
        [ResponseType(typeof(Appliance))]
        public async Task<IHttpActionResult> PostAppliance(Appliance appliance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _applianceRepository.AddAsync(appliance);

            return CreatedAtRoute("DefaultApi", new { id = appliance.ApplianceId }, appliance);
        }

        // DELETE api/Appliance/5
        [ResponseType(typeof(Appliance))]
        public async Task<IHttpActionResult> DeleteAppliance(int id)
        {
            Appliance appliance = await _applianceRepository.GetAsync(id);
            if (appliance == null)
            {
                return NotFound();
            }

            await _applianceRepository.DeleteAsync(id);

            return Ok(appliance);
        }

        protected override void Dispose(bool disposing)
        {
            _applianceRepository.Dispose();
        }

    }
}