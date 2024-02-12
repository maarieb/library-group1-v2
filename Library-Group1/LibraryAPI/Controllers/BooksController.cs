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
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly WriterService _writerService;
        private readonly DomainService _domainService;

        public BooksController(BookService service, WriterService writerService, DomainService domainService)
        {
            _bookService = service;
            _writerService = writerService;
            _domainService = domainService;
        }

        // GET: api/Addresses
        [HttpGet]
        [ProducesResponseType(201)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _bookService.GetAll();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(202)]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            try
            {
                await _domainService.Update(book.Domain);
                await _writerService.Update(book.Writer);
                await _bookService.Update(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Accepted(book);
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<Address>> PostBook(Book book)
        {
            Domain domain = await _domainService.GetByName(book.Domain.Name);
            Writer writer = await _writerService.GetSingle(book.Writer.LastName, book.Writer.FirstName);
            Book bookFounded = await _bookService.GetSingle(book.Title);

            if (bookFounded != null) return Conflict(bookFounded);
            if (domain != null) book.Domain = domain;
            if (writer != null) book.Writer = writer; 
            await _bookService.Create(book);
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            Book book = await _bookService.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            await _bookService.Delete(book);
            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _bookService.GetById(id) != null;
        }
    }
}
