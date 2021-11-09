using Identity.Domain;
using Identity.Persistence.Database.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Persistence.Database
{
    public class IdentityAppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder builder)
        {
            new ApplicationUserConfiguration(builder.Entity<ApplicationUser>());
            new ApplicationRoleConfiguration(builder.Entity<ApplicationRole>());
        }
    }
}
