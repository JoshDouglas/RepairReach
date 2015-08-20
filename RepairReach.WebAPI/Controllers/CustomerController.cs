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
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepository = null;

        public CustomerController(ICustomerRepository customerRepository)
        {
            if (customerRepository == null)
            {
                throw new ArgumentNullException("customerRepository");
            }

            _customerRepository = customerRepository;
        }

        // GET api/Customer
        public async Task<IEnumerable<Customer>> GetCustomer()
        {
            return await _customerRepository.GetAllAsync();
        }

        // GET api/Customer/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            Customer customer = await _customerRepository.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT api/Customer/5
        public async Task<IHttpActionResult> PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (customer == null)
            {
                return BadRequest();
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            await _customerRepository.UpdateAsync(customer);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Customer
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _customerRepository.AddAsync(customer);

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
        }

        // DELETE api/Customer/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            Customer customer = await _customerRepository.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerRepository.DeleteAsync(id);

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            _customerRepository.Dispose();
        }

    }
}