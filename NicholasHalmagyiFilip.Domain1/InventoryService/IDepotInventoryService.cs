using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NicholasHalmagyiFilip.DataModelCore.Models;

namespace NicholasHalmagyiFilip.DomainCore
{
    interface IDepotInventoryService
    {
        void AssociateDrugs(string depotId, int startPickNumber, int endPickNumber);
        void DisassociateDrugs(int startPickNumber, int endPickNumber);
    }
}
