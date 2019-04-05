using Pinger.DAL.Models;
using System;
using System.Collections.Generic;

namespace Pinger.DAL.Cache
{
    public interface ICacheRepository
    {
        void Init(IEnumerable<SiteOptions> sites);
        ICollection<SiteOptions> GetSites();
        void UpdateSiteLastPing(Guid id, bool isAvailable, DateTime lastPing);
        void Add(SiteOptions site);
        void Update(SiteOptions site);
        void Delete(Guid id);
    }
}
