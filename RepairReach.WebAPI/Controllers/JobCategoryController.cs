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
    public class JobCategoryController : ApiController
    {
        private readonly IJobCategoryRepository _jobCategoryRepository = null;

        public JobCategoryController(IJobCategoryRepository jobCategoryRepository)
        {
            if (jobCategoryRepository == null)
            {
                throw new ArgumentNullException("jobCategoryRepository");
            }

            _jobCategoryRepository = jobCategoryRepository;
        }

        // GET api/JobCategory
        public async Task<IEnumerable<JobCategory>> GetJobCategory()
        {
            return await _jobCategoryRepository.GetAllAsync();
        }

        // GET api/JobCategory/5
        [ResponseType(typeof(JobCategory))]
        public async Task<IHttpActionResult> GetJobCategory(int id)
        {
            JobCategory jobCategory = await _jobCategoryRepository.GetAsync(id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            return Ok(jobCategory);
        }

        // PUT api/JobCategory/5
        public async Task<IHttpActionResult> PutJobCategory(int id, JobCategory jobCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (jobCategory == null)
            {
                return BadRequest();
            }

            if (id != jobCategory.JobCategoryId)
            {
                return BadRequest();
            }

            await _jobCategoryRepository.UpdateAsync(jobCategory);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/JobCategory
        [ResponseType(typeof(JobCategory))]
        public async Task<IHttpActionResult> PostJobCategory(JobCategory jobCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _jobCategoryRepository.AddAsync(jobCategory);

            return CreatedAtRoute("DefaultApi", new { id = jobCategory.JobCategoryId }, jobCategory);
        }

        // DELETE api/JobCategory/5
        [ResponseType(typeof(JobCategory))]
        public async Task<IHttpActionResult> DeleteJobCategory(int id)
        {
            JobCategory jobCategory = await _jobCategoryRepository.GetAsync(id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            await _jobCategoryRepository.DeleteAsync(id);

            return Ok(jobCategory);
        }

        protected override void Dispose(bool disposing)
        {
            _jobCategoryRepository.Dispose();
        }

    }
}