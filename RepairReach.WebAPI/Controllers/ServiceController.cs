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
    public class ServiceController : ApiController
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

        // GET api/Service
        public async Task<IEnumerable<Service>> GetService()
        {
            return await _serviceRepository.GetAllAsync();
        }

        // GET api/Service/5
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> GetService(int id)
        {
            Service service = await _serviceRepository.GetAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT api/Service/5
        public async Task<IHttpActionResult> PutService(int id, Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (service == null)
            {
                return BadRequest();
            }

            if (id != service.ServiceId)
            {
                return BadRequest();
            }

            await _serviceRepository.UpdateAsync(service);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Service
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> PostService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _serviceRepository.AddAsync(service);

            return CreatedAtRoute("DefaultApi", new { id = service.ServiceId }, service);
        }

        // DELETE api/Service/5
        [ResponseType(typeof(Service))]
        public async Task<IHttpActionResult> DeleteService(int id)
        {
            Service service = await _serviceRepository.GetAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            await _serviceRepository.DeleteAsync(id);

            return Ok(service);
        }

        protected override void Dispose(bool disposing)
        {
            _serviceRepository.Dispose();
        }

    }
}