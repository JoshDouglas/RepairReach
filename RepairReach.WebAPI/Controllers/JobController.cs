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
    public class JobController : ApiController
    {
        private readonly IJobRepository _jobRepository = null;

        public JobController(IJobRepository jobRepository)
        {
            if (jobRepository == null)
            {
                throw new ArgumentNullException("jobRepository");
            }

            _jobRepository = jobRepository;
        }

        // GET api/Job
        public async Task<IEnumerable<Job>> GetJob()
        {
            return await _jobRepository.GetAllAsync();
        }

        // GET api/Job/5
        [ResponseType(typeof(Job))]
        public async Task<IHttpActionResult> GetJob(int id)
        {
            Job job = await _jobRepository.GetAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // PUT api/Job/5
        public async Task<IHttpActionResult> PutJob(int id, Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (job == null)
            {
                return BadRequest();
            }

            if (id != job.JobId)
            {
                return BadRequest();
            }

            await _jobRepository.UpdateAsync(job);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Job
        [ResponseType(typeof(Job))]
        public async Task<IHttpActionResult> PostJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _jobRepository.AddAsync(job);

            return CreatedAtRoute("DefaultApi", new { id = job.JobId }, job);
        }

        // DELETE api/Job/5
        [ResponseType(typeof(Job))]
        public async Task<IHttpActionResult> DeleteJob(int id)
        {
            Job job = await _jobRepository.GetAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            await _jobRepository.DeleteAsync(id);

            return Ok(job);
        }

        protected override void Dispose(bool disposing)
        {
            _jobRepository.Dispose();
        }

    }
}