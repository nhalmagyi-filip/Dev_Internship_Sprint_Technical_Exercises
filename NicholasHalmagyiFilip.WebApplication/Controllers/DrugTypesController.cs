using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NicholasHalmagyiFilip.DataModelCore;
using NicholasHalmagyiFilip.DataModelCore.Models;

namespace NicholasHalmagyiFilip.WebApplication.Controllers
{
    public class DrugTypesController : Controller
    {
        private readonly AppDbContext _context;

        public DrugTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DrugTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.DrugTypes.ToListAsync());
        }

        // GET: DrugTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context.DrugTypes
                .FirstOrDefaultAsync(m => m.DrugTypeId == id);
            if (drugType == null)
            {
                return NotFound();
            }

            return View(drugType);
        }

        // GET: DrugTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DrugTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrugTypeId,DrugTypeName,Weight")] DrugType drugType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drugType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drugType);
        }

        // GET: DrugTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context.DrugTypes.FindAsync(id);
            if (drugType == null)
            {
                return NotFound();
            }
            return View(drugType);
        }

        // POST: DrugTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DrugTypeId,DrugTypeName,Weight")] DrugType drugType)
        {
            if (id != drugType.DrugTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drugType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugTypeExists(drugType.DrugTypeId))
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
            return View(drugType);
        }

        // GET: DrugTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugType = await _context.DrugTypes
                .FirstOrDefaultAsync(m => m.DrugTypeId == id);
            if (drugType == null)
            {
                return NotFound();
            }

            return View(drugType);
        }

        // POST: DrugTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drugType = await _context.DrugTypes.FindAsync(id);
            _context.DrugTypes.Remove(drugType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugTypeExists(int id)
        {
            return _context.DrugTypes.Any(e => e.DrugTypeId == id);
        }
    }
}
