using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Pinger.DAL.Models
{
    [Table("SiteOptions")]
    public class DbSiteOptions
    {
        public Guid Id { get; set; }
        public string Host { get; set; }
        public int PingFrequency { get; set; }
    }
}
