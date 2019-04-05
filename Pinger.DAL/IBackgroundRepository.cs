using Pinger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger.DAL
{
    public interface IBackgroundRepository
    {
        ICollection<SiteOptions> GetSites();
        void UpdateSiteLastPing(Guid id, bool isAvailable, DateTime lastPing);
    }
}
