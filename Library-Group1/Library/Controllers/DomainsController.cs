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

namespace Library.Controllers
{
    public class DomainsController : Controller
    {
        public IDomainRepository Repository { get; }

        public DomainsController(IDomainRepository repository)
        {
            Repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await Repository.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domain = await Repository.GetById(id.Value);
            if (domain == null)
            {
                return NotFound();
            }

            return View(domain);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Domain domain)
        {
            if (ModelState.IsValid)
            {
                await Repository.Insert(domain);
                return RedirectToAction(nameof(Index));
            }
            return View(domain);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domain = await Repository.GetById(id.Value);
            if (domain == null)
            {
                return NotFound();
            }
            return View(domain);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Domain domain)
        {
            if (id != domain.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Repository.Update(domain);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exist = await Repository.Exist(id);
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
            return View(domain);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var domain = await Repository.GetById(id.Value);
            if (domain == null)
            {
                return NotFound();
            }

            return View(domain);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var domain = await Repository.GetById(id);
            if (domain != null)
            {
                await Repository.Delete(domain);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
