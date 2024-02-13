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
    public class ReadersController : ControllerBase
    {
        private readonly ReaderService _readerService;
        private readonly AddressService _addressService;

        public ReadersController(ReaderService service, AddressService addressService)
        {
            _readerService = service;
            _addressService = addressService;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reader>>> GetReaders()
        {
            return await _readerService.GetAll();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Reader>> GetReader(int id)
        {
            var reader = await _readerService.GetById(id);

            if (reader == null)
            {
                return NotFound();
            }

            return reader;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(202)]
        public async Task<IActionResult> PutReader(int id, Reader reader)
        {
            if (id != reader.Id)
            {
                return BadRequest();
            }

            try
            {
                await _addressService.Update(reader.Address);
                await _readerService.Update(reader);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReaderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Accepted(reader);
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Reader>> PostReader(Reader reader)
        {
            Address address = await _addressService.GetSingle(reader.Address);
            Reader readerFounded = await _readerService.GetSingle(reader);

            if (readerFounded != null) return Conflict(readerFounded);
            if (address != null) reader.Address = address;
            await _readerService.Create(reader);
            return CreatedAtAction("GetReader", new { id = reader.Id }, reader);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteReader(int id)
        {
            Reader reader = await _readerService.GetById(id);
            if (reader == null)
            {
                return NotFound();
            }
            await _readerService.Delete(reader);
            return NoContent();
        }

        private bool ReaderExists(int id)
        {
            return _readerService.GetById(id) != null;
        }
    }
}
