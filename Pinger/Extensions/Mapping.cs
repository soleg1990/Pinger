using Pinger.DAL.DTOs;
using Pinger.DAL.Models;
using Pinger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinger.Extensions
{
    internal static class Mapping
    {
        public static ICollection<SiteAvailability> MapToIndexViewModel(this ICollection<SiteOptions> sites)
        {
            if (sites == null) return null;

            //var vm = new IndexViewModel();
            var vmSites = new List<SiteAvailability>();
            foreach (var site in sites)
            {
                var sa = new SiteAvailability() { Host = site.Host, Status = site.IsAvailable ? "Доступен" : "Не доступен" };
                vmSites.Add(sa);
            }
            return vmSites;
        }

        public static ICollection<Site> MapToAdminViewModel(this ICollection<SiteOptions> sites)
        {
            if (sites == null) return null;

            //var vm = new AdminViewModel();
            var vmSites = new List<Site>();
            foreach (var site in sites)
            {
                var sa = new Site() { Id = site.Id, Host = site.Host, PingFrequency = site.PingFrequency };
                vmSites.Add(sa);
            }
            return vmSites;
        }

        public static SiteOptionsDto MapToDto(this Site site)
        {
            return new SiteOptionsDto { Id = site.Id, Host = site.Host, PingFrequency = site.PingFrequency };
        }
    }
}
