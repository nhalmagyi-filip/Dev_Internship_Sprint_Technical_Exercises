using NicholasHalmagyiFilip.DataModelCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace NicholasHalmagyiFilip.DataModelCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}
        public AppDbContext() {}

        public DbSet<Country> Countries { get; set; }
        public DbSet<Depot> Depots { get; set; }
        public DbSet<DrugUnit> DrugUnits { get; set; }
        public DbSet<DrugType> DrugTypes { get; set; }
        public DbSet<Site> Sites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>()
                .HasOne(d => d.CountryDepot)
                .WithMany(c => c.DepotCountries)
                .HasForeignKey(d => d.CountryDepotId);

            modelBuilder.Entity<DrugUnit>()
                .HasOne(d => d.DrugUnitDepot)
                .WithMany(de => de.DepotDrugUnits)
                .HasForeignKey(d => d.DrugUnitDepotId);

            modelBuilder.Entity<DrugUnit>()
                .HasOne(dt => dt.DrugUnitDrugType)
                .WithMany()
                .HasForeignKey(dt => dt.DrugUnitDrugTypeId);

            modelBuilder.Entity<Site>()
                .HasOne(c => c.Country)
                .WithMany()
                .HasForeignKey(c => c.CountryCode);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=DevSprint;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;");
        }

        public void AddInitialData()
        {
            var country1 = new Country
            {
                CountryName = "Romania",
                CountryDepotId = 1
            };
            Countries.Add(country1);

            var country2 = new Country
            {
                CountryName = "USA",
                CountryDepotId = 2
            };
            Countries.Add(country2);

            var depot1 = new Depot
            {
                DepotName = "RomaniaDepot",
            };
            depot1.DepotCountries.Add(country1);
            Depots.Add(depot1);

            var depot2 = new Depot
            {
                DepotName = "USADepot",
            };
            depot2.DepotCountries.Add(country2);
            Depots.Add(depot2);


            var drugType1 = new DrugType
            {
                DrugTypeName = "FirstDrugType"
            };
            DrugTypes.Add(drugType1);


            var drugType2 = new DrugType
            {
                DrugTypeName = "SecondDrugType"
            };
            DrugTypes.Add(drugType2);


            var site1 = new Site
            {
                SiteName = "FirstSite",
                CountryCode = country1.CountryId
            };
            site1.Country = country1;
            Sites.Add(site1);


            var site2 = new Site
            {
                SiteName = "SecondSite",
                CountryCode = country2.CountryId
            };
            site2.Country = country2;
            Sites.Add(site2);


            Random rand = new Random();
            for (int i = 0; i < 19; i++)
            {
                if (i % 2 == 0)
                {
                    var drugUnit = new DrugUnit
                    {
                        DrugUnitPickNumber = i + 1,
                        DrugUnitDrugTypeId = drugType1.DrugTypeId,
                    };
                    drugUnit.DrugUnitDrugType=drugType1;
                    DrugUnits.Add(drugUnit);
                }
                else
                {
                    var drugUnit = new DrugUnit
                    {
                        DrugUnitPickNumber = i + 1,
                        DrugUnitDrugTypeId =drugType2.DrugTypeId,
                    };
                    drugUnit.DrugUnitDrugType = drugType2;
                    DrugUnits.Add(drugUnit);
                }

            }
            SaveChanges();
        }

    }
}
