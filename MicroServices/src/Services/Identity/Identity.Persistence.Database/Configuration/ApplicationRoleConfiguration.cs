using Identity.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Persistence.Database.Configuration
{
    public class ApplicationRoleConfiguration
    {
        public ApplicationRoleConfiguration(EntityTypeBuilder<ApplicationRole> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);

            entityTypeBuilder.HasData(new ApplicationRole 
            {
                Id = Guid.NewGuid().ToString().ToLower(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            entityTypeBuilder.HasMany(e => e.UserRoles).WithOne(e => e.Role).HasForeignKey(e => e.RoleId).IsRequired();
        }
    }
}
