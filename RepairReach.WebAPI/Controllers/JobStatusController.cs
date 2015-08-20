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
    public class JobStatusController : ApiController
    {
        private readonly IJobStatusRepository _jobStatusRepository = null;

        public JobStatusController(IJobStatusRepository jobStatusRepository)
        {
            if (jobStatusRepository == null)
            {
                throw new ArgumentNullException("jobStatusRepository");
            }

            _jobStatusRepository = jobStatusRepository;
        }

        // GET api/JobStatus
        public async Task<IEnumerable<JobStatus>> GetJobStatus()
        {
            return await _jobStatusRepository.GetAllAsync();
        }

        // GET api/JobStatus/5
        [ResponseType(typeof(JobStatus))]
        public async Task<IHttpActionResult> GetJobStatus(int id)
        {
            JobStatus jobStatus = await _jobStatusRepository.GetAsync(id);
            if (jobStatus == null)
            {
                return NotFound();
            }

            return Ok(jobStatus);
        }

        // PUT api/JobStatus/5
        public async Task<IHttpActionResult> PutJobStatus(int id, JobStatus jobStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (jobStatus == null)
            {
                return BadRequest();
            }

            if (id != jobStatus.JobStatusId)
            {
                return BadRequest();
            }

            await _jobStatusRepository.UpdateAsync(jobStatus);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/JobStatus
        [ResponseType(typeof(JobStatus))]
        public async Task<IHttpActionResult> PostJobStatus(JobStatus jobStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _jobStatusRepository.AddAsync(jobStatus);

            return CreatedAtRoute("DefaultApi", new { id = jobStatus.JobStatusId }, jobStatus);
        }

        // DELETE api/JobStatus/5
        [ResponseType(typeof(JobStatus))]
        public async Task<IHttpActionResult> DeleteJobStatus(int id)
        {
            JobStatus jobStatus = await _jobStatusRepository.GetAsync(id);
            if (jobStatus == null)
            {
                return NotFound();
            }

            await _jobStatusRepository.DeleteAsync(id);

            return Ok(jobStatus);
        }

        protected override void Dispose(bool disposing)
        {
            _jobStatusRepository.Dispose();
        }

    }
}