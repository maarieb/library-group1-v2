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
    public class WriterController : ControllerBase
    {
        private readonly WriterService writerServices;

        public WriterController(WriterService service)
        {
            writerServices = service;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Writer>>> GetWriters()
        {
            return await writerServices.GetAll();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Writer>> GetWriter(int id)
        {
            var writer = await writerServices.GetById(id);

            if (writer == null)
            {
                return NotFound();
            }

            return writer;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(202)]
        public async Task<IActionResult> PutWriter(int id, Writer writer)
        {
            if (id != writer.Id)
            {
                return BadRequest();
            }

            try
            {
                await writerServices.Update(writer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WriterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Accepted(writer);
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Writer>> PostWriter(Writer writer)
        {
            await writerServices.Create(writer);
            return CreatedAtAction("GetWriter", new { id = writer.Id }, writer);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteWriter(int id)
        {
            Writer writer = await writerServices.GetById(id);
            if (writer == null)
            {
                return NotFound();
            }
            await writerServices.Delete(writer);
            return NoContent();
        }

        private bool WriterExists(int id)
        {
            return writerServices.GetById(id) != null;
        }
    }
}
