using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinger.Models
{
    public class Site
    {
        public Guid Id { get; set; }
        public string Host { get; set; }
        public int PingFrequency { get; set; }
    }
}
