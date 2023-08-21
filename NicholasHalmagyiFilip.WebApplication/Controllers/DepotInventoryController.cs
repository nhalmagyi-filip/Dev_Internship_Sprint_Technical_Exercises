using Microsoft.AspNetCore.Mvc;
using NicholasHalmagyiFilip.DomainCore;
using NicholasHalmagyiFilip.DomainCore.CorrelationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.WebApplication.Controllers
{
    public class DepotInventoryController : Controller
    {
        private readonly DepotCorrelationService _correlationService;
        private readonly DepotInventoryService _inventoryService;

        public DepotInventoryController(
            DepotCorrelationService correlationService,
            DepotInventoryService inventoryService)
        {
            _correlationService = correlationService;
            _inventoryService = inventoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AssociateDrugs(string depotId, int startPickNumber, int endPickNumber)
        {
            _inventoryService.AssociateDrugs(depotId, startPickNumber, endPickNumber);
            TempData["Message"] = "Association succeeded!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DisassociateDrugs(int startPickNumber, int endPickNumber)
        {
            _inventoryService.DisassociateDrugs(startPickNumber, endPickNumber);
            TempData["Message"] = "Disassociation succeeded!";
            return RedirectToAction("Index");
        }
    }
}
