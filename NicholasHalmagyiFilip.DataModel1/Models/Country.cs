using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DataModelCore.Models
{ 
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public int CountryDepotId { get; set; }

        public Depot CountryDepot { get; set; }

        public override string ToString()
        {
            return $"Country ID: {CountryId} || Country Name: {CountryName} || Depot ID: {CountryDepotId}";
        }
    }
}
