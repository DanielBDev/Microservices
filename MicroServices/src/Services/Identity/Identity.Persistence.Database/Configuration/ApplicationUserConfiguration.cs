using Identity.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Persistence.Database.Configuration
{
    public class ApplicationUserConfiguration
    {
        public ApplicationUserConfiguration(EntityTypeBuilder<ApplicationUser> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);

            entityTypeBuilder.Property(x => x.FistName).IsRequired().HasMaxLength(100);
            entityTypeBuilder.Property(x => x.LastName).IsRequired().HasMaxLength(100);

            entityTypeBuilder.HasMany(e => e.UserRoles).WithOne(e => e.User).HasForeignKey(e => e.UserId).IsRequired();
        }
    }
}
