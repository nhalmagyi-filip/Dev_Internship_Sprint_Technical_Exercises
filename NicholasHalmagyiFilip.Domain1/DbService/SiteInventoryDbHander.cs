using NicholasHalmagyiFilip.DataModelCore;
using NicholasHalmagyiFilip.DataModelCore.Models;
using NicholasHalmagyiFilip.DomainCore.DbService;
using System.Collections.Generic;
using System.Linq;

namespace NicholasHalmagyiFilip.DomainCore
{
    public class SiteInventoryDbHander : ISiteInventoryDbHandler 
    {
        protected readonly AppDbContext DataSet;
        private SiteDistributionService service;

        public SiteInventoryDbHander(AppDbContext dataSet, SiteDistributionService srvc)
        {
            this.DataSet = dataSet;
            this.service = srvc;
        }

        public int UpdateSiteInventory(string destinationSiteId, string requestedDrugCode, int requestedQuantity)
        {
            IEnumerable<DrugUnit> request = service.GetRequestedDrugUnits(destinationSiteId, requestedDrugCode, requestedQuantity);

            var depotId = request.First().DrugUnitDepotId;
            var drugType = request.First().DrugUnitDrugTypeId;

            foreach (DrugUnit drugUnit in DataSet.DrugUnits)
            {
                if (drugUnit.DrugUnitDepotId == null && drugUnit.DrugUnitDrugTypeId == drugType)
                {
                    drugUnit.DrugUnitDepotId = depotId;
                    requestedQuantity--;
                }

                if (requestedQuantity == 0)
                    break;
            }

            DataSet.SaveChanges();

            return requestedQuantity;
        }
    }
}
