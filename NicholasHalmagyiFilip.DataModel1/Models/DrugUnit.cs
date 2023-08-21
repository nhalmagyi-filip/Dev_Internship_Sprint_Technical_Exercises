using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DataModelCore.Models
{
    public class DrugUnit
    {
        public int DrugUnitId { get; set; }
        public int DrugUnitPickNumber { get; set; }
        public int? DrugUnitDepotId { get; set; }
        public int DrugUnitDrugTypeId { get; set; }
        public Depot DrugUnitDepot { get; set; }
        public DrugType DrugUnitDrugType { get; set; }

        public override string ToString()
        {
            return $"Drug Unit ID: {DrugUnitId} || Drug Unit Pick Number: {DrugUnitPickNumber} || Depot ID: {DrugUnitDepotId} || Drug Type ID: {DrugUnitDrugTypeId}";
        }
    }
}
