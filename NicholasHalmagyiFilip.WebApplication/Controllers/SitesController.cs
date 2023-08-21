using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NicholasHalmagyiFilip.DataModelCore;
using NicholasHalmagyiFilip.DataModelCore.Models;
using NicholasHalmagyiFilip.DomainCore;

namespace NicholasHalmagyiFilip.WebApplication.Controllers
{
    public class SitesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SiteInventoryDbHander _handler;
        public SitesController(AppDbContext context, SiteInventoryDbHander handler)
        {
            _context = context;
            _handler = handler;
        }

        // GET: Sites/Edit/5
        public IActionResult Edit(int id)
        {
            ViewBag.SiteId = id;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateSiteInventory(int siteId, string requestedDrugCode, int requestedQuantity)
        {
            string parse_siteid = siteId.ToString();
            int quantity = _handler.UpdateSiteInventory(parse_siteid, requestedDrugCode, requestedQuantity);
            if (quantity!=0)
                TempData["Message"] = "The quantity requested could not be found.";
            else
                TempData["Message"] = "Request succeeded!";
            return RedirectToAction("Index");
        }

        // GET: Sites
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Sites.Include(s => s.Country);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.SiteId == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // GET: Sites/Create
        public IActionResult Create()
        {
            ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryId", "CountryId");
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteId,SiteName,CountryCode")] Site site)
        {
            if (ModelState.IsValid)
            {
                _context.Add(site);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryId", "CountryId", site.CountryCode);
            return View(site);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598

        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _context.Sites
                .Include(s => s.Country)
                .FirstOrDefaultAsync(m => m.SiteId == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var site = await _context.Sites.FindAsync(id);
            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteExists(int id)
        {
            return _context.Sites.Any(e => e.SiteId == id);
        }
    }
}
