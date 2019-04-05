using Pinger.DAL.DTOs;
using Pinger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pinger.DAL
{
    public interface IWebRepository
    {
        ICollection<SiteOptions> GetSites();
        Task<SiteOptionsDto> CreateAsync(SiteOptionsDto site);
        Task<bool> UpdateAsync(SiteOptionsDto site);
        Task<bool> DeleteAsync(Guid id);
    }
}
