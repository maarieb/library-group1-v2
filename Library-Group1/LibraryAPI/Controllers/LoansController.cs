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
using LibraryCore.Enums;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using LibraryAPI.DTO;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly LoanService _loanService;
        private readonly ReaderService _readerService;
        private readonly BookService _bookService;

        public LoansController(LoanService service, ReaderService readerService, BookService bookService )
        {
            _loanService = service;
            _readerService = readerService;
            _bookService = bookService;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            return await _loanService.GetAll();
        }

        [HttpGet("current")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetCurrentLoans()
        {
            return await _loanService.GetCurrentLoans();
        }

        [HttpGet("finished")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetFinishedLoans()
        {
            return await _loanService.GetFinishedLoans();
        }


        // GET: api/Loans/5
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
            var loan = await _loanService.GetById(id);

            if (loan == null)
            {
                return NotFound();
            }

            return loan;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(202)]
        public async Task<IActionResult> PutLoan(int id, LoanDTO loanDto)
        {
            if (id != loanDto.Id)
            {
                return BadRequest();
            }

            Loan realLoan = await _loanService.GetById(id);
            realLoan.StartDate = loanDto.StartDate;
            realLoan.EndDate = loanDto.EndDate;
            Reader reader = await _readerService.GetSingle(loanDto.Mail);
            Book book = await _bookService.GetSingle(loanDto.Title);

            if (realLoan.Reader != reader) 
            { 
                realLoan.Reader = reader;
            }

            if (realLoan.Book.Title != book.Title)
            {
                realLoan.Book.State = BookState.DISPONIBLE;
                book.State = BookState.EMPRUNTE;
                realLoan.Book = book;
            }

            try
            {
                await _bookService.Update(realLoan.Book);
                await _bookService.Update(book);
                await _loanService.Update(realLoan);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!LoanExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Accepted(realLoan);
        }

        // POST: api/Loans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Loan>> PostLoan(LoanDTO loanDto)
        {
            Reader reader = await _readerService.GetSingle(loanDto.Mail);
            Book book = await _bookService.GetSingle(loanDto.Title);
            Loan loan = new Loan();

            if (reader != null) {
                loan.Reader = reader;
            } else
            {
                return NotFound(reader);
            }

            if (book != null)
            {
                if (book.State == BookState.DISPONIBLE)
                {
                    book.State = BookState.EMPRUNTE;
                    loan.Book = book;
                } else
                {
                    return NotFound(book.State);
                }
            }
            else
            {
                return NotFound(book);
            }

            loan.StartDate = loanDto.StartDate;
            await _loanService.Create(loan);
            await _bookService.Update(book);
            return CreatedAtAction("GetLoan", new { id = loan.Id }, loan);
        }


        [HttpPut("end/{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(202)]
        public async Task<ActionResult<Loan>> EndLoan(int id, Loan loan)
        {
            if (id != loan.Id)
            {
                return BadRequest();
            }

            Loan realLoan = await _loanService.GetById(id);

            try
            {
                realLoan.EndDate = DateTime.Now;
                realLoan.Book.State = BookState.DISPONIBLE;
                await _loanService.Update(realLoan);
            }
            catch (DbUpdateConcurrencyException)
            {

                if (realLoan == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Accepted(realLoan);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _loanService.GetById(id);
            if (loan == null)
            {
                return NotFound();
            }
            loan.Book.State = BookState.DISPONIBLE;
            await _bookService.Update(loan.Book);
            await _loanService.Delete(loan);
            return NoContent();
        }

        private bool LoanExist(int id)
        {
            return _loanService.GetById(id) != null;
        }
    }
    
}
