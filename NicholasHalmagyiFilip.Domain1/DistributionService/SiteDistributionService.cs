
using NicholasHalmagyiFilip.DataModelCore;
using NicholasHalmagyiFilip.DataModelCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DomainCore
{
    public class SiteDistributionService : ISiteDistributionService
    {
        protected readonly AppDbContext DataSet;
        public SiteDistributionService(AppDbContext dataSet)
        {
            this.DataSet = dataSet;
        }

        public IEnumerable<DrugUnit> GetRequestedDrugUnits(string siteId, string drugCode, int quantity)
        {
            int siteIdInt = Int32.Parse(siteId);
            int countryId = DataSet.Sites
                .Where(id => id.SiteId == siteIdInt)
                .Select(code => code.CountryCode)
                .FirstOrDefault();

            int depotId = DataSet.Countries
                .Where(id => id.CountryId == countryId)
                .Select(did => did.CountryDepotId)
                .FirstOrDefault();

            int drugCodeInt = Int32.Parse(drugCode);
            IEnumerable<DrugUnit> request = DataSet.DrugUnits
                .Where(r => r.DrugUnitDepotId == depotId && r.DrugUnitDrugTypeId == drugCodeInt)
                .Take(quantity);

            return request;
        }
    }
}
