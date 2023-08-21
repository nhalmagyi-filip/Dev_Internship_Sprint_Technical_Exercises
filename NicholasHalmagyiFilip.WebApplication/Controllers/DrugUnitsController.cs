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
    public class DrugUnitsController : Controller
    {
        private readonly AppDbContext _context;

        public DrugUnitsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DrugUnits
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DrugUnits.Include(d => d.DrugUnitDepot).Include(d => d.DrugUnitDrugType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DrugUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugUnit = await _context.DrugUnits
                .Include(d => d.DrugUnitDepot)
                .Include(d => d.DrugUnitDrugType)
                .FirstOrDefaultAsync(m => m.DrugUnitId == id);
            if (drugUnit == null)
            {
                return NotFound();
            }

            return View(drugUnit);
        }

        // GET: DrugUnits/Create
        public IActionResult Create()
        {
            ViewData["DrugUnitDepotId"] = new SelectList(_context.Depots, "DepotId", "DepotId");
            ViewData["DrugUnitDrugTypeId"] = new SelectList(_context.DrugTypes, "DrugTypeId", "DrugTypeId");
            return View();
        }

        // POST: DrugUnits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrugUnitId,DrugUnitPickNumber,DrugUnitDepotId,DrugUnitDrugTypeId")] DrugUnit drugUnit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drugUnit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DrugUnitDepotId"] = new SelectList(_context.Depots, "DepotId", "DepotId", drugUnit.DrugUnitDepotId);
            ViewData["DrugUnitDrugTypeId"] = new SelectList(_context.DrugTypes, "DrugTypeId", "DrugTypeId", drugUnit.DrugUnitDrugTypeId);
            return View(drugUnit);
        }

        // GET: DrugUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugUnit = await _context.DrugUnits.FindAsync(id);
            if (drugUnit == null)
            {
                return NotFound();
            }
            ViewData["DrugUnitDepotId"] = new SelectList(_context.Depots, "DepotId", "DepotId", drugUnit.DrugUnitDepotId);
            ViewData["DrugUnitDrugTypeId"] = new SelectList(_context.DrugTypes, "DrugTypeId", "DrugTypeId", drugUnit.DrugUnitDrugTypeId);
            return View(drugUnit);
        }

        // POST: DrugUnits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DrugUnitId,DrugUnitPickNumber,DrugUnitDepotId,DrugUnitDrugTypeId")] DrugUnit drugUnit)
        {
            if (id != drugUnit.DrugUnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drugUnit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrugUnitExists(drugUnit.DrugUnitId))
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
            ViewData["DrugUnitDepotId"] = new SelectList(_context.Depots, "DepotId", "DepotId", drugUnit.DrugUnitDepotId);
            ViewData["DrugUnitDrugTypeId"] = new SelectList(_context.DrugTypes, "DrugTypeId", "DrugTypeId", drugUnit.DrugUnitDrugTypeId);
            return View(drugUnit);
        }

        // GET: DrugUnits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drugUnit = await _context.DrugUnits
                .Include(d => d.DrugUnitDepot)
                .Include(d => d.DrugUnitDrugType)
                .FirstOrDefaultAsync(m => m.DrugUnitId == id);
            if (drugUnit == null)
            {
                return NotFound();
            }

            return View(drugUnit);
        }

        // POST: DrugUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drugUnit = await _context.DrugUnits.FindAsync(id);
            _context.DrugUnits.Remove(drugUnit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrugUnitExists(int id)
        {
            return _context.DrugUnits.Any(e => e.DrugUnitId == id);
        }
    }
}
