using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Entities;
using LibraryCore.Interfaces;
using Library.Models;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        public IBookRepository BookRepository { get;}
        public IWriterRepository WriterRepository { get; }
        public IDomainRepository DomainRepository { get; }

        public BooksController(IBookRepository repository, IWriterRepository writerRepository, IDomainRepository domainRepository)
        {
            BookRepository = repository;
            WriterRepository = writerRepository;
            DomainRepository = domainRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await BookRepository.GetAll());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await BookRepository.GetById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new BookViewModel();
            //viewModel.Writers = await WriterRepository.GetAll();

            ViewData["Writers"]
                = new SelectList(await WriterRepository.GetAll(), "Id", "LastName");
            ViewData["Domains"]
                = new SelectList(await DomainRepository.GetAll(), "Id", "Name");
            return View(viewModel);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                Book bookToCreate = new Book()
                {
                    Title = book.Title,
                    Description = book.Description,
                    PagesNumber = book.PagesNumber,
                    Domain = await DomainRepository.GetById(book.DomainId),
                    Writer = await WriterRepository.GetById(book.WriterId)
                };
                await BookRepository.Insert(bookToCreate);

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await BookRepository.GetById(id.Value);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await BookRepository.Update(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exist = await BookRepository.Exist(book.Id);
                    if (!exist)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await BookRepository.GetById(id.Value);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await BookRepository.GetById(id);
            if (book != null)
            {
                await BookRepository.Delete(book);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
