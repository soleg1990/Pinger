using Pinger.DAL.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Pinger.DAL.Cache
{
    internal class CacheRepository : ICacheRepository
    {
        private ConcurrentDictionary<Guid, SiteOptions> cache = new ConcurrentDictionary<Guid, SiteOptions>();

        public CacheRepository() { }

        public void Init(IEnumerable<SiteOptions> sites)
        {
            foreach (var s in sites)
            {
                cache.TryAdd(s.Id, s);
            }
        }

        public void Add(SiteOptions site)
        {
            cache.TryAdd(site.Id, site);
        }

        public void Update(SiteOptions site)
        {
            if (cache.TryGetValue(site.Id, out var oldSite))
            {
                oldSite.Host = site.Host;
                oldSite.PingFrequency = site.PingFrequency;
            }
        }

        public void Delete(Guid id)
        {
            cache.TryRemove(id, out var value);
        }

        public ICollection<SiteOptions> GetSites()
        {
            return cache?.Values;
        }

        public void UpdateSiteLastPing(Guid id, bool isAvailable, DateTime lastPing)
        {
            if (cache.TryGetValue(id, out var oldSite))
            {
                oldSite.LastPing = lastPing;
                oldSite.IsAvailable = isAvailable;
            }
        }
    }
}
