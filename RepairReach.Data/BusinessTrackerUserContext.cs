using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessTracker.Data.Infrastructure.Mapping;
using Microsoft.AspNet.Identity.EntityFramework;
using ApplicationUser = BusinessTracker.Data.Infrastructure.Identity.ApplicationUser;

namespace BusinessTracker.Data
{
    public class BusinessTrackerUserContext : IdentityDbContext<ApplicationUser>
    {
        public BusinessTrackerUserContext()
            : base("BusinessTrackerDEV")
        {
            Database.Log = sql => Debug.Write(sql);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Configurations.Add(new UserEntityTypeConfigurator());
            
        }
    }
}
