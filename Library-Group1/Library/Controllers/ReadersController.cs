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
    public class ReadersController : Controller
    {
        private readonly IReaderRepository Repository;
        private readonly IAddressRepository AdressRepository;

        public ReadersController(IReaderRepository repo, IAddressRepository adressRepository)
        {
            Repository = repo;
            AdressRepository = adressRepository;
        }

        // GET: Readers
        public async Task<IActionResult> Index()
        {
            return View(await Repository.GetAll());
        }

        // GET: Readers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await Repository.GetById(id.Value);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // GET: Readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReadersViewModel readervm)
        {
            if (ModelState.IsValid)
            {
                Address address = new Address()
                {
                    Appartment = readervm.Appartment,
                    City = readervm.City,
                    Street = readervm.Street,
                    ZipCode = readervm.ZipCode,
                    Country = readervm.Country,
                };

                Reader reader = new Reader()
                {
                    FirstName = readervm.FirstName,
                    LastName = readervm.LastName,
                    Mail = readervm.Mail,
                    PhoneNumber = readervm.PhoneNumber,
                    Address = address,
                };
 
                await Repository.Insert(reader);
                return RedirectToAction(nameof(Index));
            }
            return View(readervm);
        }

        // GET: Readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await Repository.GetById(id.Value);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reader reader)
        {
            if (id != reader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Repository.Update(reader);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool test = await Repository.Exist(reader.Id);
                    if (!test)
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
            return View(reader);
        }

        // GET: Readers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reader = await Repository.GetById(id.Value);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // POST: Readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reader = await Repository.GetById(id);
            if (reader != null)
            {
                await Repository.Delete(reader);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
