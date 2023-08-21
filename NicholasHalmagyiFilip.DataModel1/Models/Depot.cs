using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DataModelCore.Models
{
    public class Depot
    {
        public int DepotId { get; set; }
        public string DepotName { get; set; }

        public ICollection<Country> DepotCountries { get; set; } 
        public ICollection<DrugUnit> DepotDrugUnits { get; set; } 

        public override string ToString()
        {
            string countries = string.Join(", ", DepotCountries.Select(c => c.CountryId));
            string drugUnits = string.Join(", ", DepotDrugUnits.Select(d => d.DrugUnitId));

            return $"Depot ID: {DepotId} || Depot Name: {DepotName} || Countries ID's: {countries} || Drug Units ID's: {drugUnits}";
        }

        public Depot()
        {
            DepotCountries = new List<Country>(); 
            DepotDrugUnits = new List<DrugUnit>();
        }

        public static List<Depot> LoadFromDatabase()
        {
            using (var context = new AppDbContext())
            {
                return context.Depots
                    .Include(d => d.DepotCountries)
                    .Include(d => d.DepotDrugUnits)
                    .ToList();
            }
        }
    }
}
