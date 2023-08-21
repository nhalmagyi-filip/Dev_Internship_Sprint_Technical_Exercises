using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DataModelCore.Models
{
    public class DrugType
    {
        public int DrugTypeId { get; set; }
        public string DrugTypeName { get; set; }
        public double Weight { get; set; }

        public override string ToString()
        {
            return $"Drug Type ID: {DrugTypeId} || Drug Type Name: {DrugTypeName}";
        }
    }
}
