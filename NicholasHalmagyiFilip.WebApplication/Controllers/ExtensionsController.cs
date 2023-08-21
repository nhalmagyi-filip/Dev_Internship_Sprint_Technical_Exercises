using Microsoft.AspNetCore.Mvc;
using NicholasHalmagyiFilip.DataModelCore;
using NicholasHalmagyiFilip.DataModelCore.Models;
using NicholasHalmagyiFilip.DomainCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.WebApplication.Controllers
{
    public class ExtensionsController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ExtensionsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var groupedDrugUnits = _dbContext.DrugUnits.ToGroupedDrugUnits(_dbContext.DrugTypes);
            return View(groupedDrugUnits);
        }
    }
}
