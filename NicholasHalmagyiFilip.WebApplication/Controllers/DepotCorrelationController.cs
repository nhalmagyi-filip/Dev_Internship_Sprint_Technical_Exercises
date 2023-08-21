using Microsoft.AspNetCore.Mvc;
using NicholasHalmagyiFilip.DomainCore.CorrelationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.WebApplication.Controllers
{
    public class DepotCorrelationController : Controller
    {
        private readonly DepotCorrelationService _correlationService;

        public DepotCorrelationController(DepotCorrelationService correlationService)
        {
            _correlationService = correlationService;
        }

        public IActionResult Index()
        {
            var correlationDataList = _correlationService.CorrelateData();
            return View(correlationDataList);
        }
    }
}
