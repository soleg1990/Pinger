using Microsoft.EntityFrameworkCore;
using Pinger.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pinger.DAL.EF
{
    public class EFContext : DbContext
    {
        internal DbSet<DbSiteOptions> Sites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=pinger.db;");
        }

    }
}
