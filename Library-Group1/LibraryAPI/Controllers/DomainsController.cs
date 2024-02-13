using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Entities;
using LibraryAPI.Services;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        private readonly DomainService domainServices;

        public DomainsController(DomainService service)
        {
            domainServices = service;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain>>> GetDomains()
        {
            return await domainServices.GetAll();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Domain>> GetDomain(int id)
        {
            var domain = await domainServices.GetById(id);

            if (domain == null)
            {
                return NotFound();
            }

            return domain;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(202)]
        public async Task<IActionResult> PutDomain(int id, Domain domain)
        {
            if (id != domain.Id)
            {
                return BadRequest();
            }

            try
            {
                await domainServices.Update(domain);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DomainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Accepted(domain);
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Domain>> PostBook(Domain domain)
        {
            await domainServices.Create(domain);
            return CreatedAtAction("GetDomain", new { id = domain.Id }, domain);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteDomain(int id)
        {
            Domain domain = await domainServices.GetById(id);
            if (domain == null)
            {
                return NotFound();
            }
            await domainServices.Delete(domain);
            return NoContent();
        }

        private bool DomainExists(int id)
        {
            return domainServices.GetById(id) != null;
        }
    }
}
