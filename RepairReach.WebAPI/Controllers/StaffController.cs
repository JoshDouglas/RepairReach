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
    public class StaffController : ApiController
    {
        private readonly IStaffRepository _staffRepository = null;

        public StaffController(IStaffRepository staffRepository)
        {
            if (staffRepository == null)
            {
                throw new ArgumentNullException("staffRepository");
            }

            _staffRepository = staffRepository;
        }

        // GET api/Staff
        public async Task<IEnumerable<Staff>> GetStaff()
        {
            return await _staffRepository.GetAllAsync();
        }

        // GET api/Staff/5
        [ResponseType(typeof(Staff))]
        public async Task<IHttpActionResult> GetStaff(int id)
        {
            Staff staff = await _staffRepository.GetAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            return Ok(staff);
        }

        // PUT api/Staff/5
        public async Task<IHttpActionResult> PutStaff(int id, Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (staff == null)
            {
                return BadRequest();
            }

            if (id != staff.StaffId)
            {
                return BadRequest();
            }

            await _staffRepository.UpdateAsync(staff);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Staff
        [ResponseType(typeof(Staff))]
        public async Task<IHttpActionResult> PostStaff(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _staffRepository.AddAsync(staff);

            return CreatedAtRoute("DefaultApi", new { id = staff.StaffId }, staff);
        }

        // DELETE api/Staff/5
        [ResponseType(typeof(Staff))]
        public async Task<IHttpActionResult> DeleteStaff(int id)
        {
            Staff staff = await _staffRepository.GetAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            await _staffRepository.DeleteAsync(id);

            return Ok(staff);
        }

        protected override void Dispose(bool disposing)
        {
            _staffRepository.Dispose();
        }

    }
}