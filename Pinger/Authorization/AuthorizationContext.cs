using Microsoft.EntityFrameworkCore;
using Pinger.Authorization.Models;
using System;

namespace Pinger.Authorization
{
    public class AuthorizationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AuthorizationContext(DbContextOptions<AuthorizationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";

            string adminLogin = "admin";
            string adminPassword = "admin";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            User adminUser = new User { Id = 1, Login = adminLogin, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
