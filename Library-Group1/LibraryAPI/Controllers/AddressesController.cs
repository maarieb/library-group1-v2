using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Entities;
using LibraryCore.Interfaces;
using LibraryAPI.Services;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private AddressService addressServices;

        public AddressesController(AddressService service)
        {
            addressServices = service;
        }

        // GET: api/Addresses
        [HttpGet]
        [ProducesResponseType(201)]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            return await addressServices.GetAll();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await addressServices.GetById(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(202)]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            try
            {
                await addressServices.Update(address);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Accepted(address);
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            await addressServices.Create(address);
            return CreatedAtAction("GetAddress", new { id = address.Id }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            Address address = await addressServices.GetById(id);
            if (address == null)
            {
                return NotFound();
            }
            await addressServices.Delete(address);
            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return addressServices.GetById(id) != null;
        }
    }
}
