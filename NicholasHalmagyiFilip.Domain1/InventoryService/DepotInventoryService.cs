using Microsoft.EntityFrameworkCore;
using NicholasHalmagyiFilip.DataModelCore;
using NicholasHalmagyiFilip.DataModelCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DomainCore
{
    public class DepotInventoryService : IDepotInventoryService
    {
        protected readonly AppDbContext DataSet;
        public DepotInventoryService(AppDbContext dataSet)
        {
            this.DataSet = dataSet;
        }

        public void AssociateDrugs(string depotId, int startPickNumber, int endPickNumber)
        {
            int depotIdNr = Int32.Parse(depotId);

            DataSet.DrugUnits
                .Where(drugUnit => drugUnit.DrugUnitPickNumber > startPickNumber && drugUnit.DrugUnitPickNumber < endPickNumber)
                .ToList()
                .ForEach(drugUnit => drugUnit.DrugUnitDepotId = depotIdNr);
   
            DataSet.SaveChanges();
        }

        public void DisassociateDrugs(int startPickNumber, int endPickNumber)
        {
            DataSet.DrugUnits
                .Where(drugUnit => drugUnit.DrugUnitPickNumber > startPickNumber && drugUnit.DrugUnitPickNumber < endPickNumber)
                .ToList()
                .ForEach(drugUnit => drugUnit.DrugUnitDepotId = null);

            DataSet.SaveChanges();
        }
    }
}