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
    public class LineItemController : ApiController
    {
        private readonly ILineItemRepository _lineItemRepository = null;

        public LineItemController(ILineItemRepository lineItemRepository)
        {
            if (lineItemRepository == null)
            {
                throw new ArgumentNullException("lineItemRepository");
            }

            _lineItemRepository = lineItemRepository;
        }

        // GET api/LineItem
        public async Task<IEnumerable<LineItem>> GetLineItem()
        {
            return await _lineItemRepository.GetAllAsync();
        }

        // GET api/LineItem/5
        [ResponseType(typeof(LineItem))]
        public async Task<IHttpActionResult> GetLineItem(int id)
        {
            LineItem lineItem = await _lineItemRepository.GetAsync(id);
            if (lineItem == null)
            {
                return NotFound();
            }

            return Ok(lineItem);
        }

        // PUT api/LineItem/5
        public async Task<IHttpActionResult> PutLineItem(int id, LineItem lineItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (lineItem == null)
            {
                return BadRequest();
            }

            if (id != lineItem.LineItemId)
            {
                return BadRequest();
            }

            await _lineItemRepository.UpdateAsync(lineItem);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/LineItem
        [ResponseType(typeof(LineItem))]
        public async Task<IHttpActionResult> PostLineItem(LineItem lineItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lineItemRepository.AddAsync(lineItem);

            return CreatedAtRoute("DefaultApi", new { id = lineItem.LineItemId }, lineItem);
        }

        // DELETE api/LineItem/5
        [ResponseType(typeof(LineItem))]
        public async Task<IHttpActionResult> DeleteLineItem(int id)
        {
            LineItem lineItem = await _lineItemRepository.GetAsync(id);
            if (lineItem == null)
            {
                return NotFound();
            }

            await _lineItemRepository.DeleteAsync(id);

            return Ok(lineItem);
        }

        protected override void Dispose(bool disposing)
        {
            _lineItemRepository.Dispose();
        }

    }
}