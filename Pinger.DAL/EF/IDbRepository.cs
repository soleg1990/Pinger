using Pinger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pinger.DAL.EF
{
    public interface IDbRepository
    {
        Task<ICollection<DbSiteOptions>> GetSitesAsync();
        Task<DbSiteOptions> CreateAsync(DbSiteOptions site);
        Task<bool> UpdateAsync(DbSiteOptions site);
        Task<bool> DeleteAsync(Guid id);
    }
}
