using Microsoft.EntityFrameworkCore;
using Pinger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinger.DAL.EF
{
    public class EFRepository : IDbRepository
    {
        public async Task<ICollection<DbSiteOptions>> GetSitesAsync()
        {
            using (var context = new EFContext())
            {
                return await context.Sites.OrderBy(s => s.Host).ToListAsync();
            }
        }

        public async Task<DbSiteOptions> CreateAsync(DbSiteOptions site)
        {
            using (var context = new EFContext())
            {
                context.Sites.Add(site);
                await context.SaveChangesAsync();
                return site;
            }
        }

        public async Task<bool> UpdateAsync(DbSiteOptions site)
        {
            using (var context = new EFContext())
            {
                if (await context.Sites.AnyAsync(s => s.Id == site.Id))
                {
                    context.Sites.Update(site);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var context = new EFContext())
            {
                var site = await context.Sites.FindAsync(id);
                if (site == null)
                    return false;
                context.Sites.Remove(site);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
