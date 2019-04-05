using Pinger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger.DAL.DTOs
{
    public class SiteOptionsDto
    {
        public Guid Id { get; set; }
        public string Host { get; set; }
        public int PingFrequency { get; set; }

        internal DbSiteOptions MapToDb()
        {
            return new DbSiteOptions { Id = Id, Host = Host, PingFrequency = PingFrequency };
        }

        internal SiteOptions MapToCache()
        {
            return new SiteOptions { Id = Id, Host = Host, PingFrequency = PingFrequency };
        }

        internal static SiteOptionsDto Map(DbSiteOptions site)
        {
            return new SiteOptionsDto { Id = site.Id, Host = site.Host, PingFrequency = site.PingFrequency };
        }
    }
}
