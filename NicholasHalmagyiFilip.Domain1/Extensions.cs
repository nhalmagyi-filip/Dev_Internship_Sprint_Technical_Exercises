using Microsoft.EntityFrameworkCore;
using NicholasHalmagyiFilip.DataModelCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DomainCore
{
    public static class Extensions
    {
        public static Dictionary<string, List<DrugUnit>> ToGroupedDrugUnits(this DbSet<DrugUnit> drugUnits, DbSet<DrugType> drugTypes)
        {
            var drugUnitList = drugUnits.ToList();
            var drugTypeList = drugTypes.ToList();

            return drugUnitList
                .Join(drugTypeList,
                      drugUnit => drugUnit.DrugUnitDrugTypeId,
                      drugType => drugType.DrugTypeId,
                      (drugUnit, drugType) => new { DrugUnit = drugUnit, DrugType = drugType })
                .GroupBy(pr => pr.DrugType.DrugTypeName)
                .ToDictionary(gr => gr.Key, gr => gr.Select(pr => pr.DrugUnit).ToList());
        }
    }
}
