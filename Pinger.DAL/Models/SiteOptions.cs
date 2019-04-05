using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger.DAL.Models
{
    public class SiteOptions
    {
        public Guid Id { get; internal set; }
        public string Host { get; internal set; }
        public int PingFrequency { get; internal set; }
        public DateTime LastPing { get; internal set; }
        public bool IsAvailable { get; internal set; }
    }
}
