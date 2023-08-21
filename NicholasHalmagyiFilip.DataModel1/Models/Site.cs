using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicholasHalmagyiFilip.DataModelCore.Models
{
    public class Site
    {
        public int SiteId { get; set; }

        public string SiteName { get; set; }

        public int CountryCode { get; set; }

        public Country Country { get; set; }

        public override string ToString()
        {
            return $"Site ID: {SiteId} || Site Name: {SiteName} || Country Code: {CountryCode}";
        }
    }
}
