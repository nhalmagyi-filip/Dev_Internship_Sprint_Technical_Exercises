using Microsoft.EntityFrameworkCore;
using NicholasHalmagyiFilip.DataModelCore;
using NicholasHalmagyiFilip.DataModelCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DomainCore.CorrelationService
{
    public class DepotCorrelationService : BaseCorrelationService<List<DepotCorrelationResult>> 
    {
        private readonly AppDbContext dataSet;
        public DepotCorrelationService(AppDbContext DataSet) : base(DataSet)
        {
            dataSet = DataSet; 
        }

        public override List<DepotCorrelationResult> CorrelateData()
        {
            var correlationDataList = dataSet.Depots
                .Include(d => d.DepotCountries)
                .SelectMany(depot => depot.DepotDrugUnits, (depot, depotDrugUnit) => new { depot, depotDrugUnit })
                .Select(pr => new DepotCorrelationResult
                {
                    DepotName = pr.depot.DepotName,
                    CountryName = FindCountryName(pr.depot, dataSet),
                    DrugTypeName = FindDrugTypeName(pr.depotDrugUnit, dataSet),
                    DrugUnitId = pr.depotDrugUnit.DrugUnitId,
                    PickNumber = pr.depotDrugUnit.DrugUnitPickNumber
                })
                .ToList();
            return correlationDataList;
        }

        private static string FindCountryName(Depot depot, AppDbContext dataSet)
        {
            var depotWithCountries = dataSet.Depots.Include(d => d.DepotCountries).FirstOrDefault(d => d.DepotId == depot.DepotId);
            var countryNames = depotWithCountries.DepotCountries
                .Select(dc => dc.CountryName)
                .Where(name => !string.IsNullOrEmpty(name))
                .ToList();

            return string.Join(", ", countryNames);
        }

        private static string FindDrugTypeName(DrugUnit drugUnit, AppDbContext dataSet)
        {
            var drugType = dataSet.DrugTypes.FirstOrDefault(dt => dt.DrugTypeId == drugUnit.DrugUnitDrugTypeId);
            return drugType?.DrugTypeName;
        }
    }

    public class DepotCorrelationResult
    {
        public string DepotName { get; set; }
        public string CountryName { get; set; }
        public string DrugTypeName { get; set; }
        public int DrugUnitId { get; set; }
        public int PickNumber { get; set; }
    }
}