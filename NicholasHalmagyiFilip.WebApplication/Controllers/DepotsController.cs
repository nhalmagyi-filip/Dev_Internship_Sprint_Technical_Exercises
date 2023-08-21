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
    public class DepotDrugWeightViewModel
    {
        public int DepotId { get; set; }
        public string DepotName { get; set; }
        public int DrugTypeId { get; set; }
        public string DrugTypeName { get; set; }
        public double TotalWeightKg { get; set; }
    }

    public class DepotsController : Controller
    {
        private readonly AppDbContext _context;

        public DepotsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Depots/DrugWeights
        public async Task<IActionResult> DrugWeights()
        {
            var depotDrugUnits = await _context.DrugUnits
                .Include(d => d.DrugUnitDepot)
                .Include(d => d.DrugUnitDrugType)
                .ToListAsync();

            var depotDrugWeights = depotDrugUnits
                .Where(d => d.DrugUnitDepot != null) // Filter out items with null DrugUnitDepot
                .GroupBy(d => new
                {
                    DrugUnitDepotId = d.DrugUnitDepotId.GetValueOrDefault(),
                    DrugUnitDrugTypeId = d.DrugUnitDrugTypeId
                })
                .Select(group => new DepotDrugWeightViewModel
                {
                    DepotId = group.Key.DrugUnitDepotId,
                    DepotName = group.First().DrugUnitDepot?.DepotName, // Use null conditional operator
            DrugTypeId = group.Key.DrugUnitDrugTypeId,
                    DrugTypeName = group.First().DrugUnitDrugType?.DrugTypeName, // Use null conditional operator
            TotalWeightKg = group.Sum(d => d.DrugUnitDrugType.Weight) / 2.2
                })
                .ToList();

            return View("~/Views/DepotDrugWeights/Index.cshtml", depotDrugWeights);
        }

        // GET: Depots
        public async Task<IActionResult> Index()
        {
            return View(await _context.Depots.ToListAsync());
        }

        // GET: Depots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depot = await _context.Depots
                .FirstOrDefaultAsync(m => m.DepotId == id);
            if (depot == null)
            {
                return NotFound();
            }

            return View(depot);
        }

        // GET: Depots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Depots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepotId,DepotName")] Depot depot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depot);
        }

        // GET: Depots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depot = await _context.Depots.FindAsync(id);
            if (depot == null)
            {
                return NotFound();
            }
            return View(depot);
        }

        // POST: Depots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepotId,DepotName")] Depot depot)
        {
            if (id != depot.DepotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepotExists(depot.DepotId))
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
            return View(depot);
        }

        // GET: Depots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depot = await _context.Depots
                .FirstOrDefaultAsync(m => m.DepotId == id);
            if (depot == null)
            {
                return NotFound();
            }

            return View(depot);
        }

        // POST: Depots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depot = await _context.Depots.FindAsync(id);
            _context.Depots.Remove(depot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepotExists(int id)
        {
            return _context.Depots.Any(e => e.DepotId == id);
        }
    }
}
