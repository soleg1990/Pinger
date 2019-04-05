using Pinger.DAL.EF;
using Pinger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pinger.Test
{
    public class FakeDbRepository : IDbRepository
    {
        public Task<DbSiteOptions> CreateAsync(DbSiteOptions site)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DbSiteOptions>> GetSitesAsync()
        {
            return await Task.Run(() => {
                return new List<DbSiteOptions>
                {
                    new DbSiteOptions{Id = Guid.NewGuid(), Host = "yandex.ru", PingFrequency = 10},
                    new DbSiteOptions{Id = Guid.NewGuid(), Host = "google.com", PingFrequency = 15}
                };
            });
        }

        public async Task<bool> UpdateAsync(DbSiteOptions site)
        {
            return await Task.Run(() => { return true; });
        }
    }
}
