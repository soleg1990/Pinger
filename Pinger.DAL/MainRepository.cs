using Pinger.DAL.Cache;
using Pinger.DAL.DTOs;
using Pinger.DAL.EF;
using Pinger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinger.DAL
{
    public class MainRepository : IWebRepository, IBackgroundRepository
    {
        private IDbRepository dbRepository;

        private static volatile CacheRepository cache;
        private static object syncRoot = new Object();
        private CacheRepository Cache
        {
            get
            {
                if (cache == null)
                {
                    lock (syncRoot)
                    {
                        if (cache == null)
                        {
                            cache = new CacheRepository();
                            var sites = GetSitesFromDb()
                                .Select(s => new SiteOptions
                                {
                                    Id = s.Id,
                                    Host = s.Host,
                                    PingFrequency = s.PingFrequency
                                });
                            cache.Init(sites);
                        }
                    }
                }
                return cache;
            }
        }

        public MainRepository(IDbRepository repository)
        {
            dbRepository = repository;
        }

        public async Task<SiteOptionsDto> CreateAsync(SiteOptionsDto site)
        {
            var entity = await dbRepository.CreateAsync(site.MapToDb());
            Cache.Add(new SiteOptions { Id = entity.Id, Host = entity.Host, PingFrequency = entity.PingFrequency });
            return SiteOptionsDto.Map(entity);
        }

        public async Task<bool> UpdateAsync(SiteOptionsDto site)
        {
            if (await dbRepository.UpdateAsync(site.MapToDb()))
            {
                Cache.Update(site.MapToCache());
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (await dbRepository.DeleteAsync(id))
            {
                Cache.Delete(id);
                return true;
            }
            return false;
        }

        public ICollection<SiteOptions> GetSites()
        {
            return Cache.GetSites();
        }

        public void UpdateSiteLastPing(Guid id, bool isAvailable, DateTime lastPing)
        {
            Cache.UpdateSiteLastPing(id, isAvailable, lastPing);
        }

        private IEnumerable<DbSiteOptions> GetSitesFromDb()
        {
            return dbRepository.GetSitesAsync().GetAwaiter().GetResult();
        }
    }
}
