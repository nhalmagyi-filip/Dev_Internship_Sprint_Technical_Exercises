using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DomainCore.DbService
{
    interface ISiteInventoryDbHandler
    {
        int UpdateSiteInventory(string destinationSiteId, string requestedDrugCode, int requestedQuantity);
    }
}
