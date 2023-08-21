using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NicholasHalmagyiFilip.DataModelCore;
using Microsoft.EntityFrameworkCore;
using NicholasHalmagyiFilip.DataModelCore.Models;
using NicholasHalmagyiFilip.DomainCore.CorrelationService;
using NicholasHalmagyiFilip.DomainCore;

namespace NicholasHalmagyiFilipCore
{
    class Program
    {
        public static void PrintAll(DbSet<Country> countries, List<Depot> depots, DbSet<DrugType> drugTypes, DbSet<DrugUnit> drugUnits, DbSet<Site> sites)
        {
            Console.WriteLine("Countries: ");
            foreach (Country country in countries)
            {
                Console.WriteLine(country);
            }
            Console.WriteLine();

            Console.WriteLine("Sites: ");
            foreach (Site site in sites)
            {
                Console.WriteLine(site);
            }
            Console.WriteLine();

            Console.WriteLine("Depots: ");
            foreach (Depot depot in depots)
            {
                Console.WriteLine(depot);
            }
            Console.WriteLine();

            Console.WriteLine("Drug Units: ");
            foreach (DrugUnit drugUnit in drugUnits)
            {
                Console.WriteLine(drugUnit);
            }
            Console.WriteLine();

            Console.WriteLine("Drug Types: ");
            foreach (DrugType drugType in drugTypes)
            {
                Console.WriteLine(drugType);
            }
            Console.WriteLine();
        }

        public static void PrintGroupedDrugUnits(Dictionary<string, List<DrugUnit>> groupedDrugUnits)
        {
            foreach (var grouping in groupedDrugUnits)
            {
                Console.WriteLine($"Drug Type: {grouping.Key}");
                foreach (var drugUnit in grouping.Value)
                {
                    Console.WriteLine(drugUnit);
                }
            }
        }

        public static void PrintCorellationData(List<DepotCorrelationResult> correlationData)
        {
            foreach (var result in correlationData)
            {
                Console.WriteLine($"Depot Name: {result.DepotName}");
                Console.WriteLine($"Country Name: {result.CountryName}");
                Console.WriteLine($"Drug Type Name: {result.DrugTypeName}");
                Console.WriteLine($"Drug Unit ID: {result.DrugUnitId}");
                Console.WriteLine($"Pick Number: {result.PickNumber}");
                Console.WriteLine("");
            }
        }

        public static void PrintCallerRequest(IEnumerable<DrugUnit> requests, int quantity)
        {

            if (!requests.Any() || requests.Count() != quantity)
            {
                Console.WriteLine("!ERROR! There are no Drug Units with the specified criteria OR the quantity is to big!");
            }
            foreach (DrugUnit request in requests)
            {
                Console.WriteLine(request);
            }
        }

        public static void Main(string[] args)
        {
            AppDbContext context = new AppDbContext();
            DepotInventoryService depotInventoryServices = new DepotInventoryService(context);
            SiteDistributionService siteDistributionService = new SiteDistributionService(context);
            DepotCorrelationService correlationService = new DepotCorrelationService(context);
            SiteInventoryDbHander inventoryDbHander = new SiteInventoryDbHander(context, siteDistributionService);

            Console.WriteLine("Initial data:");
            List<Depot> depotFromDatabase = Depot.LoadFromDatabase();
            PrintAll(context.Countries, depotFromDatabase, context.DrugTypes, context.DrugUnits, context.Sites);
            Console.WriteLine("");

            Console.WriteLine("------------------");
            Console.WriteLine("Caller request:");
            PrintCallerRequest(siteDistributionService.GetRequestedDrugUnits("1", "2", 2), 2);

            Console.WriteLine("------------------");
            Console.WriteLine("Correlation data:");
            var correlationData = correlationService.CorrelateData();
            PrintCorellationData(correlationData);
            Console.WriteLine("");

            Console.WriteLine("---------");
            Console.WriteLine("Grouping:");
            Dictionary<string, List<DrugUnit>> groupedDrugUnits = Extensions.ToGroupedDrugUnits(context.DrugUnits, context.DrugTypes);
            PrintGroupedDrugUnits(groupedDrugUnits);
            Console.WriteLine("");

            /*
            Console.WriteLine("------------------");
            Console.WriteLine("Request update :");
            int result = inventoryDbHander.UpdateSiteInventory("2", "1", 1);
            if (result != 0)
                Console.WriteLine("!WARNING! There were not enought drug units to assign the requested capacity!");
            List<Depot> depotFromDatabase1 = Depot.LoadFromDatabase();
            PrintAll(context.Countries, depotFromDatabase1, context.DrugTypes, context.DrugUnits, context.Sites);
            Console.WriteLine("");
    
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Data after Drug Disassociation:");
            depotInventoryServices.DisassociateDrugs(context.DrugUnits, context.Depots, 1, 20);
            PrintAll(context.Countries, context.Depots, context.DrugTypes, context.DrugUnits, context.Sites);
            Console.WriteLine("");

            Console.WriteLine("----------------------------");
            Console.WriteLine("Data after Drug Association:");
            depotInventoryServices.AssociateDrugs(context.DrugUnits, context.Depots, "1", 2, 10);
            depotInventoryServices.AssociateDrugs(context.DrugUnits, context.Depots, "2", 15, 19);
            PrintAll(context.Countries, context.Depots, context.DrugTypes, context.DrugUnits, context.Sites);
            Console.WriteLine("");
            */
        }
    }
}
